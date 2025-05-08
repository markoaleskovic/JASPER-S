using Avalonia.Controls;

namespace JaspersSimulator.Views;

public partial class TopbarView : UserControl
{
    public TopbarView()
    {
        InitializeComponent();
        DataContext = new ViewModels.TopbarViewModel();
    }
}
