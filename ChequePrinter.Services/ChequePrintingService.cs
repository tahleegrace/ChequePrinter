namespace ChequePrinter.Services
{
    /// <summary>
    /// The cheque printing service.
    /// </summary>
    public class ChequePrintingService : IChequePrintingService
    {
        private Dictionary<long, string> digits = new Dictionary<long, string>()
        {
            { 0, "ZERO" },
            { 1, "ONE" },
            { 2, "TWO" },
            { 3, "THREE" },
            { 4, "FOUR" },
            { 5, "FIVE" },
            { 6, "SIX" },
            { 7, "SEVEN" },
            { 8, "EIGHT" },
            { 9, "NINE" }
        };

        private Dictionary<long, string> teens = new Dictionary<long, string>()
        {
            { 11, "ELEVEN" },
            { 12, "TWELVE" },
            { 13, "THIRTEEN" },
            { 14, "FOURTEEN" },
            { 15, "FIFTEEN" },
            { 16, "SIXTEEN" },
            { 17, "SEVENTEEN" },
            { 18, "EIGHTEEN" },
            { 19, "NINETEEN" }
        };

        private Dictionary<long, string> tens = new Dictionary<long, string>()
        {
            { 10, "TEN" },
            { 20, "TWENTY" },
            { 30, "THIRTY" },
            { 40, "FORTY" },
            { 50, "FIFTY" },
            { 60, "SIXTY" },
            { 70, "SEVENTY" },
            { 80, "EIGHTY" },
            { 90, "NINETY" }
        };

        // For names of numbers greater than 10 ^ 15 see https://en.wikipedia.org/wiki/Names_of_large_numbers.
        private Dictionary<long, string> powersOfTen = new Dictionary<long, string>()
        {
            { 3, "THOUSAND" },
            { 6, "MILLION" },
            { 9, "BILLION" },
            { 12, "TRILLION" },
            { 15, "QUADRILLION" }
        };

        /// <summary>
        /// Prints the specified currency in text version (e.g. one hundred and twenty-three dollars).
        /// </summary>
        public string Print(decimal value)
        {
            if (value < 0)
            {
                throw new Exception("Cannot print negative cheques");
            }

            if (value > Convert.ToDecimal(Math.Pow(10, 18)) - 1)
            {
                throw new NotSupportedException("Cannot print numbers greater than or equal to 10^18");
            }

            if (value == 0)
            {
                return "ZERO DOLLARS";
            }

            var centsPortion = value - Math.Truncate(value);
            var centsText = PrintCentsPortion(centsPortion);

            var dollarsPortion = Convert.ToInt64(Math.Truncate(value));
            var dollarsText = PrintDollarsPortion(dollarsPortion);

            var parts = new List<string>();

            if (dollarsPortion > 0)
            {
                parts.Add(dollarsText);
            }

            if (centsPortion > 0)
            {
                parts.Add(centsText);
            }

            return String.Join(" AND ", parts);
        }

        private string PrintDollarsPortion(long value)
        {
            if (value == 0)
            {
                return String.Empty;
            }

            var suffix = value == 1 ? "DOLLAR" : "DOLLARS";
            var parts = new List<string>();

            var powerOf10 = Convert.ToInt64(Math.Truncate(Math.Log10(value))); // 10,000,000 = 10 ^ 7.
            var currentExponent = (powerOf10 / 3) * 3; // 10,000,000 = 10 ^ 7. Nearest multiple of 3 is 6.

            var remaining = value;

            while (remaining > 0)
            {
                if (remaining < 1000)
                {
                    parts.Add(PrintNumberBelow1000(remaining));
                    break;
                }

                var currentMultiple = Convert.ToInt64(Math.Truncate(remaining / Math.Pow(10, currentExponent))); // 123,456,789 / 10 ^ 6 = 123.
                parts.Add(PrintNumberBelow1000(currentMultiple));
                parts.Add(powersOfTen[currentExponent]);

                remaining -= Convert.ToInt64(currentMultiple * Math.Pow(10, currentExponent)); // 123,456,789 - 10 ^ 6 = 456,789
                currentExponent -= 3;
            }

            var numberText = String.Join(" ", parts);

            return $"{numberText} {suffix}";
        }

        private string PrintCentsPortion(decimal value)
        {
            if (value == 0)
            {
                return String.Empty;
            }

            var suffix = value == 0.01m ? "CENT" : "CENTS";
            var numberText = PrintNumberBelow100(Convert.ToInt64(Math.Truncate(value * 100)));

            return $"{numberText} {suffix}";
        }

        private string PrintNumberBelow1000(long value)
        {
            if (value == 0)
            {
                return String.Empty;
            }

            // For example - 987. Non-hundreds portion will be 87. Multiples of 100 will be 9.
            var parts = new List<string>();

            var nonHundredsPortion = value % 100;
            var multiplesOf100 = value / 100;

            if (multiplesOf100 > 0)
            {
                parts.Add($"{ digits[multiplesOf100] } HUNDRED");
            }

            if (nonHundredsPortion > 0)
            {
                parts.Add(PrintNumberBelow100(nonHundredsPortion));
            }

            return String.Join(" AND ", parts);
        }

        private string PrintNumberBelow100(long value)
        {
            if (value >= 11 && value <= 19)
            {
                return teens[value];
            }

            // For example - 45. Ones portion will be 5, tens portion will be 40.
            var onesPortion = value % 10;
            var tensPortion = value - onesPortion;

            var parts = new List<string>();

            if (tensPortion > 0)
            {
                parts.Add(tens[tensPortion]);
            }

            if (onesPortion > 0)
            {
                parts.Add(digits[onesPortion]);
            }

            return String.Join('-', parts);
        }
    }
}