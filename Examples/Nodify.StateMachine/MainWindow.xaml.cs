using System.Windows;
using Avalonia.Input;
using MouseWheelEventArgs = Avalonia.Input.PointerWheelEventArgs;
using ModifierKeys = Avalonia.Input.KeyModifiers;

namespace Nodify.StateMachine
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Connector.EnableStickyConnections = true;
            NodifyEditor.EnableCuttingLinePreview = true;

            EditorGestures.Mappings.Connection.Disconnect.Value = MultiGesture.None;
            EditorGestures.Mappings.Editor.ZoomModifierKey = ModifierKeys.Control;

            PART_ScrollViewer.AddHandler(PointerWheelChangedEvent, ScrollViewer_MouseWheel, RoutingStrategies.Tunnel);
            PART_ScrollViewer.AddHandler(KeyDownEvent, ScrollViewer_PreviewKeyDown, RoutingStrategies.Tunnel);
        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.KeyModifiers != ModifierKeys.Shift)
                return;

            var scrollViewer = (ScrollViewer)sender;

            if (e.Delta.Length < 0)
            {
                scrollViewer.LineRight();
            }
            else
            {
                scrollViewer.LineLeft();
            }

            e.Handled = true;
        }

        private void ScrollViewer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyModifiers != ModifierKeys.Shift)
                return;

            var scrollViewer = (ScrollViewer)sender;

            if (e.Key == Key.PageUp)
            {
                scrollViewer.PageLeft();
                e.Handled = true;
            }
            else if (e.Key == Key.PageDown)
            {
                scrollViewer.PageRight();
                e.Handled = true;
            }
        }
    }
}
