using GTA5OnlineTools.Common.Utils;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// AboutView.xaml 的交互逻辑
/// </summary>
public partial class AboutView : UserControl
{
    public RelayCommand<string> HyperlinkClickCommand { get; private set; }

    public AboutView()
    {
        InitializeComponent();
        this.DataContext = this;

        HyperlinkClickCommand = new(HyperlinkClick);
    }

    private void HyperlinkClick(string url)
    {
        ProcessUtil.OpenPath(url);
    }
}
