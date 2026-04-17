using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AndroidPadSimulator.ViewModels;

namespace AndroidPadSimulator.Views;

public partial class AboutDeviceView : UserControl
{
    public AboutDeviceView()
    {
        InitializeComponent();
        DataContext = new AboutDeviceViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
