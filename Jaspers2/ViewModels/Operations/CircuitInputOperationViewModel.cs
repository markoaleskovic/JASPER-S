using Jaspers.ViewModels.Editor;
using Nodify;

namespace Jaspers.ViewModels.Operations
{
    public class CircuitInputOperationViewModel : OperationViewModel
    {
        public CircuitInputOperationViewModel()
        {
            AddOutputCommand = new RequeryCommand(
                () => Output.Add(new ConnectorViewModel
                {
                    Title = $"Input {(char)('A' + Output.Count)}"
                }),
                () => Output.Count < 10);

            RemoveOutputCommand = new RequeryCommand(
                () => Output.RemoveAt(Output.Count - 1),
                () => Output.Count > 1);

            Output.Add(new ConnectorViewModel
            {
                Title = $"Input {(char)('A' + Output.Count)}"
            });
        }

        public new NodifyObservableCollection<ConnectorViewModel> Output { get; set; } =
            new NodifyObservableCollection<ConnectorViewModel>();

        public INodifyCommand AddOutputCommand { get; }
        public INodifyCommand RemoveOutputCommand { get; }
    }
}
