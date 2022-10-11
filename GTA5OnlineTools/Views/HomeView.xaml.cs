using CommunityToolkit.Mvvm.Messaging;

namespace GTA5OnlineTools.Views;

/// <summary>
/// HomeView.xaml 的交互逻辑
/// </summary>
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<string, string>(this, "Notice", (s, e) =>
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                TextBox_Notice.Text = e;
            });
        });

        WeakReferenceMessenger.Default.Register<string, string>(this, "Change", (s, e) =>
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                TextBox_Change.Text = e;
            });
        });
    }
}
