using System;

namespace Jaspers.Operations
{
    public class BinaryOperation : IOperation
    {
        private readonly Func<bool, bool, bool> _func;

        public BinaryOperation(Func<bool, bool, bool> func) => _func = func;

        public bool Execute(params bool[] operands)
            => _func.Invoke(operands[0], operands[1]);
    }
}
