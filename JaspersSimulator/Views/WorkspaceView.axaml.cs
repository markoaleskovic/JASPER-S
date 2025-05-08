using Avalonia.Controls;

namespace JaspersSimulator.Views;

public partial class WorkspaceView : UserControl
{
    public WorkspaceView()
    {
        InitializeComponent();
        DataContext = new ViewModels.WorkspaceViewModel();
    }
}
