# Brian's USB Quiz Box Communication Protocol API

##  About

An API for interfacing with USB quiz boxes using the Brian's Quiz Box communication protocol.  These include:
- Brian's Quiz Boxes
- Kirkman Basic Quiz Boxes (name TBD)

## How to Use

Create an instance of 'QuizBoxApi'. Register with events and then call 'Connect'.

```cs
using BriansUsbQuizBoxApi;

Console.WriteLine("--- Brian's USB Quiz Box Communication Protocol Test App ---");

using var api = new QuizBoxApi(new QuizBoxCoreApi());

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
    Console.WriteLine("Press 'G' to start reaction timing game. Press [ENTER] to exit program...");
}

var key = Console.ReadKey();
while(key.Key != ConsoleKey.Enter)
{
    if(key.Key == ConsoleKey.G)
    {
        Console.WriteLine("Starting reaction time game...");
        api.StartReactionTimingGame();
    }

    key = Console.ReadKey();
}
```

Complete example located at: https://github.com/pvoelker/BriansUsbQuizBoxApi/tree/main/BriansUsbQuizBoxApi.TestApp

## Notes

- Do **not** block on events from QuizBoxApi.  This will prevent the background read thread from running
- Do **not** make call command methods (like 'Reset') from events on QuizBoxApi. An exception will be thrown if this is attempted. Using Task.Run is a way to get around this limitation
- On MacOS / Mac Catalyst if the application is running in a 'sandbox' (com.apple.security.app-sandbox), you will need to apply the 'com.apple.security.device.usb' entitlement. Otherwise quiz boxes will **not** be detected

## Credits

- Brian McKevett <bmckevett@yahoo.com> for providing technical documentation about the quiz box communication protocol
- Steve Kirkman <kirkmans@aol.com> for working with me on adding support for the Kirkman quiz boxes