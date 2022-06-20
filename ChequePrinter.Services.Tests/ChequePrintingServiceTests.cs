using System;
using Xunit;

namespace ChequePrinter.Services.Tests;

public class ChequePrintingServiceTests
{
    [Theory]
    [InlineData(-0.01, "NEGATIVE ONE CENT")]
    [InlineData(-0.45, "NEGATIVE FORTY-FIVE CENTS")]
    [InlineData(-0.002, "NEGATIVE TWO TENTHS OF A CENT")]
    [InlineData(-0.022, "NEGATIVE TWO CENTS AND TWO TENTHS OF A CENT")]
    [InlineData(-1.00, "NEGATIVE ONE DOLLAR")]
    [InlineData(-1.10, "NEGATIVE ONE DOLLAR AND TEN CENTS")]
    [InlineData(-123.45, "NEGATIVE ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS")]

    [InlineData(0.00, "ZERO DOLLARS")]
    [InlineData(0.01, "ONE CENT")]
    [InlineData(0.04, "FOUR CENTS")]
    [InlineData(0.45, "FORTY-FIVE CENTS")]
    [InlineData(0.0001, "ZERO DOLLARS")]
    [InlineData(0.0011, "ONE TENTH OF A CENT")]
    [InlineData(0.001, "ONE TENTH OF A CENT")]
    [InlineData(0.002, "TWO TENTHS OF A CENT")]
    [InlineData(0.011, "ONE CENT AND ONE TENTH OF A CENT")]
    [InlineData(0.012, "ONE CENT AND TWO TENTHS OF A CENT")]
    [InlineData(0.021, "TWO CENTS AND ONE TENTH OF A CENT")]
    [InlineData(0.022, "TWO CENTS AND TWO TENTHS OF A CENT")]

    [InlineData(1.00, "ONE DOLLAR")]
    [InlineData(1.01, "ONE DOLLAR AND ONE CENT")]
    [InlineData(1.02, "ONE DOLLAR AND TWO CENTS")]
    [InlineData(1.10, "ONE DOLLAR AND TEN CENTS")]
    [InlineData(1.11, "ONE DOLLAR AND ELEVEN CENTS")]
    [InlineData(1.12, "ONE DOLLAR AND TWELVE CENTS")]
    [InlineData(1.15, "ONE DOLLAR AND FIFTEEN CENTS")]
    [InlineData(1.20, "ONE DOLLAR AND TWENTY CENTS")]
    [InlineData(1.23, "ONE DOLLAR AND TWENTY-THREE CENTS")]
    [InlineData(1.001, "ONE DOLLAR AND ONE TENTH OF A CENT")]
    [InlineData(1.002, "ONE DOLLAR AND TWO TENTHS OF A CENT")]
    [InlineData(1.011, "ONE DOLLAR AND ONE CENT AND ONE TENTH OF A CENT")]
    [InlineData(1.012, "ONE DOLLAR AND ONE CENT AND TWO TENTHS OF A CENT")]
    [InlineData(1.021, "ONE DOLLAR AND TWO CENTS AND ONE TENTH OF A CENT")]
    [InlineData(1.022, "ONE DOLLAR AND TWO CENTS AND TWO TENTHS OF A CENT")]

    [InlineData(2.00, "TWO DOLLARS")]
    [InlineData(2.01, "TWO DOLLARS AND ONE CENT")]
    [InlineData(2.23, "TWO DOLLARS AND TWENTY-THREE CENTS")]
    [InlineData(2.001, "TWO DOLLARS AND ONE TENTH OF A CENT")]
    [InlineData(2.002, "TWO DOLLARS AND TWO TENTHS OF A CENT")]
    [InlineData(2.011, "TWO DOLLARS AND ONE CENT AND ONE TENTH OF A CENT")]
    [InlineData(2.012, "TWO DOLLARS AND ONE CENT AND TWO TENTHS OF A CENT")]
    [InlineData(2.021, "TWO DOLLARS AND TWO CENTS AND ONE TENTH OF A CENT")]
    [InlineData(2.022, "TWO DOLLARS AND TWO CENTS AND TWO TENTHS OF A CENT")]

    [InlineData(3.00, "THREE DOLLARS")]
    [InlineData(4.00, "FOUR DOLLARS")]
    [InlineData(5.00, "FIVE DOLLARS")]
    [InlineData(6.00, "SIX DOLLARS")]
    [InlineData(7.00, "SEVEN DOLLARS")]
    [InlineData(8.00, "EIGHT DOLLARS")]
    [InlineData(9.00, "NINE DOLLARS")]

    [InlineData(10.00, "TEN DOLLARS")]
    [InlineData(11.00, "ELEVEN DOLLARS")]
    [InlineData(12.00, "TWELVE DOLLARS")]
    [InlineData(13.00, "THIRTEEN DOLLARS")]
    [InlineData(14.00, "FOURTEEN DOLLARS")]
    [InlineData(15.00, "FIFTEEN DOLLARS")]
    [InlineData(16.00, "SIXTEEN DOLLARS")]
    [InlineData(17.00, "SEVENTEEN DOLLARS")]
    [InlineData(18.00, "EIGHTEEN DOLLARS")]
    [InlineData(19.00, "NINETEEN DOLLARS")]

