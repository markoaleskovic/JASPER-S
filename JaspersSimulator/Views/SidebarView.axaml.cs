using Avalonia.Controls;

namespace JaspersSimulator.Views;

public partial class SidebarView : UserControl
{
    public SidebarView()
    {
        InitializeComponent();
        DataContext = new ViewModels.SidebarViewModel();
    }
}
