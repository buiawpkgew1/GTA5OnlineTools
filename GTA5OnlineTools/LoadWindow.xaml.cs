using GTA5OnlineTools.Models;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Common.Helper;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

using Chinese;

namespace GTA5OnlineTools;

/// <summary>
/// LoadWindow.xaml 的交互逻辑
/// </summary>
public partial class LoadWindow
{
    /// <summary>
    /// Load数据模型
    /// </summary>
    public LoadModel LoadModel { get; set; } = new();

    public LoadWindow()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    private void Window_Load_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            try
            {
                LoadModel.LoadState = "正在初始化工具，请稍后...";

                LoggerHelper.Info("开始初始化程序...");
                LoggerHelper.Info($"当前程序版本号 {CoreUtil.ClientVersion}");
                LoggerHelper.Info($"当前程序最后编译时间 {CoreUtil.ClientBuildTime}");

                // 客户端程序版本号
                LoadModel.VersionInfo = CoreUtil.ClientVersion.ToString();
                // 最后编译时间
                LoadModel.BuildDate = CoreUtil.ClientBuildTime;

                // 关闭第三方进程
                ProcessUtil.CloseThirdProcess();

                // 检测GTA5是否启动
                if (!ProcessUtil.IsGTA5Run())
                {
                    LoadModel.LoadState = $"未发现《GTA5》游戏进程！程序即将关闭";
                    LoggerHelper.Error("未发现《GTA5》进程");
                    Task.Delay(2000).Wait();

                    this.Dispatcher.Invoke(() =>
                    {
                        Application.Current.Shutdown();
                    });
                    return;
                }

                // 初始化GTA5内存模块
                if (!GTA5Mem.Initialize())
                {
                    LoadModel.LoadState = "《GTA5》内存模块初始化失败！程序即将关闭";
                    LoggerHelper.Error("《GTA5》内存模块初始化失败");
                    Task.Delay(2000).Wait();

                    this.Dispatcher.Invoke(() =>
                    {
                        Application.Current.Shutdown();
                    });
                    return;
                }
                else
                {
                    LoadModel.LoadState = "《GTA5》内存模块初始化成功";
                    LoggerHelper.Info("《GTA5》内存模块初始化成功");

                    General.WorldPTR = GTA5Mem.FindPattern(Offsets.Mask.WorldMask);
                    General.WorldPTR = GTA5Mem.Rip_37(General.WorldPTR);

                    General.BlipPTR = GTA5Mem.FindPattern(Offsets.Mask.BlipMask);
                    General.BlipPTR = GTA5Mem.Rip_37(General.BlipPTR);

                    General.GlobalPTR = GTA5Mem.FindPattern(Offsets.Mask.GlobalMask);
                    General.GlobalPTR = GTA5Mem.Rip_37(General.GlobalPTR);

                    General.PlayerChatterNamePTR = GTA5Mem.FindPattern(Offsets.Mask.PlayerchatterNameMask);
                    General.PlayerChatterNamePTR = GTA5Mem.Rip_37(General.PlayerChatterNamePTR);

                    General.PlayerExternalDisplayNamePTR = GTA5Mem.FindPattern(Offsets.Mask.PlayerExternalDisplayNameMask);
                    General.PlayerExternalDisplayNamePTR = GTA5Mem.Rip_37(General.PlayerExternalDisplayNamePTR);

                    General.NetworkPlayerMgrPTR = GTA5Mem.FindPattern(Offsets.Mask.NetworkPlayerMgrMask);
                    General.NetworkPlayerMgrPTR = GTA5Mem.Rip_37(General.NetworkPlayerMgrPTR);

                    General.ReplayInterfacePTR = GTA5Mem.FindPattern(Offsets.Mask.ReplayInterfaceMask);
                    General.ReplayInterfacePTR = GTA5Mem.Rip_37(General.ReplayInterfacePTR);

                    General.WeatherPTR = GTA5Mem.FindPattern(Offsets.Mask.WeatherMask);
                    General.WeatherPTR = GTA5Mem.Rip_6A(General.WeatherPTR);

                    General.UnkModelPTR = GTA5Mem.FindPattern(Offsets.Mask.UnkModelMask);
                    General.UnkModelPTR = GTA5Mem.Rip_37(General.UnkModelPTR);

                    General.PickupDataPTR = GTA5Mem.FindPattern(Offsets.Mask.PickupDataMask);
                    General.PickupDataPTR = GTA5Mem.Rip_37(General.PickupDataPTR);

                    General.ViewPortPTR = GTA5Mem.FindPattern(Offsets.Mask.ViewPortMask);
                    General.ViewPortPTR = GTA5Mem.Rip_37(General.ViewPortPTR);

                    General.AimingPedPTR = GTA5Mem.FindPattern(Offsets.Mask.AimingPedMask);
                    General.AimingPedPTR = GTA5Mem.Rip_37(General.AimingPedPTR);

                    General.CCameraPTR = GTA5Mem.FindPattern(Offsets.Mask.CCameraMask);
                    General.CCameraPTR = GTA5Mem.Rip_37(General.CCameraPTR);

                    General.UnkPTR = GTA5Mem.FindPattern(Offsets.Mask.UnkMask);
                    General.UnkPTR = GTA5Mem.Rip_37(General.UnkPTR);

                    General.LocalScriptsPTR = GTA5Mem.FindPattern(Offsets.Mask.LocalScriptsMask);
                    General.LocalScriptsPTR = GTA5Mem.Rip_37(General.LocalScriptsPTR);
                }

                /////////////////////////////////////////////////////////////////////

                LoadModel.LoadState = "正在初始化配置文件...";
                LoggerHelper.Info("正在初始化配置文件...");

                // 创建指定文件夹，用于释放必要文件和更新软件（如果已存在则不会创建）
                Directory.CreateDirectory(FileUtil.D_Cache_Path);
                Directory.CreateDirectory(FileUtil.D_Config_Path);
                Directory.CreateDirectory(FileUtil.D_Kiddion_Path);
                Directory.CreateDirectory(FileUtil.D_KiddionScripts_Path);
                Directory.CreateDirectory(FileUtil.D_Inject_Path);
                Directory.CreateDirectory(FileUtil.D_Log_Path);

                // 清空缓存文件夹
                FileUtil.DelectDir(FileUtil.D_Cache_Path);

                // 释放必要文件
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "Kiddion.exe", FileUtil.D_Kiddion_Path + "Kiddion.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "Kiddion_Chs.exe", FileUtil.D_Kiddion_Path + "Kiddion_Chs.exe");

                // 释放前先判断，防止覆盖配置文件
                if (!File.Exists(FileUtil.D_Kiddion_Path + "config.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "config.json", FileUtil.D_Kiddion_Path + "config.json");
                if (!File.Exists(FileUtil.D_Kiddion_Path + "themes.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "themes.json", FileUtil.D_Kiddion_Path + "themes.json");
                if (!File.Exists(FileUtil.D_Kiddion_Path + "teleports.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "teleports.json", FileUtil.D_Kiddion_Path + "teleports.json");
                if (!File.Exists(FileUtil.D_Kiddion_Path + "vehicles.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "vehicles.json", FileUtil.D_Kiddion_Path + "vehicles.json");

                // Kiddion Lua脚本
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "scripts.Readme.api", FileUtil.D_KiddionScripts_Path + "Readme.api");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "GTAHax.exe", FileUtil.D_Cache_Path + "GTAHax.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "stat.txt", FileUtil.D_Cache_Path + "stat.txt");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "BincoHax.exe", FileUtil.D_Cache_Path + "BincoHax.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "LSCHax.exe", FileUtil.D_Cache_Path + "LSCHax.exe");

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "dControl.exe", FileUtil.D_Cache_Path + "dControl.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "dControl.ini", FileUtil.D_Cache_Path + "dControl.ini");

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "GetKidTxt.exe", FileUtil.D_Cache_Path + "GetKidTxt.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "Notepad2.exe", FileUtil.D_Cache_Path + "Notepad2.exe");

                // 判断DLL文件是否存在以及是否被占用
                if (!File.Exists(FileUtil.D_Inject_Path + "YimMenu.dll"))
                {
                    FileUtil.ExtractResFile(FileUtil.Resource_Inject_Path + "YimMenu.dll", FileUtil.D_Inject_Path + "YimMenu.dll");
                }
                else
                {
                    if (!FileUtil.IsOccupied(FileUtil.D_Inject_Path + "YimMenu.dll"))
                        FileUtil.ExtractResFile(FileUtil.Resource_Inject_Path + "YimMenu.dll", FileUtil.D_Inject_Path + "YimMenu.dll");
                }

                if (!File.Exists(FileUtil.D_Inject_Path + "BlcokMsg.dll"))
                {
                    FileUtil.ExtractResFile(FileUtil.Resource_Inject_Path + "BlcokMsg.dll", FileUtil.D_Inject_Path + "BlcokMsg.dll");
                }
                else
                {
                    if (!FileUtil.IsOccupied(FileUtil.D_Inject_Path + "BlcokMsg.dll"))
                        FileUtil.ExtractResFile(FileUtil.Resource_Inject_Path + "BlcokMsg.dll", FileUtil.D_Inject_Path + "BlcokMsg.dll");
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                // 提前预加载转换字库
                ChineseConverter.ToTraditional("免费，跨平台，开源！");
                LoggerHelper.Info("简繁翻译库初始化成功");

                this.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    // 转移主程序控制权
                    Application.Current.MainWindow = mainWindow;
                    // 显示主窗口
                    mainWindow.Show();
                    // 关闭初始化窗口
                    this.Close();
                });
            }
            catch (Exception ex)
            {
                LoadModel.LoadState = $"初始化错误，发生了未知异常！程序即将关闭\n{ex.Message}";
                LoggerHelper.Error("初始化错误，发生了未知异常", ex);

                Task.Delay(2000).Wait();
                this.Dispatcher.Invoke(() =>
                {
                    Application.Current.Shutdown();
                });
            }
        });
    }
}
