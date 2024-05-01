IF OBJECT_ID('dbo.CalculateMonthlyTotal') IS NOT NULL
  DROP FUNCTION dbo.CalculateMonthlyTotal
GO

IF OBJECT_ID('dbo.CalculateMonthlyLoanPayment') IS NOT NULL
  DROP FUNCTION dbo.CalculateMonthlyLoanPayment
GO

IF OBJECT_ID('dbo.CalculateRow') IS NOT NULL
  DROP FUNCTION dbo.CalculateRow
GO

IF OBJECT_ID('dbo.CalculateLoanTableWithRecycle') IS NOT NULL
  DROP PROCEDURE dbo.CalculateLoanTableWithRecycle
GO

CREATE FUNCTION dbo.CalculateMonthlyTotal
(
    @Rate DECIMAL(10, 5),
    @Principal DECIMAL(10, 5),
    @Duration INT
)
RETURNS DECIMAL(10, 5)
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @MonthlyRate DECIMAL(10, 5);
    SET @MonthlyRate = (@Rate / 12);
    DECLARE @MonthlyTotal DECIMAL(10, 5);
    SET @MonthlyTotal
        = (@Principal * @MonthlyRate * POWER(@MonthlyRate + 1, @Duration))
          / (POWER(@MonthlyRate + 1, @Duration) - 1);
    RETURN (@MonthlyTotal);
END;
GO

CREATE FUNCTION dbo.CalculateMonthlyLoanPayment
(
    @Rate DECIMAL(10, 5),
    @RemainingLoan DECIMAL(10, 5),
    @MonthlyTotal DECIMAL(10, 5)
)
RETURNS DECIMAL(10, 5)
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @MonthlyLoanPayment DECIMAL(10, 5);
    SET @MonthlyLoanPayment = @MonthlyTotal - (@RemainingLoan * @Rate / 12);
    RETURN (@MonthlyLoanPayment);
END;
GO

CREATE FUNCTION dbo.CalculateRow
(
    @Rate DECIMAL(10, 5),
    @Principal DECIMAL(10, 5),
    @Duration INT,
    @MonthlyTotal DECIMAL(10, 5)
)
RETURNS TABLE
AS
RETURN
(
    SELECT @MonthlyTotal AS MonthlyPayment,
           @MonthlyTotal - dbo.CalculateMonthlyLoanPayment(@Rate, @Principal, @MonthlyTotal) AS MonthlyInterestPayment,
           dbo.CalculateMonthlyLoanPayment(@Rate, @Principal, @MonthlyTotal) AS MonthlyLoanPayment,
           @Principal - dbo.CalculateMonthlyLoanPayment(@Rate, @Principal, @MonthlyTotal) AS RemainingLoan
)
GO

CREATE PROCEDURE dbo.CalculateLoanTableWithRecycle @Principal DECIMAL(10, 2)
AS
SET NOCOUNT ON;

DECLARE @FirstPeriodRate DECIMAL(10, 3) = 0.08;
DECLARE @FirstPeriodDuration INT = 36;
DECLARE @SecondPeriodRate DECIMAL(10, 3) = 0.045;
DECLARE @SecondPeriodDuration INT = 48;

WITH MonthlyData (PaymentNumber, MonthlyPayment, MonthlyInterestPayment, MonthlyLoanPayment, RemainingLoan)
AS
(
    SELECT 1, *
    FROM dbo.CalculateRow(
		@FirstPeriodRate,
		@Principal,
		@FirstPeriodDuration,
		dbo.CalculateMonthlyTotal(@FirstPeriodRate, @Principal, @FirstPeriodDuration)
    )
    UNION ALL
    SELECT md.PaymentNumber + 1,
           md.MonthlyPayment,
           md.MonthlyPayment - dbo.CalculateMonthlyLoanPayment(@FirstPeriodRate, md.RemainingLoan, md.MonthlyPayment),
           dbo.CalculateMonthlyLoanPayment(@FirstPeriodRate, md.RemainingLoan, md.MonthlyPayment),
           CONVERT(DECIMAL(10, 5), md.RemainingLoan)
				- dbo.CalculateMonthlyLoanPayment(@FirstPeriodRate, md.RemainingLoan, md.MonthlyPayment)
    FROM MonthlyData AS md
    WHERE md.PaymentNumber < 12
    UNION ALL
    SELECT md.PaymentNumber + 1,
           dbo.CalculateMonthlyTotal(@SecondPeriodRate, md.RemainingLoan, @SecondPeriodDuration),
           dbo.CalculateMonthlyTotal(@SecondPeriodRate, md.RemainingLoan, @SecondPeriodDuration)
           - dbo.CalculateMonthlyLoanPayment(
                @SecondPeriodRate,
                md.RemainingLoan,
                dbo.CalculateMonthlyTotal(@SecondPeriodRate, md.RemainingLoan, @SecondPeriodDuration)
            ),
           dbo.CalculateMonthlyLoanPayment(
				@SecondPeriodRate,
				md.RemainingLoan,
				dbo.CalculateMonthlyTotal(@SecondPeriodRate, md.RemainingLoan, @SecondPeriodDuration)
			),
           CONVERT(DECIMAL(10, 5), md.RemainingLoan)
           - dbo.CalculateMonthlyLoanPayment(
                @SecondPeriodRate,
                md.RemainingLoan,
                dbo.CalculateMonthlyTotal(@SecondPeriodRate, md.RemainingLoan, @SecondPeriodDuration)
            )
    FROM MonthlyData AS md
    WHERE md.PaymentNumber = 12
    UNION ALL
    SELECT md.PaymentNumber + 1,
           md.MonthlyPayment,
           md.MonthlyPayment - dbo.CalculateMonthlyLoanPayment(@SecondPeriodRate, md.RemainingLoan, md.MonthlyPayment),
           dbo.CalculateMonthlyLoanPayment(@SecondPeriodRate, md.RemainingLoan, md.MonthlyPayment),
           CONVERT(DECIMAL(10, 5), md.RemainingLoan)
           - dbo.CalculateMonthlyLoanPayment(@SecondPeriodRate, md.RemainingLoan, md.MonthlyPayment)
    FROM MonthlyData AS md
    WHERE md.PaymentNumber > 12
          AND md.PaymentNumber < 60
)

SELECT * FROM MonthlyData;
GO

EXEC dbo.CalculateLoanTableWithRecycle 36000;