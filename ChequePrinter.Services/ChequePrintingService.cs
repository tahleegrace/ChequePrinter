namespace ChequePrinter.Services
{
    /// <summary>
    /// The cheque printing service.
    /// </summary>
    public class ChequePrintingService : IChequePrintingService
    {
        private Dictionary<int, string> numbersBelow10 = new Dictionary<int, string>()
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

        /// <summary>
        /// Prints the specified currency in text version (e.g. one hundred and twenty-three dollars).
        /// </summary>
        public string Print(decimal value)
        {
            if (value < 0)
            {
                throw new Exception("Cannot print negative cheques");
            }

            var centsPortion = value - Math.Truncate(value);
            string centsText = PrintDecimalValue(centsPortion);

            return centsText;
        }

        private string PrintDecimalValue(decimal value)
        {
            if (value == 0)
            {
                return String.Empty;
            }

            string suffix = value == 0.01m ? "CENT" : "CENTS";
            string numberText = PrintNumberBelow100(Convert.ToInt32(Math.Truncate(value * 100)));

            return $"{numberText} {suffix}";
        }

        private string PrintNumberBelow100(int value)
        {
            if (value > 10)
            {
                return "NA";
            };

            return numbersBelow10[value];
        }
    }
}