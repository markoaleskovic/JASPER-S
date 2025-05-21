using System;
using System.Linq;

namespace Jaspers.Operations
{
    public static class OperationsContainer
    {
        //[Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        //public static double Add(params double[] operands)
        //    => operands.Sum();

        //[Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        //public static double Multiply(params double[] operands)
        //    => operands.Aggregate((x, y) => x * y);

        //public static double Divide(double a, double b)
        //    => a / b;

        //public static double Subtract(double a, double b)
        //    => a - b;

        //public static double Pow(double value, double exp)
        //    => (double)Math.Pow((double)value, (double)exp);

        //[Operation(GenerateInputNames = false)]
        //public static double Abs(double value)
        //    => Math.Abs(value);

        //public static double PI()
        //    => (double)Math.PI;

        [Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        public static bool AND(params bool[] operands)
            => operands.All(x => x);
        [Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        public static bool OR(params bool[] operands)
            => operands.Any(x => x);

        [Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        public static bool XOR(params bool[] operands)
            => operands.Aggregate(false, (x, y) => x ^ y);

        [Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        public static bool NOR(params bool[] operands)
            => !operands.Any(x => x);
        [Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        public static bool NAND(params bool[] operands)
            => !operands.All(x => x);
        [Operation(MinInput = 2, MaxInput = 10, GenerateInputNames = false)]
        public static bool XNOR(params bool[] operands)
            => !operands.Aggregate(false, (x, y) => x ^ y);

        [Operation(GenerateInputNames = false)]
        public static bool NOT(bool value)
            => !value;

        [Operation(GenerateInputNames = false)]
        public static bool Var(bool value)
            => value;
    }

    public sealed class OperationAttribute : Attribute
    {
        public uint MaxInput { get; set; }
        public uint MinInput { get; set; }
        public bool GenerateInputNames { get; set; }
    }
}
