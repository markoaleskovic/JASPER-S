namespace Jaspers.ViewModels.Operations
{
    public class OperationGroupViewModel : OperationViewModel
    {
        private Size _size;
        public Size GroupSize
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }
    }
}