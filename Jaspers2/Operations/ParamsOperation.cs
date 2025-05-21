using System;

namespace Jaspers.Operations
{
    public class ParamsOperation : IOperation
    {
        private readonly Func<bool[], bool> _func;

        public ParamsOperation(Func<bool[], bool> func) => _func = func;

        public bool Execute(params bool[] operands)
            => _func.Invoke(operands);
    }
}
