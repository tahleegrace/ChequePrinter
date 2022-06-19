using Microsoft.AspNetCore.Mvc;

using ChequePrinter.Services;

namespace ChequePrinter.API.Controllers
{
    /// <summary>
    /// Provides functionality for printing cheques.
    /// </summary>
    [Route("api/cheques")]
    [ApiController]
    public class ChequePrintingController : ControllerBase
    {
        private readonly IChequePrintingService _chequePrintingService;

        /// <summary>
        /// Creates a new instance of ChequePrinterController.
        /// </summary>
        /// <param name="chequePrintingService">The cheque printing service.</param>
        public ChequePrintingController(IChequePrintingService chequePrintingService)
        {
            this._chequePrintingService = chequePrintingService;
        }

        /// <summary>
        /// Prints the specified value of currency in text version (e.g. 123.00 = one hundred and twenty-three dollars).
        /// </summary>
        [HttpPost("print")]
        public string Print(decimal value)
        {
            return this._chequePrintingService.Print(value);
        }
    }
}