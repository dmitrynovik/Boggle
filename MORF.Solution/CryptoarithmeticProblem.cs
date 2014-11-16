using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace MORF.Solution
{
    /// <summary>The Cryptoarithmetic problems solution.</summary>
    /// <remarks>
    /// Currently, only addition of two numbers is supported. 
    /// An attempt to add up more than two numbers, as well as perform other arithmetic operations will throw the <see cref="NotImplementedException"/>.
    /// </remarks>
    public class CryptoarithmeticProblem
    {
        public enum Operator
        {
            None,
            Add,
            Subtract,
            Multiply,
            Divide,
        }

        private const char DontCare = '?';
        public const string AnswerPad = "----------";

        private const int DigitsBase = 10;
        private readonly char[] _letters = new char[DigitsBase];
        private char[] _distinctLetters;
        private readonly IDictionary<int, char[]> _constraints = new Dictionary<int, char[]>(DigitsBase);

        private readonly string _input;
        private string _output;

        public CryptoarithmeticProblem(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) 
                throw new ArgumentException("Cannot accept blank strings.");

            _input = input;
            Parse();
            Validate();
            Solve();
        }

        public Operator Operation { get; private set; }
        public string OperandOne { get; private set; }
        public string OperandTwo { get; private set; }
        public string OperationResult { get; private set; }

        public string Solution
        {
            get { return _output ?? ""; }
        }

        private void Solve()
        {
            Initialize();
            SetUpConstraints();
            SolveWithPermutations(0, DigitsBase - 1);
        }

        private void Initialize()
        {
            int i = 0;
            for (; i < _distinctLetters.Length; ++i)
                _letters[i] = _distinctLetters[i];

            for (; i < DigitsBase; ++i)
                _letters[i] = DontCare;
        }

        private void SolveWithPermutations(int i, int n)
        {
            if (_output != null)
                return;

            if (i == n)
            {
                CheckSolution();
                return;
            }

            for (int j = i; j <= n; j++)
            {
                if (_letters[i] != DontCare || _letters[j] != DontCare)
                {
                    Swap(i, j);
                    SolveWithPermutations(i + 1, n);
                    Swap(i, j);
                }
            }
        }

        private void Swap(int i, int j)
        {
            char tmp = _letters[i];
            _letters[i] = _letters[j];
            _letters[j] = tmp;
        }

        private bool CheckSolution()
        {
            if (OperandOne[0] == _letters[0] || OperandTwo[0] == _letters[0])
            {
                // The numbers cannot start from the digit '0':
                return false;
            }

            // check pre- arithmetical constraints (faster):
            for (int i = 0; i < _letters.Length; ++i)
            {
                var letter = _letters[i];
                if (letter != DontCare && !_constraints[i].Contains(letter))
                    return false;
            }

            // all constraints were satidfied
            // check the possible solution via numerical addition (the slowest operation):
            var op1 = ToNumber(OperandOne);
            var op2 = ToNumber(OperandTwo);
            var result = ToNumber(OperationResult);
            //Console.WriteLine("{0} + {1} = {2} ?", op1, op2, result);
            if (op1 + op2 == result)
            {
                _output = string.Join("\n", op1, "+" + op2, AnswerPad, result);
                return true;
            }
            return false;
        }

        private int ToNumber(string crypt)
        {
            var buf = new StringBuilder(crypt.Length);
            for (int i = 0; i < crypt.Length; ++i)
            {
                buf.Append(Array.IndexOf<char>(_letters, crypt[i]));
            }
            return int.Parse(buf.ToString(), CultureInfo.InvariantCulture);
        }

        private void Parse()
        {
            const int expectedLines = 4;
            string[] tokens = _input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < expectedLines) 
                throw new ArgumentException("The problem should have minimum 4 lines of text.");
            if (tokens.Length > expectedLines)
                throw new NotImplementedException("More than two operands are not currently supported.");

            OperationResult = tokens[tokens.Length - 1].Trim();
            for (int i = 0; i < tokens.Length - 2; ++i)
            {
                var current = tokens[i].Trim();
                var op = ParseOperator(current[0]);
                if (op != Operator.None)
                    Operation = op;

                for (int j = 0; j < current.Length; ++j)
                {
                    if (char.IsLetter(current[j]))
                    {
                        var crypt = current.Substring(j, current.Length - j);
                        if (i == 0)
                            OperandOne = crypt;
                        else
                            OperandTwo = crypt;
                        break;
                    }
                }
            }

            _distinctLetters = _input.Where(char.IsLetter).Distinct().ToArray();
        }

        private void Validate()
        {
            ValidateNumberOfUniqueLetters();
            switch (Operation)
            {
                case Operator.Add:
                    ValidateAddition();
                    break;
            }
        }

        private void ValidateAddition()
        {
            int longestOperand = new[] { this.OperandOne, this.OperandTwo }.Max(op => op.Length);
            if (longestOperand > OperationResult.Length)
                throw new ArgumentException("The result cannot be shorter than one of the operands.");
        }

        private void SetUpConstraints()
        {
            for (int i = 0; i < _letters.Length; ++i)
            {
                var values = new char[_distinctLetters.Length];
                Array.Copy(_distinctLetters, values, _distinctLetters.Length);
                _constraints[i] = values;
            }

            switch (Operation)
            {
                case Operator.Add:
                    SetConstraintsForAddition();
                    break;
            }
        }

        private void SetConstraintsForAddition()
        {
            // Check whether 1st digit in addition result is '1':
            int longestOperand = new[] { OperandOne, OperandTwo }.Max(op => op.Length);
            if (longestOperand + 1 == OperationResult.Length)
            {
                // The result's 1st digit must be '1' because of carry:
                var one = OperationResult[0];
                for (int i = 0; i < _letters.Length; ++i)
                {
                    if (i == 1)
                        _constraints[i] = new[] {one};
                    else
                        _constraints[i] = _constraints[i].Except(new[] {one} ).ToArray();
                }
            }

            //
            // Could set up more arithmetical constraints here (for example, seek for digits '0' and '9') to speed up the performance a bit
            //
        }

        private void ValidateNumberOfUniqueLetters()
        {
            if (_distinctLetters.Length > DigitsBase) 
                throw new ArgumentException(string.Format("The input contains more than 10 distinct letters"));
        }

        private static Operator ParseOperator(char op)
        {
            switch (op)
            {
                case '+':
                    return Operator.Add;
                case '*':
                case '/':
                case '-':
                    throw new NotImplementedException(string.Format("The operator {0} is not implemented", op));
                default:
                    return Operator.None;
            }
        }
    }
}
