namespace Jaspers.Operations
{
    public interface IOperation
    {
        bool Execute(params bool[] operands);
    }
}
