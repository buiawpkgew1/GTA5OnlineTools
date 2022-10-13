using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Common.Helper;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

using Chinese;
using RestSharp;
using Forms = System.Windows.Forms;

namespace GTA5OnlineTools.Views.Modules;

/// <summary>
/// SessionChatView.xaml 的交互逻辑
/// </summary>
public partial class SessionChatView : UserControl
{
    private const string youdaoAPI = "http://fanyi.youdao.com/translate?&doctype=json&type=AUTO&i=";

    public SessionChatView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        TextBox_InputMessage.Text = "测试文本: 请把游戏中聊天输入法调成英文,否则会漏掉文字.Hello1234,漏掉文字了吗?";
        ReadPlayerName();

        if (File.Exists(FileUtil.BlockWords_Path))
        {
            try
            {
                var file = new StreamReader(FileUtil.BlockWords_Path, Encoding.Default);
                string content = string.Empty;
                while (content != null)
                {
                    content = file.ReadLine();
                    if (!string.IsNullOrEmpty(content))
                        ListBox_BlcokWords.Items.Add(content);
                }
                file.Close();
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowException(ex);
            }
        }
        else
        {
            DefaultBlcokWords();
        }
    }

    private void MainWindow_WindowClosingEvent()
    {
        SaveBlcokWords();
    }

    private void Button_Translate_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            var message = TextBox_InputMessage.Text.Trim();

            if (!string.IsNullOrEmpty(message))
            {
                var btnContent = (e.OriginalSource as Button).Content.ToString();

                switch (btnContent)
                {
                    case "中英互译":
                        YouDaoTranslation(message);
                        break;
                    case "简转繁":
                        TextBox_InputMessage.Text = ChineseConverter.ToTraditional(message);
                        break;
                    case "繁转简":
                        TextBox_InputMessage.Text = ChineseConverter.ToSimplified(message);
                        break;
                    case "转拼音":
                        TextBox_InputMessage.Text = Pinyin.GetString(message, PinyinFormat.WithoutTone);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    private void Button_SendTextToGTA5_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        try
        {
            if (TextBox_InputMessage.Text != "")
            {
                TextBox_InputMessage.Text = ToDBC(TextBox_InputMessage.Text);

                GTA5Mem.SetForegroundWindow();

                SendMessageToGTA5(TextBox_InputMessage.Text);
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    private void KeyPress(WinVK winVK)
    {
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        Win32.Keybd_Event(winVK, Win32.MapVirtualKey(winVK, 0), 0, 0);
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        Win32.Keybd_Event(winVK, Win32.MapVirtualKey(winVK, 0), 2, 0);
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
    }

    private void SendMessageToGTA5(string str)
    {
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));

        KeyPress(WinVK.RETURN);

        if (RadioButton_PressKeyT.IsChecked == true)
            KeyPress(WinVK.T);
        else
            KeyPress(WinVK.Y);

        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));
        Forms.SendKeys.Flush();
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        Forms.SendKeys.SendWait(str);
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep2.Value));
        Forms.SendKeys.Flush();
        Thread.Sleep(Convert.ToInt32(Slider_SendKey_Sleep1.Value));

        KeyPress(WinVK.RETURN);
        KeyPress(WinVK.RETURN);
    }

    /// <summary>
    /// 调用有道翻译中英互译API
    /// </summary>
    /// <param name="message"></param>
    private async void YouDaoTranslation(string message)
    {
        try
        {
            var stringBuilder = new StringBuilder();

            var options = new RestClientOptions(youdaoAPI + message)
            {
                MaxTimeout = 5000,
                FollowRedirects = true
            };
            var client = new RestClient(options);

            var request = new RestRequest();
            var response = await client.ExecuteGetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var rb = JsonUtil.JsonDese<ReceiveObj>(response.Content);

                foreach (var item in rb.translateResult)
                {
                    foreach (var t in item)
                    {
                        stringBuilder.Append(t.tgt);
                    }
                }

                TextBox_InputMessage.Text = stringBuilder.ToString();
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    private void TextBox_InputMessage_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            e.Handled = true;
            Button_SendTextToGTA5_Click(null, null);
        }
    }

    private string ToDBC(string input)
    {
        char[] c = input.ToCharArray();

        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }

            if (c[i] > 65280 && c[i] < 65375)
            {
                c[i] = (char)(c[i] - 65248);
            }
        }

        return new string(c);
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenPath(e.Uri.OriginalString);
        e.Handled = true;
    }

    private void Button_ReadPlayerName_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        ReadPlayerName();
    }

    private void Button_WritePlayerName_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        if (TextBox_OnlineList.Text != "" &&
            TextBox_ChatName.Text != "" &&
            TextBox_ExternalDisplay.Text != "")
        {
            GTA5Mem.WriteString(General.WorldPTR, Offsets.OnlineListPlayerName, TextBox_OnlineList.Text + "\0");
            GTA5Mem.WriteString(General.PlayerChatterNamePTR + 0xBC, null, TextBox_ChatName.Text + "\0");
            GTA5Mem.WriteString(General.PlayerExternalDisplayNamePTR + 0x84, null, TextBox_ExternalDisplay.Text + "\0");

            NotifierHelper.Show(NotifierType.Success, "写入成功，请切换战局生效");
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "内容不能为空");
        }
    }

    private void ReadPlayerName()
    {
        TextBox_OnlineList.Text = GTA5Mem.ReadString(General.WorldPTR, Offsets.OnlineListPlayerName, 20);
        TextBox_ChatName.Text = GTA5Mem.ReadString(General.PlayerChatterNamePTR + 0xBC, null, 20);
        TextBox_ExternalDisplay.Text = GTA5Mem.ReadString(General.PlayerExternalDisplayNamePTR + 0x84, null, 20);
    }

    private void TextBox_OnlineList_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox_ChatName.Text = TextBox_OnlineList.Text;
        TextBox_ExternalDisplay.Text = TextBox_OnlineList.Text;
    }

    /////////////////////////////////////////////////////////////////////////////

    #region 战局垃圾信息拦截
    private void Button_AddBlcokWords_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var txt = TextBox_InputBlcokWord.Text;
        if (!string.IsNullOrEmpty(txt))
        {
            foreach (string item in ListBox_BlcokWords.Items)
            {
                if (item.Equals(txt))
                {
                    NotifierHelper.Show(NotifierType.Warning, $"关键词 {txt} 已经添加过了，请勿重复添加");
                    return;
                }
            }

            ListBox_BlcokWords.Items.Add(txt);
            ListBox_BlcokWords.SelectedIndex = ListBox_BlcokWords.Items.Count - 1;
            TextBox_InputBlcokWord.Clear();
        }
    }

    private void Button_RemoveBlcokWords_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var index = ListBox_BlcokWords.SelectedIndex;
        if (index != -1)
        {
            ListBox_BlcokWords.Items.RemoveAt(index);
            ListBox_BlcokWords.SelectedIndex = ListBox_BlcokWords.Items.Count - 1;
        }
    }

    private void Button_InjectGTA5Process_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        SaveBlcokWords();

        var _DLLPath = FileUtil.Inject_Path + "BlcokMsg.dll";

        if (!File.Exists(_DLLPath))
        {
            NotifierHelper.Show(NotifierType.Error, "发生异常，目标DLL文件不存在");
            return;
        }

        foreach (ProcessModule module in Process.GetProcessById(GTA5Mem.GTA5ProId).Modules)
        {
            if (module.FileName == _DLLPath)
            {
                NotifierHelper.Show(NotifierType.Warning, "该DLL已经被注入过了，请勿重复注入");
                return;
            }
        }

        GTA5Mem.SetForegroundWindow();

        try
        {
            BaseInjector.DLLInjector(GTA5Mem.GTA5ProId, _DLLPath);
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    private void Button_SaveBlcokWords_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        SaveBlcokWords();
    }

    private void SaveBlcokWords()
    {
        try
        {
            using var fs = new FileStream(FileUtil.BlockWords_Path, FileMode.Create);
            using var sw = new StreamWriter(fs, Encoding.Default);
            for (int i = 0; i < ListBox_BlcokWords.Items.Count; i++)
            {
                sw.WriteLine(ListBox_BlcokWords.Items[i].ToString());
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    private void Button_DefaultBlcokWords_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        DefaultBlcokWords();
    }

    private void DefaultBlcokWords()
    {
        ListBox_BlcokWords.Items.Clear();

        ListBox_BlcokWords.Items.Add("网站");
        ListBox_BlcokWords.Items.Add("网址");
        ListBox_BlcokWords.Items.Add("www");
        ListBox_BlcokWords.Items.Add("com");
        ListBox_BlcokWords.Items.Add("top");
        ListBox_BlcokWords.Items.Add("xyz");
        ListBox_BlcokWords.Items.Add("cn");

        ListBox_BlcokWords.Items.Add("微信");
        ListBox_BlcokWords.Items.Add("vx");
        ListBox_BlcokWords.Items.Add("扣扣");
        ListBox_BlcokWords.Items.Add("qq");
        ListBox_BlcokWords.Items.Add("q群");
        ListBox_BlcokWords.Items.Add("加群");

        ListBox_BlcokWords.Items.Add("外挂");
        ListBox_BlcokWords.Items.Add("价格");

        ListBox_BlcokWords.Items.Add("毛");
        ListBox_BlcokWords.Items.Add("元");
        ListBox_BlcokWords.Items.Add("块");
        ListBox_BlcokWords.Items.Add("一亿");

        ListBox_BlcokWords.Items.Add("刷金");
        ListBox_BlcokWords.Items.Add("淘宝");
        ListBox_BlcokWords.Items.Add("代刷");
        ListBox_BlcokWords.Items.Add("辅助");
        ListBox_BlcokWords.Items.Add("一亿");
        ListBox_BlcokWords.Items.Add("手工");
        ListBox_BlcokWords.Items.Add("解封");
        ListBox_BlcokWords.Items.Add("下单");
        ListBox_BlcokWords.Items.Add("充值");
        ListBox_BlcokWords.Items.Add("科技");
        ListBox_BlcokWords.Items.Add("不要挂");
        ListBox_BlcokWords.Items.Add("恶搞");
        ListBox_BlcokWords.Items.Add("全网最低");
        ListBox_BlcokWords.Items.Add("地堡解锁");
        ListBox_BlcokWords.Items.Add("恶搞");
        ListBox_BlcokWords.Items.Add("自瞄");
        ListBox_BlcokWords.Items.Add("福利");
        ListBox_BlcokWords.Items.Add("保底");
        ListBox_BlcokWords.Items.Add("妹妹");
        ListBox_BlcokWords.Items.Add("妹子");

        ListBox_BlcokWords.Items.Add("上岛");
        ListBox_BlcokWords.Items.Add("免费带岛");
        ListBox_BlcokWords.Items.Add("萌新");
        ListBox_BlcokWords.Items.Add("加我送");
        ListBox_BlcokWords.Items.Add("挂狗");
        ListBox_BlcokWords.Items.Add("百分百");
        ListBox_BlcokWords.Items.Add("拍照保留");
        ListBox_BlcokWords.Items.Add("截图");
        ListBox_BlcokWords.Items.Add("开挂勿进");
        ListBox_BlcokWords.Items.Add("限时限量");

        ListBox_BlcokWords.Items.Add("gta");
        ListBox_BlcokWords.Items.Add("vip");

        ListBox_BlcokWords.Items.Add("单场百万");
        ListBox_BlcokWords.Items.Add("另售");
        ListBox_BlcokWords.Items.Add("有抽奖");

        ListBox_BlcokWords.Items.Add("麻豆");
        ListBox_BlcokWords.Items.Add("传媒");
        ListBox_BlcokWords.Items.Add("蜜桃星空");
        ListBox_BlcokWords.Items.Add("av");
        ListBox_BlcokWords.Items.Add("欧美");
        ListBox_BlcokWords.Items.Add("荷官");
        ListBox_BlcokWords.Items.Add("在线观看");
        ListBox_BlcokWords.Items.Add("处男");
        ListBox_BlcokWords.Items.Add("幼女");
        ListBox_BlcokWords.Items.Add("自慰");
        ListBox_BlcokWords.Items.Add("挂逼");
        ListBox_BlcokWords.Items.Add("强奸");
        ListBox_BlcokWords.Items.Add("人妻");
        ListBox_BlcokWords.Items.Add("日本");
        ListBox_BlcokWords.Items.Add("性爱");
        ListBox_BlcokWords.Items.Add("处女");
        ListBox_BlcokWords.Items.Add("乱伦");
        ListBox_BlcokWords.Items.Add("小电影");

        ListBox_BlcokWords.Items.Add("不过百");
        ListBox_BlcokWords.Items.Add("加入我们");
        ListBox_BlcokWords.Items.Add("修仙");
        ListBox_BlcokWords.Items.Add("加我免费");

    }
    #endregion
}

public class ReceiveObj
{
    public string type { get; set; }
    public int errorCode { get; set; }
    public int elapsedTime { get; set; }
    public List<List<TranslateResultItemItem>> translateResult { get; set; }
    public class TranslateResultItemItem
    {
        public string src { get; set; }
        public string tgt { get; set; }
    }
}
