using System;
using Nodify;

namespace Jaspers
{
    public class EditorViewModel : ObservableObject
    {
        public event Action<EditorViewModel, CircuitViewModel>? OnOpenInnerCalculator;

        public EditorViewModel? Parent { get; set; }

        public EditorViewModel()
        {
            Circuit = new CircuitViewModel();
            OpenCalculatorCommand = new DelegateCommand<CircuitViewModel>(calculator =>
            {
                OnOpenInnerCalculator?.Invoke(this, calculator);
            });
        }

        public INodifyCommand OpenCalculatorCommand { get; }

        public Guid Id { get; } = Guid.NewGuid();

        private CircuitViewModel _circuit = default!;
        public CircuitViewModel Circuit 
        {
            get => _circuit;
            set => SetProperty(ref _circuit, value);
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
