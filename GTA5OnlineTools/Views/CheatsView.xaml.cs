using GTA5OnlineTools.Models;
using GTA5OnlineTools.Views.Cheats;
using GTA5OnlineTools.Features;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Common.Helper;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// CheatsView.xaml 的交互逻辑
/// </summary>
public partial class CheatsView : UserControl
{
    /// <summary>
    /// Third数据模型
    /// </summary>
    public CheatsModel CheatsModel { get; set; } = new();

    public RelayCommand<string> CheatsClickCommand { get; private set; }
    public RelayCommand<string> ReadMeClickCommand { get; private set; }
    public RelayCommand<string> PathClickCommand { get; private set; }

    public RelayCommand FrameHideClickCommand { get; private set; }

    private readonly KiddionPage KiddionPage = new();
    private readonly GTAHaxPage GTAHaxPage = new();
    private readonly BincoHaxPage BincoHaxPage = new();
    private readonly LSCHaxPage LSCHaxPage = new();
    private readonly YimMenuPage YimMenuPage = new();

    public CheatsView()
    {
        InitializeComponent();
        this.DataContext = this;

        CheatsClickCommand = new(CheatsClick);
        ReadMeClickCommand = new(ReadMeClick);
        PathClickCommand = new(PathClick);

        FrameHideClickCommand = new(FrameHideClick);

        new Thread(CheckCheatsIsRun)
        {
            Name = "CheckCheatsIsRun",
            IsBackground = true
        }.Start();
    }

    /// <summary>
    /// 检查第三方辅助是否正在运行线程
    /// </summary>
    private void CheckCheatsIsRun()
    {
        while (Globals.IsAppRunning)
        {
            // 判断 Kiddion 是否运行
            CheatsModel.KiddionIsRun = ProcessUtil.IsAppRun("Kiddion");
            // 判断 GTAHax 是否运行
            CheatsModel.GTAHaxIsRun = ProcessUtil.IsAppRun("GTAHax");
            // 判断 BincoHax 是否运行
            CheatsModel.BincoHaxIsRun = ProcessUtil.IsAppRun("BincoHax");
            // 判断 LSCHax 是否运行
            CheatsModel.LSCHaxIsRun = ProcessUtil.IsAppRun("LSCHax");

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 点击第三方辅助开关按钮
    /// </summary>
    /// <param name="hackName"></param>
    private void CheatsClick(string hackName)
    {
        AudioUtil.PlayClickSound();

        switch (hackName)
        {
            case "Kiddion":
                KiddionClick();
                break;
            case "GTAHax":
                GTAHaxClick();
                break;
            case "BincoHax":
                BincoHaxClick();
                break;
            case "LSCHax":
                LSCHaxClick();
                break;
            case "YimMenu":
                YimMenuClick();
                break;
        }
    }

    /// <summary>
    /// 点击第三方辅助使用说明
    /// </summary>
    /// <param name="pageName"></param>
    private void ReadMeClick(string pageName)
    {
        AudioUtil.PlayClickSound();

        switch (pageName)
        {
            case "KiddionPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = KiddionPage;
                break;
            case "GTAHaxPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = GTAHaxPage;
                break;
            case "BincoHaxPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = BincoHaxPage;
                break;
            case "LSCHaxPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = LSCHaxPage;
                break;
            case "YimMenuPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = YimMenuPage;
                break;
        }
    }

    /// <summary>
    /// 点击第三方辅助配置文件路径
    /// </summary>
    /// <param name="path"></param>
    private void PathClick(string path)
    {
        AudioUtil.PlayClickSound();

        switch (path)
        {
            case "KiddionConfigFolder":
                ProcessUtil.OpenPath(FileUtil.D_Kiddion_Path);
                break;
            case "GTAHaxStatFile":
                ProcessUtil.Notepad2EditTextFile(FileUtil.F_GTAHaxStat_Path);
                break;
            case "YimMenuConfigFolder":
                ProcessUtil.OpenPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/BigBaseV2/");
                break;
        }
    }

    /// <summary>
    /// Kiddion点击事件
    /// </summary>
    private void KiddionClick()
    {
        bool isRun = false;

        Task.Run(() =>
        {
            if (CheatsModel.KiddionIsRun)
            {
                // 先关闭Kiddion汉化程序
                ProcessUtil.CloseProcess("Kiddion_Chs");
                // 如果Kiddion没有运行则打开Kiddion
                if (!ProcessUtil.IsAppRun("Kiddion"))
                    ProcessUtil.OpenProcess("Kiddion", true);

                do
                {
                    // 等待Kiddion启动
                    if (ProcessUtil.IsAppRun("Kiddion"))
                    {
                        // Kiddion进程启动标志
                        isRun = true;
                        // Kiddion菜单界面显示标志
                        bool isShow = false;
                        do
                        {
                            // 拿到Kiddion进程
                            var pKiddion = Process.GetProcessesByName("Kiddion").ToList()[0];
                            // 获取Kiddion窗口句柄
                            var caption_handle = Win32.FindWindowEx(pKiddion.MainWindowHandle, IntPtr.Zero, "Static", null);

                            var length = Win32.GetWindowTextLength(caption_handle);
                            var windowName = new StringBuilder(length + 1);
                            Win32.GetWindowText(caption_handle, windowName, windowName.Capacity);

                            if (windowName.ToString() == "Kiddion's Modest Menu v0.9.4")
                            {
                                isShow = true;
                                ProcessUtil.OpenProcess("Kiddion_Chs", true);
                            }
                            else
                            {
                                isShow = false;
                            }

                            Task.Delay(100).Wait();
                        } while (!isShow);
                    }
                    else
                    {
                        isRun = false;
                    }

                    Task.Delay(250).Wait();
                } while (!isRun);
            }
            else
            {
                ProcessUtil.CloseProcess("Kiddion");
                ProcessUtil.CloseProcess("Kiddion_Chs");
            }
        });

        Task.Run(() =>
        {
            // 模拟任务超时
            Task.Delay(5000);
            isRun = true;
        });
    }

    /// <summary>
    /// GTAHax点击事件
    /// </summary>
    private void GTAHaxClick()
    {
        if (CheatsModel.GTAHaxIsRun)
            ProcessUtil.OpenProcess("GTAHax", false);
        else
            ProcessUtil.CloseProcess("GTAHax");
    }

    /// <summary>
    /// BincoHax点击事件
    /// </summary>
    private void BincoHaxClick()
    {
        if (CheatsModel.BincoHaxIsRun)
            ProcessUtil.OpenProcess("BincoHax", false);
        else
            ProcessUtil.CloseProcess("BincoHax");
    }

    /// <summary>
    /// LSCHax点击事件
    /// </summary>
    private void LSCHaxClick()
    {
        if (CheatsModel.LSCHaxIsRun)
            ProcessUtil.OpenProcess("LSCHax", false);
        else
            ProcessUtil.CloseProcess("LSCHax");
    }

    /// <summary>
    /// YimMenu点击事件
    /// </summary>
    private void YimMenuClick()
    {
        var _DLLPath = FileUtil.D_Inject_Path + "YimMenu.dll";

        if (!File.Exists(_DLLPath))
        {
            NotifierHelper.Show(NotifierType.Error, "发生异常，YimMenu菜单DLL文件不存在");
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

    /// <summary>
    /// 使用说明隐藏按钮点击事件
    /// </summary>
    private void FrameHideClick()
    {
        CheatsModel.FrameState = Visibility.Collapsed;
        CheatsModel.FrameContent = null;
    }
}
