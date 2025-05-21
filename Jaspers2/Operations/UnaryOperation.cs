using System;

namespace Jaspers.Operations
{
    public class UnaryOperation : IOperation
    {
        private readonly Func<bool, bool> _func;

        public UnaryOperation(Func<bool, bool> func) => _func = func;

        public bool Execute(params bool[] operands)
            => _func.Invoke(operands[0]);
    }
}
