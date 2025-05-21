using System;
using System.Collections.Generic;
using System.Linq;
using Nodify;
using StringMath;

namespace Jaspers
{
    public class ExpressionOperationViewModel : OperationViewModel
    {
        private MathExpr? _expr;
        private string? _expression;

        public string? Expression
        {
            get => _expression;
            set => SetProperty(ref _expression, value)
                .Then(GenerateInput);
        }

        private void GenerateInput()
        {
            try
            {
                _expr = Expression!.ToMathExpr();
                ConnectorViewModel[]? toRemove = Input.Where(i => !_expr.LocalVariables.Contains(i.Title)).ToArray();
                toRemove.ForEach(i => Input.Remove(i));
                HashSet<string> existingVars = Input.Select(s => s.Title).Where(s => s != null).ToHashSet()!;

                foreach (string variable in _expr.LocalVariables.Except(existingVars))
                {
                    Input.Add(new ConnectorViewModel
                    {
                        Title = variable
                    });
                }

                OnInputValueChanged();
            }
            catch
            {

            }
        }

        public override void OnInputValueChanged()
        {
            if (Output != null && _expr != null)
            {
                try
                {
                    // Build a dictionary of variable values
                    var values = Input.ToDictionary(i => i.Title!, i => i.Value);

                    // Evaluate the expression (very basic, supports AND, OR, NOT)
                    bool result = EvaluateBooleanExpression(Expression, values);
                    Output.Value = result;
                }
                catch
                {

                }
            }
        }

        private bool EvaluateBooleanExpression(string expr, Dictionary<string, bool> values)
        {
            // Replace variable names with their values
            foreach (var kvp in values)
            {
                expr = expr.Replace(kvp.Key, kvp.Value ? "true" : "false");
            }

            // Use C# DataTable or a simple parser for evaluation
            // For safety, here is a minimal parser for AND/OR/NOT/parentheses
            return SimpleBooleanParser.Evaluate(expr);
        }

        public static class SimpleBooleanParser
        {
            public static bool Evaluate(string expr)
            {
                // Remove spaces
                expr = expr.Replace(" ", "");

                // Handle parentheses recursively
                while (expr.Contains("("))
                {
                    int open = expr.LastIndexOf('(');
                    int close = expr.IndexOf(')', open);
                    if (close == -1) throw new Exception("Mismatched parentheses");
                    var inner = expr.Substring(open + 1, close - open - 1);
                    var innerResult = Evaluate(inner);
                    expr = expr.Substring(0, open) + (innerResult ? "true" : "false") + expr.Substring(close + 1);
                }

                // Handle NOT
                while (expr.Contains("!true") || expr.Contains("!false"))
                {
                    expr = expr.Replace("!true", "false").Replace("!false", "true");
                }

                // Handle AND
                while (expr.Contains("&&"))
                {
                    var parts = expr.Split(new[] { "&&" }, 2, StringSplitOptions.None);
                    bool left = Evaluate(parts[0]);
                    bool right = Evaluate(parts[1]);
                    return left && right;
                }

                // Handle OR
                while (expr.Contains("||"))
                {
                    var parts = expr.Split(new[] { "||" }, 2, StringSplitOptions.None);
                    bool left = Evaluate(parts[0]);
                    bool right = Evaluate(parts[1]);
                    return left || right;
                }

                // Final value
                if (expr == "true") return true;
                if (expr == "false") return false;
                throw new Exception("Invalid expression");
            }
        }
    }
}
