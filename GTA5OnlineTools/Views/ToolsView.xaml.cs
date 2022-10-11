using GTA5OnlineTools.Windows;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Common.Helper;
using GTA5OnlineTools.Features.Core;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// ToolsView.xaml 的交互逻辑
/// </summary>
public partial class ToolsView : UserControl
{
    /// <summary>
    /// 工具按钮点击命令
    /// </summary>
    public RelayCommand<string> ToolsButtonClickCommand { get; private set; }

    private InjectorWindow InjectorWindow = null;

    public ToolsView()
    {
        InitializeComponent();
        this.DataContext = this;

        ToolsButtonClickCommand = new(ToolsButtonClick);
    }

    /// <summary>
    /// 工具按钮点击
    /// </summary>
    /// <param name="name"></param>
    private void ToolsButtonClick(string name)
    {
        AudioUtil.ClickSound();

        switch (name)
        {
            #region 分组1
            case "KiddionChsON":
                KiddionChsONClick();
                break;
            case "KiddionChsOFF":
                KiddionChsOFFClick();
                break;
            case "KiddionKey104":
                KiddionKey104Click();
                break;
            case "KiddionKey87":
                KiddionKey87Click();
                break;
            case "EditKiddionConfig":
                EditKiddionConfigClick();
                break;
            case "EditKiddionTP":
                EditKiddionTPClick();
                break;
            case "EditKiddionVC":
                EditKiddionVCClick();
                break;
            case "KiddionScriptsDirectory":
                KiddionScriptsDirectory();
                break;
            case "GetKiddionText":
                GetKiddionTextClick();
                break;
            #endregion
            ////////////////////////////////////
            #region 分组2
            case "GTA5Win2TopMost":
                GTA5Win2TopMostClick();
                break;
            case "GTA5Win2NoTopMost":
                GTA5Win2NoTopMostClick();
                break;
            case "RestartApp":
                RestartAppClick();
                break;
            case "InitCPDPath":
                InitCPDPathClick();
                break;
            case "EditGTAHaxStat":
                EditGTAHaxStatClick();
                break;
            case "BigBaseV2Directory":
                BigBaseV2DirectoryClick();
                break;
            case "StoryModeArchive":
                StoryModeArchiveClick();
                break;
            case "BaseInjector":
                BaseInjectorClick();
                break;
            case "OpenUpdateWindow":
                OpenUpdateWindowClick();
                break;
            case "DefenderControl":
                DefenderControlClick();
                break;
            #endregion
            ////////////////////////////////////
            #region 分组3
            case "CurrentDirectory":
                CurrentDirectoryClick();
                break;
            case "ReleaseDirectory":
                ReleaseDirectoryClick();
                break;
            case "ReNameAppCN":
                ReNameAppCNClick();
                break;
            case "ReNameAppEN":
                ReNameAppENClick();
                break;
            case "RefreshDNSCache":
                RefreshDNSCacheClick();
                break;
            case "EditHosts":
                EditHostsClick();
                break;
            case "MinimizeToTray":
                MinimizeToTrayClick();
                break;
            case "ManualGC":
                ManualGCClick();
                break;
            #endregion
            ////////////////////////////////////
            default:
                break;
        }
    }

    ////////////////////////////////////////////////////////////////////////

    #region 分组1
    /// <summary>
    /// 启用Kiddion汉化
    /// </summary>
    private void KiddionChsONClick()
    {
        if (ProcessUtil.IsAppRun("Kiddion"))
        {
            ProcessUtil.CloseProcess("Kiddion_Chs");
            ProcessUtil.OpenProcess("Kiddion_Chs", true);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请先启动《Kiddion》程序");
        }
    }

    /// <summary>
    /// 关闭Kiddion汉化
    /// </summary>
    private void KiddionChsOFFClick()
    {
        if (ProcessUtil.IsAppRun("Kiddion_Chs"))
        {
            ProcessUtil.CloseProcess("Kiddion_Chs");
            NotifierHelper.Show(NotifierType.Success, "关闭《Kiddion汉化》成功");
        }
        else
        {
            NotifierHelper.Show(NotifierType.Notification, "未发现《Kiddion汉化》进程");
        }
    }

