using GTA5OnlineTools.Common.Data;
using GTA5OnlineTools.Views.Modules;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// ModulesView.xaml 的交互逻辑
/// </summary>
public partial class ModulesView : UserControl
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public List<MenuBar> MenuBars { get; set; } = new();
    /// <summary>
    /// 导航命令
    /// </summary>
    public RelayCommand<MenuBar> NavigateCommand { get; private set; }

    private readonly SelfStateView SelfStateView = new();
    private readonly WorldFunctionView WorldFunctionView = new();
    private readonly OnlineOptionView OnlineOptionView = new();
    private readonly PlayerListView PlayerListView = new();
    private readonly SpawnVehicleView SpawnVehicleView = new();
    private readonly SpawnWeaponView SpawnWeaponView = new();
    private readonly ExternalOverlayView ExternalOverlayView = new();
    private readonly SessionChatView SessionChatView = new();
    private readonly JobHelperView JobHelperView = new();
    private readonly OutfitsEditView OutfitsEditView = new();
    private readonly StatScriptsView StatScriptsView = new();
    private readonly HeistCutView HeistCutView = new();

    public ModulesView()
    {
        InitializeComponent();
        this.DataContext = this;

        // 创建菜单
        CreateMenuBar();
        // 绑定菜单切换命令
        NavigateCommand = new(Navigate);
        // 设置主页
        ContentControl_Main.Content = SelfStateView;
    }

    /// <summary>
    /// 创建导航菜单
    /// </summary>
    private void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar() { Emoji = "🍎", Title = "自身属性", NameSpace = "SelfStateView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍐", Title = "世界功能", NameSpace = "WorldFunctionView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍋", Title = "线上选项", NameSpace = "OnlineOptionView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍉", Title = "玩家列表", NameSpace = "PlayerListView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍇", Title = "线上载具", NameSpace = "SpawnVehicleView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍓", Title = "线上武器", NameSpace = "SpawnWeaponView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍈", Title = "外部ESP", NameSpace = "ExternalOverlayView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍑", Title = "战局聊天", NameSpace = "SessionChatView" });
        MenuBars.Add(new MenuBar() { Emoji = "🥭", Title = "任务帮手", NameSpace = "JobHelperView" });
        MenuBars.Add(new MenuBar() { Emoji = "🚗", Title = "服装编辑", NameSpace = "OutfitsEditView" });
        MenuBars.Add(new MenuBar() { Emoji = "🚙", Title = "预设脚本", NameSpace = "StatScriptsView" });
        MenuBars.Add(new MenuBar() { Emoji = "🚌", Title = "抢劫分红", NameSpace = "HeistCutView" });
    }

    /// <summary>
    /// 页面导航（重复点击不会重复触发）
    /// </summary>
    /// <param name="menu"></param>
    private void Navigate(MenuBar menu)
    {
        if (menu == null || string.IsNullOrEmpty(menu.NameSpace))
            return;

        switch (menu.NameSpace)
        {
            case "SelfStateView":
                ContentControl_Main.Content = SelfStateView;
                break;
            case "WorldFunctionView":
                ContentControl_Main.Content = WorldFunctionView;
                break;
            case "OnlineOptionView":
                ContentControl_Main.Content = OnlineOptionView;
                break;
            case "PlayerListView":
                ContentControl_Main.Content = PlayerListView;
                break;
            case "SpawnVehicleView":
                ContentControl_Main.Content = SpawnVehicleView;
                break;
            case "SpawnWeaponView":
                ContentControl_Main.Content = SpawnWeaponView;
                break;
            case "ExternalOverlayView":
                ContentControl_Main.Content = ExternalOverlayView;
                break;
            case "SessionChatView":
                ContentControl_Main.Content = SessionChatView;
                break;
            case "JobHelperView":
                ContentControl_Main.Content = JobHelperView;
                break;
            case "OutfitsEditView":
                ContentControl_Main.Content = OutfitsEditView;
                break;
            case "StatScriptsView":
                ContentControl_Main.Content = StatScriptsView;
                break;
            case "HeistCutView":
                ContentControl_Main.Content = HeistCutView;
                break;
        }
    }
}
