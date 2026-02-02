# Brian's USB Quiz Box Communication Protocol API

##  About

An API for interfacing with USB quiz boxes using the Brian's Quiz Box communication protocol.  At the time this documentation was written, these include:
- Brian's Quiz Box
- Kirkman Basic Quizbox Plus (light gray bottom) -- https://www.quizequipment.com/

The goal of this API is to provide an easy to use interface for developers to create software that uses these quiz boxes.

If there are any issues or feature requests, please contact me via my GitHub account: https://github.com/pvoelker/

## 'Kirkman Basic Quizbox Plus' Deviations and Departures

- 'StopPaddleLockout' is not supported, however 'Reset' will clear a paddle lockout
- Buzz in times are not guaranteed to be the same as Brian's Quiz Box due to hardware differences. They are close, but may vary by a few milliseconds

## NuGet Package

https://www.nuget.org/packages/BriansUsbQuizBoxApi/

## How to Use

Add a reference to 'BriansUsbQuizBoxApi' NuGet package to your project.

Create an instance of 'QuizBoxApi'. Register with events and then call 'Connect'.

```cs
using BriansUsbQuizBoxApi;

Console.WriteLine("--- Brian's USB Quiz Box Communication Protocol Test App ---");

using var api = new QuizBoxApi(new QuizBoxCoreApi());

api.ConnectionComplete += Api_ConnectionComplete;
api.BuzzIn += Api_BuzzIn;
api.FiveSecondTimerStarted += Api_FiveSecondTimerStarted;
api.FiveSecondTimerExpired += Api_FiveSecondTimerExpired;
api.LockoutTimerStarted += Api_LockoutTimerStarted;
api.LockoutTimerExpired += Api_LockoutTimerExpired;
api.GameStarted += Api_GameStarted;
api.GameLightOn += Api_GameLightOn;
api.GameDone += Api_GameDone;
api.BuzzInStats += Api_BuzzInStats;
api.DisconnectionDetected += Api_DisconnectionDetected;

void Api_ConnectionComplete(object? sender, ConnectionCompleteEventArgs e)
{
    Console.WriteLine($"Connection complete...");
    Console.WriteLine($"Communication protocol version: {e.ProtocolVersion}");
    Console.WriteLine($"Has additional winner information: {e.HasAdditionalWinnerInfo}");
}

void Api_DisconnectionDetected(object? sender, DisconnectionEventArgs e)
{
    Console.WriteLine("ERROR: Disconnection from quiz box detected! Program will need to be restarted...");
}

void Api_GameStarted(object? sender, EventArgs e)
{
    Console.WriteLine("Game mode started.  Wait for yellow light to turn on and press a paddle!");
}

void Api_GameLightOn(object? sender, EventArgs e)
{
    Console.WriteLine("Yellow light on!");
}

void Api_GameDone(object? sender, GameDoneEventArgs e)
{
    Console.WriteLine("Game done!");
    Console.WriteLine($"Additional Winner Info = {e.HasAdditionalWinnerInfo}");
    Console.WriteLine($"First Place Winner = {(e.Winner != null ? e.Winner : "-none-")}");
    Console.WriteLine($"Second Place Winner = {(e.Winner2 != null ? e.Winner2 : "-none-")}");
    Console.WriteLine($"Third Place Winner = {(e.Winner3 != null ? e.Winner3 : "-none-")}");
    Console.WriteLine($"Fourth Place Winner = {(e.Winner4 != null ? e.Winner4 : "-none-")}");
    Console.WriteLine($"Fifth Place Winner = {(e.Winner5 != null ? e.Winner5 : "-none-")}");
    Console.WriteLine($"Sixth Place Winner = {(e.Winner6 != null ? e.Winner6 : "-none-")}");
    Console.WriteLine($"Seventh Place Winner = {(e.Winner7 != null ? e.Winner7 : "-none-")}");
    Console.WriteLine($"Eigth Place Winner = {(e.Winner8 != null ? e.Winner8 : "-none-")}");
    Console.WriteLine($"Red 1 Time = {(e.Red1Time.HasValue ? e.Red1Time + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 2 Time = {(e.Red2Time.HasValue ? e.Red2Time + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 3 Time = {(e.Red3Time.HasValue ? e.Red3Time + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 4 Time = {(e.Red4Time.HasValue ? e.Red4Time + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 1 Time = {(e.Green1Time.HasValue ? e.Green1Time + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 2 Time = {(e.Green2Time.HasValue ? e.Green2Time + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 3 Time = {(e.Green3Time.HasValue ? e.Green3Time + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 4 Time = {(e.Green4Time.HasValue ? e.Green4Time + "ms" : "-no buzz in-")}");

    Console.WriteLine();
    Console.WriteLine("Press Reset to continue...");
}

void Api_BuzzInStats(object? sender, BuzzInStatsEventArgs e)
{
    Console.WriteLine("Buzz in statistics:");
    Console.WriteLine($"Additional Winner Info = {e.HasAdditionalWinnerInfo}");
    Console.WriteLine($"First Place Winner = {(e.Winner != null ? e.Winner : "-none-")}");
    Console.WriteLine($"Second Place Winner = {(e.Winner2 != null ? e.Winner2 : "-none-")}");
    Console.WriteLine($"Third Place Winner = {(e.Winner3 != null ? e.Winner3 : "-none-")}");
    Console.WriteLine($"Fourth Place Winner = {(e.Winner4 != null ? e.Winner4 : "-none-")}");
    Console.WriteLine($"Fifth Place Winner = {(e.Winner5 != null ? e.Winner5 : "-none-")}");
    Console.WriteLine($"Sixth Place Winner = {(e.Winner6 != null ? e.Winner6 : "-none-")}");
    Console.WriteLine($"Seventh Place Winner = {(e.Winner7 != null ? e.Winner7 : "-none-")}");
    Console.WriteLine($"Eigth Place Winner = {(e.Winner8 != null ? e.Winner8 : "-none-")}");
    Console.WriteLine($"Red 1 Time = {(e.Red1TimeDelta.HasValue ? e.Red1TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 2 Time = {(e.Red2TimeDelta.HasValue ? e.Red2TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 3 Time = {(e.Red3TimeDelta.HasValue ? e.Red3TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 4 Time = {(e.Red4TimeDelta.HasValue ? e.Red4TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 1 Time = {(e.Green1TimeDelta.HasValue ? e.Green1TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 2 Time = {(e.Green2TimeDelta.HasValue ? e.Green2TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 3 Time = {(e.Green3TimeDelta.HasValue ? e.Green3TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 4 Time = {(e.Green4TimeDelta.HasValue ? e.Green4TimeDelta + "ms" : "-no buzz in-")}");
}

void Api_BuzzIn(object? sender, BuzzInEventArgs e)
{
    Console.WriteLine($"Buzz in on paddle: {e.Paddle}");
}

void Api_FiveSecondTimerStarted(object? sender, EventArgs e)
{
    Console.WriteLine("Five second timer started");
}

void Api_FiveSecondTimerExpired(object? sender, EventArgs e)
{
    Console.WriteLine("Five second timer expired");
}

void Api_LockoutTimerStarted(object? sender, EventArgs e)
{
    Console.WriteLine("Paddle lockout timer started");
}

void Api_LockoutTimerExpired(object? sender, EventArgs e)
{
    Console.WriteLine("Paddle lockout timer expired");
}

if (api.Connect() == false)
{
    Console.WriteLine("ERROR: Unable to connect to a quiz box");

    Console.WriteLine("Press [ENTER] to exit program...");
}
else
{
    Console.WriteLine("Connected quiz box type: " + api.ConnectedQuizBoxType);
    Console.WriteLine();
    Console.WriteLine("Press 'G' to start reaction timing game.");
    Console.WriteLine("Press 'T' to start 5 second buzz in timer.");
    Console.WriteLine("Press 'S' to start indefinite paddle lockout timer.");
    Console.WriteLine("Press 'E' to end indefinite paddle lockout timer.");
    Console.WriteLine("Press '1' to start 30 second paddle lockout timer.");
    Console.WriteLine("Press '2' to start 1 minute paddle lockout timer.");
    Console.WriteLine("Press '3' to start 2 minute paddle lockout timer.");
    Console.WriteLine("Press '4' to start 3 minute paddle lockout timer.");
    Console.WriteLine("Press 'R' to reset quiz box.");
    Console.WriteLine("Press [ENTER] to exit program...");
}

var key = Console.ReadKey();
while(key.Key != ConsoleKey.Enter)
{
    if(key.Key == ConsoleKey.G)
    {
        Console.WriteLine("Starting reaction time game...");
        api.StartReactionTimingGame();
    }
    else if (key.Key == ConsoleKey.T)
    {
        Console.WriteLine("Starting 5 second buzz in timer...");
        api.Start5SecondBuzzInTimer();
    }
    else if (key.Key == ConsoleKey.S)
    {
        Console.WriteLine("Starting indefinite paddle lockout timer...");
        api.StartPaddleLockout();
    }
    else if (key.Key == ConsoleKey.E)
    {
        Console.WriteLine("Ending indefinite paddle lockout timer...");
        try
        {
            api.StopPaddleLockout();
        }
        catch(NotSupportedException ex)
        {
            Console.WriteLine("WARNING: " + ex.Message);
        }
    }
    else if (key.Key == ConsoleKey.D1)
    {
        Console.WriteLine("Start 30 second paddle lockout timer...");
        api.StartPaddleLockoutTimer(LockoutTimerEnum.Timer30Seconds);
    }
    else if (key.Key == ConsoleKey.D2)
    {
        Console.WriteLine("Start 1 minute paddle lockout timer...");
        api.StartPaddleLockoutTimer(LockoutTimerEnum.Timer1Minute);
    }
    else if (key.Key == ConsoleKey.D3)
    {
        Console.WriteLine("Start 2 minute paddle lockout timer...");
        api.StartPaddleLockoutTimer(LockoutTimerEnum.Timer2Minutes);
    }
    else if (key.Key == ConsoleKey.D4)
    {
        Console.WriteLine("Start 3 minute paddle lockout timer...");
        api.StartPaddleLockoutTimer(LockoutTimerEnum.Timer3Minutes);
    }
    else if (key.Key == ConsoleKey.R)
    {
        Console.WriteLine("Reseting...");
        api.Reset();
    }

    key = Console.ReadKey();
}
```

Complete example located at: https://github.com/pvoelker/BriansUsbQuizBoxApi/tree/main/BriansUsbQuizBoxApi.TestApp

## Notes

- Times between different quiz box models may vary slightly (within a few milliseconds) due to hardware differences
- Do **not** block on events from QuizBoxApi.  This will prevent the background read thread from running
- Do **not** make call command methods (like 'Reset') from events on QuizBoxApi. An exception will be thrown if this is attempted. Using Task.Run is a way to get around this limitation
- On MacOS / Mac Catalyst if the application is running in a 'sandbox' (com.apple.security.app-sandbox), you will need to apply the 'com.apple.security.device.usb' entitlement. Otherwise quiz boxes will **not** be detected
- It is recommended you use .NET Core 10 or later

## Credits

- Brian McKevett <bmckevett@yahoo.com> for providing technical documentation about the quiz box communication protocol
- Steve Kirkman <kirkmans@aol.com> and Brian McKevett <bmckevett@yahoo.com> for working with me on adding support for the Kirkman quiz boxes