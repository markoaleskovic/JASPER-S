using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Jaspers.ViewModels.Editor;
using Nodify;

namespace Jaspers.ViewModels.Operations
{
    public class CircuitOperationViewModel : OperationViewModel
    {
        public CircuitViewModel InnerCircuit { get; } = new CircuitViewModel();

        public NodifyObservableCollection<ConnectorViewModel> Outputs
        {
            get
            {
                var list = new NodifyObservableCollection<ConnectorViewModel>();
                if (Output != null)
                {
                    list.Add(Output);
                }
                if (AdditionalOutputs.Count != 0)
                {
                    list.AddRange(AdditionalOutputs);
                }
                return list;
            }
        }

        private CircuitOutputOperationViewModel InnerOutput { get; } = new CircuitOutputOperationViewModel
        {
            Title = "Output Parameters",
            Location = new Point(600, 300),
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

            //OUTPUTS
            Output = new ConnectorViewModel();

            InnerOutput.Input[0].ValueObservers.Add(Output);

            if (InnerOutput.Input.Count() > 1)
            {
                for (int i = 0; i < InnerOutput.Input.Count(); i++)
                {
                    InnerOutput.Input[i + 1].ValueObservers.Add(AdditionalOutputs[i]);
                }

                InnerOutput.Input.ForEach(x => AdditionalOutputs.Add(new ConnectorViewModel
                {
                    Title = x.Title,
                }));
            }

            InnerOutput.Input
                .WhenAdded(x =>
                {
                    var c = new ConnectorViewModel
                    {
                        Title = x.Title,
                    };
                    x.ValueObservers.Add(c);
                    AdditionalOutputs.Add(c);
                })
                .WhenRemoved(x =>
                {
                    x.ValueObservers.Remove(AdditionalOutputs.FirstOrDefault(i => i.Title == x.Title));
                    AdditionalOutputs.RemoveOne(i => i.Title == x.Title);
                });


            //INPUTS
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
