// See https://aka.ms/new-console-template for more information
using BriansUsbQuizBoxApi;

Console.WriteLine("Hello, World!");

var api = new QuizBoxApi();

api.QuizBoxReady += Api_QuizBoxReady;
api.BuzzIn += Api_BuzzIn;
api.FiveSecondTimerStarted += Api_FiveSecondTimerStarted;
api.FiveSecondTimerExpired += Api_FiveSecondTimerExpired;
api.LockoutTimerStarted += Api_LockoutTimerStarted;
api.LockoutTimerExpired += Api_LockoutTimerExpired;

void Api_QuizBoxReady(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Cyan;

    Console.WriteLine("Quiz box is ready");

    Console.ForegroundColor = ConsoleColor.White;
}

void Api_BuzzIn(object? sender, BuzzInEventArgs e)
{
    if (e.PaddleColor == PaddleColorEnum.Green)
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }
    else if (e.PaddleColor == PaddleColorEnum.Red)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }

    Console.WriteLine($"Buzz in: {e.PaddleColor} - {e.PaddleNumber}");

    Console.ForegroundColor = ConsoleColor.White;
}

void Api_FiveSecondTimerStarted(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Five second timer started");

    Console.ForegroundColor = ConsoleColor.White;
}

void Api_FiveSecondTimerExpired(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Five second timer expired");

    Console.ForegroundColor = ConsoleColor.White;
}
void Api_LockoutTimerStarted(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Lockout timer started");

    Console.ForegroundColor = ConsoleColor.White;
}
void Api_LockoutTimerExpired(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Lockout timer expired");

    Console.ForegroundColor = ConsoleColor.White;
}

if (api.Connect() == false)
{
    Console.WriteLine("Unable to connect to a quiz box");
}
else
{
    api.Reset();
}

Console.WriteLine("Press [ENTER] to stop...");

Console.ReadLine();