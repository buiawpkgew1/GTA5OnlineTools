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

    private readonly ReadMeView ReadMeView = new();
    private readonly PlayerStateView PlayerStateView = new();
    private readonly WorldFunctionView WorldFunctionView = new();
    private readonly OnlineOptionView OnlineOptionView = new();
    private readonly PlayerListView PlayerListView = new();

    public ModulesView()
    {
        InitializeComponent();
        this.DataContext = this;

        // 创建菜单
        CreateMenuBar();
        // 绑定菜单切换命令
        NavigateCommand = new(Navigate);
        // 设置主页
        ContentControl_Main.Content = ReadMeView;
    }

    /// <summary>
    /// 创建导航菜单
    /// </summary>
    private void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar() { Emoji = "🍏", Title = "使用说明", NameSpace = "ReadMeView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍊", Title = "玩家属性", NameSpace = "PlayerStateView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍉", Title = "世界功能", NameSpace = "WorldFunctionView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍓", Title = "线上选项", NameSpace = "OnlineOptionView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍑", Title = "玩家列表", NameSpace = "PlayerListView" });
    }

    /// <summary>
    /// 页面导航（重复点击不会重复触发）
    /// </summary>
    /// <param name="obj"></param>
    private void Navigate(MenuBar obj)
    {
        if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
            return;

        switch (obj.NameSpace)
        {
            case "ReadMeView":
                ContentControl_Main.Content = ReadMeView;
                break;
            case "PlayerStateView":
                ContentControl_Main.Content = PlayerStateView;
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
        }
    }
}
