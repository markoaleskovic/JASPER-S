using System;
using System.Collections.Generic;
using System.Linq;
using Jaspers.Operations;
using Jaspers.ViewModels.Operations;
using Nodify;

namespace Jaspers.ViewModels.Editor
{
    public class OperationsMenuViewModel : ObservableObject
    {
        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetProperty(ref _isVisible, value);
                if (!value)
                {
                    Closed?.Invoke();
                }
            }
        }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public event Action? Closed;

        public void OpenAt(Point targetLocation)
        {
            Close();
            Location = targetLocation;
            IsVisible = true;
        }

        public void Close()
        {
            IsVisible = false;
        }

        public NodifyObservableCollection<OperationInfoViewModel> AvailableOperations { get; }
        public INodifyCommand CreateOperationCommand { get; }
        private readonly CircuitViewModel _circuit;

        public OperationsMenuViewModel(CircuitViewModel circuit)
        {
            _circuit = circuit;
            List<OperationInfoViewModel> operations = new List<OperationInfoViewModel>
            {
                //new OperationInfoViewModel
                //{
                //    Type = OperationType.Graph,
                //    Title = "Operation Graph",
                //},
                new OperationInfoViewModel
                {
                    Type = OperationType.Circuit,
                    Title = "Custom Circuit"
                },
                //new OperationInfoViewModel
                //{
                //    Type = OperationType.Expression,
                //    Title = "Custom",
                //}
            };
            operations.AddRange(OperationFactory.GetOperationsInfo(typeof(OperationsContainer)));

            AvailableOperations = new NodifyObservableCollection<OperationInfoViewModel>(operations);
            CreateOperationCommand = new DelegateCommand<OperationInfoViewModel>(CreateOperation);
        }

        private void CreateOperation(OperationInfoViewModel operationInfo)
        {
            OperationViewModel op = OperationFactory.GetOperation(operationInfo);
            op.Location = Location;

            _circuit.Operations.Add(op);

            var pending = _circuit.PendingConnection;
            if (pending.IsVisible)
            {
                var connector = pending.Source.IsInput ? op.Output : op.Input.FirstOrDefault();
                if (connector != null && _circuit.CanCreateConnection(pending.Source, connector))
                {
                    _circuit.CreateConnection(pending.Source, connector);
                }
            }
            Close();
        }
    }
}
