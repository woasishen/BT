using System;

namespace BT.Compute.Bases.MathComputes
{
    public class BTMathComputeInt : BTMathCompute<int>
    {
        protected override int ComputeValue(int a, int b, BTMathComputeType computeType)
        {
            switch (computeType)
            {
                case BTMathComputeType.Addition:
                    return a + b;
                case BTMathComputeType.Subtraction:
                    return a - b;
                case BTMathComputeType.Multiplication:
                    return a * b;
                case BTMathComputeType.Division:
                    return a / b;
                default:
                    throw new ArgumentOutOfRangeException(nameof(computeType), computeType, null);
            }
        }
    }
}