    /// <summary>
    /// 启用Kiddion[104键]
    /// </summary>
    private void KiddionKey104Click()
    {
        ProcessUtil.CloseProcess("Kiddion");
        ProcessUtil.CloseProcess("Kiddion_Chs");
        FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "config.json", FileUtil.Kiddion_Path + @"config.json");
        NotifierHelper.Show(NotifierType.Success, "切换到《Kiddion [104键]》成功");
    }

    /// <summary>
    /// 启用Kiddion[87键]
    /// </summary>
    private void KiddionKey87Click()
    {
        ProcessUtil.CloseProcess("Kiddion");
        ProcessUtil.CloseProcess("Kiddion_Chs");
        FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "key87.config.json", FileUtil.Kiddion_Path + @"config.json");
        NotifierHelper.Show(NotifierType.Success, "切换到《Kiddion [87键]》成功");
    }

    /// <summary>
    /// 编辑Kiddion配置文件
    /// </summary>
    private void EditKiddionConfigClick()
    {
        ProcessUtil.OpenFileWithProcess("notepad.exe", FileUtil.Kiddion_Path + @"config.json");
    }

    /// <summary>
    /// 编辑Kiddion自定义传送
    /// </summary>
    private void EditKiddionTPClick()
    {
        ProcessUtil.OpenFileWithProcess("notepad.exe", FileUtil.Kiddion_Path + @"teleports.json");
    }

    /// <summary>
    /// 编辑Kiddion自定义载具
    /// </summary>
    private void EditKiddionVCClick()
    {
        ProcessUtil.OpenFileWithProcess("notepad.exe", FileUtil.Kiddion_Path + @"vehicles.json");
    }

    /// <summary>
    /// Kiddion脚本目录
    /// </summary>
    private void KiddionScriptsDirectory()
    {
        ProcessUtil.OpenPath(FileUtil.KiddionScripts_Path);
    }

    /// <summary>
    /// 获取Kiddion UI文本
    /// </summary>
    private void GetKiddionTextClick()
    {
        ProcessUtil.OpenProcess("GetKidTxt", false);
    }
    #endregion

    ////////////////////////////////////////////////////////////////////////

    #region 分组2
    /// <summary>
    /// GTA5窗口置顶
    /// </summary>
    private void GTA5Win2TopMostClick()
    {
        GTA5Mem.TopMostProcess(true);
        NotifierHelper.Show(NotifierType.Success, "置顶GTA5窗口成功");
    }

    /// <summary>
    /// GTA5窗口取消置顶
    /// </summary>
    private void GTA5Win2NoTopMostClick()
    {
        GTA5Mem.TopMostProcess(false);
        NotifierHelper.Show(NotifierType.Success, "取消置顶GTA5窗口成功");
    }

    /// <summary>
    /// 重启程序
    /// </summary>
    private void RestartAppClick()
    {
        ProcessUtil.CloseThirdProcess();
        App.AppMainMutex.Dispose();
        ProcessUtil.OpenPath(FileUtil.Current_Path);
        Application.Current.Shutdown();
    }

    /// <summary>
    /// 初始化配置文件夹
    /// </summary>
    private void InitCPDPathClick()
    {
        try
        {
            if (MessageBox.Show("你确定要初始化配置文件吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Directory.SetCurrentDirectory(FileUtil.CurrentDirectory_Path);
                ProcessUtil.CloseThirdProcess();
                Thread.Sleep(20);
                if (File.Exists(FileUtil.Default_Path))
                    Directory.Delete(FileUtil.Default_Path, true);
                Directory.CreateDirectory(FileUtil.Default_Path);

                App.AppMainMutex.Dispose();
                ProcessUtil.OpenPath(FileUtil.Current_Path);
                Application.Current.Shutdown();
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 编辑GTAHax导入文件
    /// </summary>
    private void EditGTAHaxStatClick()
    {
        ProcessUtil.OpenFileWithProcess("notepad.exe", FileUtil.GTAHaxStat_Path);
    }

    /// <summary>
    /// BigBaseV2配置目录
    /// </summary>
    private void BigBaseV2DirectoryClick()
    {
        ProcessUtil.OpenPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/BigBaseV2/");
    }

    /// <summary>
    /// 故事模式完美存档
    /// </summary>
    private void StoryModeArchiveClick()
    {
        var path = Path.Combine(FileUtil.MyDocuments_Path, @"Rockstar Games\GTA V\Profiles");
        if (!Directory.Exists(path))
        {
            NotifierHelper.Show(NotifierType.Error, "GTA5故事模式存档路径不存在，操作取消");
            return;
        }

        if (MessageBox.Show("你确定替换GTA5故事模式存档吗？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        {
            try
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    var dirIf = new DirectoryInfo(dir);
                    string fullName = Path.Combine(dirIf.FullName, "SGTA50000");
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "Other.SGTA50000", fullName);
                }

                NotifierHelper.Show(NotifierType.Success, $"GTA5故事模式存档替换成功\n{path}");
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowException(ex);
            }
        }
    }

    /// <summary>
    /// 基础DLL注入器
    /// </summary>
    private void BaseInjectorClick()
    {
        if (InjectorWindow == null)
        {
            InjectorWindow = new InjectorWindow();
            InjectorWindow.Show();
        }
        else
        {
            if (InjectorWindow.IsVisible)
            {
                InjectorWindow.Topmost = true;
                InjectorWindow.Topmost = false;
                InjectorWindow.WindowState = WindowState.Normal;
            }
            else
            {
                InjectorWindow = null;
                InjectorWindow = new InjectorWindow();
                InjectorWindow.Show();
            }
        }
    }

    /// <summary>
    /// 打开更新窗口
    /// </summary>
    private void OpenUpdateWindowClick()
    {
        var UpdateWindow = new UpdateWindow
        {
            Owner = MainWindow.MainWindowInstance
        };
        UpdateWindow.ShowDialog();
    }

    /// <summary>
    /// Win10安全中心设置
    /// </summary>
    private void DefenderControlClick()
    {
        ProcessUtil.OpenProcess("dControl", false);
    }
    #endregion

    ////////////////////////////////////////////////////////////////////////

    #region 分组3
    /// <summary>
    /// 程序当前目录
    /// </summary>
    private void CurrentDirectoryClick()
    {
        ProcessUtil.OpenPath(FileUtil.CurrentDirectory_Path);
    }

    /// <summary>
    /// 程序释放目录
    /// </summary>
    private void ReleaseDirectoryClick()
    {
        ProcessUtil.OpenPath(FileUtil.Default_Path);
    }

    /// <summary>
    /// 重命名小助手为中文
    /// </summary>
    private void ReNameAppCNClick()
    {
        try
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUtil.Current_Path);
            if (fileNameWithoutExtension != (CoreUtil.MainAppWindowName + CoreUtil.ClientVersion))
            {
                FileUtil.FileReName(FileUtil.Current_Path, FileUtil.GetCurrFullPath(CoreUtil.MainAppWindowName + CoreUtil.ClientVersion + ".exe"));

                ProcessUtil.CloseThirdProcess();
                App.AppMainMutex.Dispose();
                ProcessUtil.OpenPath(FileUtil.GetCurrFullPath(CoreUtil.MainAppWindowName + CoreUtil.ClientVersion + ".exe"));
                Application.Current.Shutdown();
            }
            else
            {
                NotifierHelper.Show(NotifierType.Notification, "程序文件名已经符合中文命名标准，无需继续重命名");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 重命名小助手为英文
    /// </summary>
    private void ReNameAppENClick()
    {
        try
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUtil.Current_Path);
            if (fileNameWithoutExtension != "GTA5OnlineTools")
            {
                FileUtil.FileReName(FileUtil.Current_Path, "GTA5OnlineTools.exe");

                ProcessUtil.CloseThirdProcess();
                App.AppMainMutex.Dispose();
                ProcessUtil.OpenPath(FileUtil.GetCurrFullPath("GTA5OnlineTools.exe"));
                Application.Current.Shutdown();
            }
            else
            {
                NotifierHelper.Show(NotifierType.Notification, "程序文件名已经符合英文命名标准，无需继续重命名");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 刷新DNS缓存
    /// </summary>
    private void RefreshDNSCacheClick()
    {
        CoreUtil.FlushDNSCache();
        MainWindow.ActionShowNoticeInfo("成功刷新DNS解析程序缓存");
    }

    /// <summary>
    /// 编辑Hosts文件
    /// </summary>
    private void EditHostsClick()
    {
        ProcessUtil.OpenFileWithProcess("notepad.exe", @"C:\windows\system32\drivers\etc\hosts");
    }

    /// <summary>
    /// 最小化程序到系统托盘
    /// </summary>
    private void MinimizeToTrayClick()
    {
        MainWindow.MainWindowInstance.WindowState = WindowState.Minimized;
        MainWindow.MainWindowInstance.ShowInTaskbar = false;
        MainWindow.ActionShowNoticeInfo("程序已最小化到托盘");
    }

    /// <summary>
    /// GC垃圾回收
    /// </summary>
    private void ManualGCClick()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        NotifierHelper.Show(NotifierType.Notification, "执行GC垃圾回收成功");
    }
    #endregion
}