    [InlineData(20.00, "TWENTY DOLLARS")]
    [InlineData(23.00, "TWENTY-THREE DOLLARS")]

    [InlineData(30.00, "THIRTY DOLLARS")]
    [InlineData(40.00, "FORTY DOLLARS")]
    [InlineData(50.00, "FIFTY DOLLARS")]
    [InlineData(60.00, "SIXTY DOLLARS")]
    [InlineData(70.00, "SEVENTY DOLLARS")]
    [InlineData(80.00, "EIGHTY DOLLARS")]
    [InlineData(90.00, "NINETY DOLLARS")]
    [InlineData(90.45, "NINETY DOLLARS AND FORTY-FIVE CENTS")]

    [InlineData(100.00, "ONE HUNDRED DOLLARS")]
    [InlineData(101.00, "ONE HUNDRED AND ONE DOLLARS")]
    [InlineData(102.00, "ONE HUNDRED AND TWO DOLLARS")]
    [InlineData(110.00, "ONE HUNDRED AND TEN DOLLARS")]
    [InlineData(111.00, "ONE HUNDRED AND ELEVEN DOLLARS")]
    [InlineData(115.00, "ONE HUNDRED AND FIFTEEN DOLLARS")]
    [InlineData(120.00, "ONE HUNDRED AND TWENTY DOLLARS")]
    [InlineData(123.00, "ONE HUNDRED AND TWENTY-THREE DOLLARS")]
    [InlineData(123.45, "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS")]

    [InlineData(1_000.00, "ONE THOUSAND DOLLARS")]
    [InlineData(1_100.00, "ONE THOUSAND ONE HUNDRED DOLLARS")]
    [InlineData(1_123.00, "ONE THOUSAND ONE HUNDRED AND TWENTY-THREE DOLLARS")]
    [InlineData(1_123.45, "ONE THOUSAND ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS")]

    [InlineData(10_000, "TEN THOUSAND DOLLARS")]
    [InlineData(10_100, "TEN THOUSAND ONE HUNDRED DOLLARS")]
    [InlineData(15_000, "FIFTEEN THOUSAND DOLLARS")]
    [InlineData(15_123, "FIFTEEN THOUSAND ONE HUNDRED AND TWENTY-THREE DOLLARS")]

    [InlineData(100_000, "ONE HUNDRED THOUSAND DOLLARS")]
    [InlineData(100_123, "ONE HUNDRED THOUSAND ONE HUNDRED AND TWENTY-THREE DOLLARS")]
    [InlineData(123_000, "ONE HUNDRED AND TWENTY-THREE THOUSAND DOLLARS")]
    [InlineData(123_456, "ONE HUNDRED AND TWENTY-THREE THOUSAND FOUR HUNDRED AND FIFTY-SIX DOLLARS")]

    [InlineData(1_000_000.00, "ONE MILLION DOLLARS")]
    [InlineData(1_100_000.00, "ONE MILLION ONE HUNDRED THOUSAND DOLLARS")]
    [InlineData(1_123_000.00, "ONE MILLION ONE HUNDRED AND TWENTY-THREE THOUSAND DOLLARS")]   

    [InlineData(10_000_000.00, "TEN MILLION DOLLARS")]
    [InlineData(10_010_234.00, "TEN MILLION TEN THOUSAND TWO HUNDRED AND THIRTY-FOUR DOLLARS")]

    [InlineData(123_000_000.00, "ONE HUNDRED AND TWENTY-THREE MILLION DOLLARS")]
    [InlineData(123_456_000.00, "ONE HUNDRED AND TWENTY-THREE MILLION FOUR HUNDRED AND FIFTY-SIX THOUSAND DOLLARS")]
    [InlineData(123_456_789.00, "ONE HUNDRED AND TWENTY-THREE MILLION FOUR HUNDRED AND FIFTY-SIX THOUSAND SEVEN HUNDRED AND EIGHTY-NINE DOLLARS")]

    [InlineData(1_000_000_000.00, "ONE BILLION DOLLARS")]

    [InlineData(1_000_000_000_000.00, "ONE TRILLION DOLLARS")]

    [InlineData(1_000_000_000_000_000.00, "ONE QUADRILLION DOLLARS")]
    public void TestChequePrinterReturnsCorrectTextForANumber(decimal number, string expectedResult)
    {
        var chequePrintingService = new ChequePrintingService();
        var result = chequePrintingService.Print(number);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TestChequePrinterThrowsExceptionWhenNumberIs10ToThePowerOf18()
    {
        var chequePrintingService = new ChequePrintingService();
        var exception = Assert.Throws<NotSupportedException>(() => chequePrintingService.Print(Convert.ToDecimal(Math.Pow(10, 18))));

        Assert.Equal("Cannot print numbers greater than or equal to 10^18", exception.Message);
    }
}