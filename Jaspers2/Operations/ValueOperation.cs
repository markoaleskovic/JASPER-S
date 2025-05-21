using System;

namespace Jaspers.Operations
{
    public class ValueOperation : IOperation
    {
        private readonly Func<bool> _func;

        public ValueOperation(Func<bool> func) => _func = func;

        public bool Execute(params bool[] operands)
            => _func();
    }
}
