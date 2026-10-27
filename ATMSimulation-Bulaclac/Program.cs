// Authentication & Account Status
int correctPin = 4821;
bool isCardBlocked = false;
bool isInternationalCard = true;
bool isVipCustomer = true;

// Account Balances & Limits
double accountBalance = 1250.00;
double dailyWithdrawalLimit = 500.00;
double dailyWithdrawnSoFar = 150.00;

// Transaction Inputs & Totals
int enteredPin;
double withdrawalAmount;
double totalFees = 0.00;
double totalDeduction;

Console.WriteLine("========================================");
Console.WriteLine("Welcome to the ATM Simulation!");
Console.WriteLine("========================================");

if (isCardBlocked)
{
    Console.WriteLine("Your card is blocked. Please visit your nearest branch.");
    return;
}

Console.WriteLine("Enter your 4-digit PIN:");

if (!int.TryParse(Console.ReadLine(), out enteredPin) || enteredPin != correctPin)
{
    Console.WriteLine("Incorrect PIN. Please try again.");
    return;
}

Console.WriteLine($"\nCurrent Account Balance: ₱{accountBalance:F2}");

Console.Write("Enter withdrawal amount: ₱");

if (!double.TryParse(Console.ReadLine(), out withdrawalAmount) || withdrawalAmount <= 0)
{
    Console.WriteLine("Invalid amount. Transaction aborted.");
    return;
}

if (withdrawalAmount <= 0 )
{
    Console.WriteLine("Invalid amount. Must be greater than zero.");
    return;
}

if (withdrawalAmount % 20 != 0 && withdrawalAmount % 50 != 0)
{
    Console.WriteLine("Invalid denomination. Machine only dispenses " + " ₱20 and ₱50 notes.");
    return;
}

double remainingDailyLimit = dailyWithdrawalLimit - dailyWithdrawnSoFar;

if (withdrawalAmount > remainingDailyLimit)
{
    Console.WriteLine($"Declined: Daily withdrawal limit exceeded. " + 
    $"Remaining limit: ₱{remainingDailyLimit:F2}.");
    return;
}

if (withdrawalAmount > accountBalance)
{
    Console.WriteLine("Declined: Insufficient account balance.");
    return;
}

if (!isVipCustomer)
{
    if (isInternationalCard)
    {
        totalFees += 4.50;
    }

    if (withdrawalAmount > 300.00)
    {
        totalFees += withdrawalAmount * 0.01;
    }
}

totalDeduction = withdrawalAmount + totalFees;

if (totalDeduction > accountBalance)
{
    Console.WriteLine($"Declined: Balance cannot cover withdrawal plus fees " + $"(₱ {totalDeduction:F2}).");
    return;
}

accountBalance -= totalDeduction;
dailyWithdrawnSoFar += withdrawalAmount;

Console.WriteLine("\nTransaction Approved!");
Console.WriteLine($"Withdrawal Amount: ₱{withdrawalAmount:F2}");
Console.WriteLine($"International card fee: " + $"₱{(isVipCustomer ? 0.00 : (isInternationalCard ? 4.50 : 0.00)):F2}");

double highValueFee = 0.00;

if (!isVipCustomer && withdrawalAmount > 300.00)
{
    highValueFee = withdrawalAmount * 0.01;
    Console.WriteLine($"High-value surcharge: ₱{highValueFee:F2}");
}

Console.WriteLine($"Total Fees: ₱{totalFees:F2}");
Console.WriteLine($"Total Deduction: ₱{totalDeduction:F2}");
Console.WriteLine($"Remaining Balance: ₱{accountBalance:F2}");
Console.WriteLine($"Daily Withdrawal used: ₱{dailyWithdrawnSoFar:F2}");
Console.WriteLine($"Daily Limit Remaining: 4{dailyWithdrawalLimit - dailyWithdrawnSoFar:F2}");

Console.WriteLine("Please take your cash and receipt.");

