using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsEngine.Models
{
    public class CalculatorModel
    {

        public double X { get; set; }
        public double Y { get; set; }
        public CalculatorOperation Operation { get; set; }

        public double GetResult()
        {
            return GetResult(X, Y, Operation);
        }

        private double GetResult(double x, double y, CalculatorOperation operation)
        {
            double result = 0d;
            switch(operation)
            {
                case CalculatorOperation.Add:
                    result = x + y;
                    break;
                case CalculatorOperation.Division:
                    result = x / y;
                    break;
                case CalculatorOperation.Multiplication:
                    result = x * y;
                    break;
                case CalculatorOperation.Subtraction:
                    result = x - y;
                    break;
            }
            return result;
        }

        public enum CalculatorOperation
        {
            Add, Subtraction, Multiplication, Division
        }
    }
}