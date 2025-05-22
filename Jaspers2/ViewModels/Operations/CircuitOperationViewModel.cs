using Jaspers.ViewModels.Editor;
using Nodify;

namespace Jaspers.ViewModels.Operations
{
    public class CircuitOperationViewModel : OperationViewModel
    {
        public CircuitViewModel InnerCircuit { get; } = new CircuitViewModel();

        private OperationViewModel InnerOutput { get; } = new OperationViewModel
        {
            Title = "Output Parameters",
            Input = { new ConnectorViewModel() },
            Location = new Point(500, 300),
            IsReadOnly = true
        };

        private CircuitInputOperationViewModel InnerInput { get; } = new CircuitInputOperationViewModel
        {
            Title = "Input Parameters",
            Location = new Point(300, 300),
            IsReadOnly = true
        };

        public CircuitOperationViewModel()
        {
            InnerCircuit.Operations.Add(InnerInput);
            InnerCircuit.Operations.Add(InnerOutput);

            Output = new ConnectorViewModel();

            InnerOutput.Input[0].ValueObservers.Add(Output);

            InnerInput.Output.ForEach(x => Input.Add(new ConnectorViewModel
            {
                Title = x.Title
            }));

            InnerInput.Output
                .WhenAdded(x => Input.Add(new ConnectorViewModel
                {
                    Title = x.Title
                }))
                .WhenRemoved(x => Input.RemoveOne(i => i.Title == x.Title));
        }

        public override void OnInputValueChanged()
        {
            for (var i = 0; i < Input.Count; i++)
            {
                InnerInput.Output[i].Value = Input[i].Value;
            }
        }
    }
}
