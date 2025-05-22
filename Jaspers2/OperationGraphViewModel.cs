namespace Jaspers
{
    public class OperationGraphViewModel : CircuitOperationViewModel
    {
        private Size _size;
        public Size DesiredSize
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }
        
        private Size _prevSize;

        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (SetProperty(ref _isExpanded, value))
                {
                    if (_isExpanded)
                    {
                        DesiredSize = _prevSize;
                    }
                    else
                    {
                        _prevSize = Size;
                        // Fit content
                        DesiredSize = new Size(0,0);
                    }
                }
            }
        }

        public OperationGraphViewModel()
        {
            InnerCircuit.Operations[0].Location = new Point(50, 50);
            InnerCircuit.Operations[1].Location = new Point(200, 50);
        }
    }
}