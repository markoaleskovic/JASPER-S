using System.Linq;

namespace Jaspers.Operations;

public class AndOperation : IOperation
{
    public bool Execute(params bool[] operands)
    {
        // AND returns true only if all operands are true
        return operands.All(x => x);
    }
}