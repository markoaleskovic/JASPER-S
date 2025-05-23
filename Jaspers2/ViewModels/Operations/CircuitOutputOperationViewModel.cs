using Jaspers.ViewModels.Editor;
using Nodify;

namespace Jaspers.ViewModels.Operations
{
    public class CircuitOutputOperationViewModel : OperationViewModel
    {
        public CircuitOutputOperationViewModel()
        {
            AddInputCommand = new RequeryCommand(
                () => Input.Add(new ConnectorViewModel
                {
                    Title = $"Output {(char)('A' + Input.Count)}",
                    IsInput = true
                }),
                () => Input.Count < 10);

            RemoveInputCommand = new RequeryCommand(
                () => Input.RemoveAt(Input.Count - 1),
                () => Input.Count > 1);

            Input.Add(new ConnectorViewModel
            {
                Title = $"Output {(char)('A' + Input.Count)}",
                IsInput = true
            });
        }

        public new NodifyObservableCollection<ConnectorViewModel> Input { get; set; } =
            new NodifyObservableCollection<ConnectorViewModel>();

        public INodifyCommand AddInputCommand { get; }
        public INodifyCommand RemoveInputCommand { get; }
    }
}
