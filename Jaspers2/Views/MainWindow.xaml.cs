using Nodify;

namespace Jaspers.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EditorGestures.Mappings.Editor.Cutting.Value = MultiGesture.None;
        }
    }
}
