namespace ChequePrinter.Services
{
    /// <summary>
    /// A service that prints cheques.
    /// </summary>
    public interface IChequePrintingService
    {
        /// <summary>
        /// Prints the specified currency in text version (e.g. one hundred and twenty-three dollars).
        /// </summary>
        string Print(decimal value);
    }
}