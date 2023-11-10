# Brian's USB Quiz Box API

##  About

An API for interfacing with a USB Quiz Box by Brian's Boxes

## How to Use

Create an instance of 'QuizBoxApi'. Register with events and then call 'Connect'.

```cs
using BriansUsbQuizBoxApi;

Console.WriteLine("--- Brian's USB Quiz Box Test App ---");

var api = new QuizBoxApi(new QuizBoxCoreApi());

api.QuizBoxReady += Api_QuizBoxReady;
api.BuzzIn += Api_BuzzIn;
api.FiveSecondTimerStarted += Api_FiveSecondTimerStarted;
api.FiveSecondTimerExpired += Api_FiveSecondTimerExpired;
api.LockoutTimerStarted += Api_LockoutTimerStarted;
api.LockoutTimerExpired += Api_LockoutTimerExpired;
api.GameStarted += Api_GameStarted;
api.GameLightOn += Api_GameLightOn;
api.GameFirstBuzzIn += Api_GameFirstBuzzIn;
api.GameDone += Api_GameDone;
api.BuzzInStats += Api_BuzzInStats;

api.ReadThreadDisconnection += Api_ReadThreadDisconnection;

void Api_ReadThreadDisconnection(object? sender, DisconnectionEventArgs e)
{
    Console.WriteLine("ERROR: Disconnection occurred in background read thread! Program will need to be restarted...");
}

void Api_GameStarted(object? sender, EventArgs e)
{
    Console.WriteLine("Game mode started.  Wait for yellow light to come on and press a paddle!");
}

void Api_GameLightOn(object? sender, EventArgs e)
{
    Console.WriteLine("Yellow light on!");
}

void Api_GameFirstBuzzIn(object? sender, EventArgs e)
{
    Console.WriteLine("Game first buzz in!");
}

void Api_GameDone(object? sender, GameDoneEventArgs e)
{
    Console.WriteLine("Game done!");
    Console.WriteLine($"Red 1 Time = {e.Red1Time}ms");
    Console.WriteLine($"Red 2 Time = {e.Red2Time}ms");
    Console.WriteLine($"Red 3 Time = {e.Red3Time}ms");
    Console.WriteLine($"Red 4 Time = {e.Red4Time}ms");
    Console.WriteLine($"Green 1 Time = {e.Green1Time}ms");
    Console.WriteLine($"Green 2 Time = {e.Green2Time}ms");
    Console.WriteLine($"Green 3 Time = {e.Green3Time}ms");
    Console.WriteLine($"Green 4 Time = {e.Green4Time}ms");
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

void Api_QuizBoxReady(object? sender, EventArgs e)
{
    Console.WriteLine("Quiz box is ready");
}

void Api_BuzzIn(object? sender, BuzzInEventArgs e)
{
    Console.WriteLine($"Buzz in: {e.PaddleColor} - {e.PaddleNumber}");
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

## Notes

- Do **not** block on events from QuizBoxApi.  This will prevent the background read thread from running
- Do **not** make call command methods (like 'Reset') from events on QuizBoxApi. An exception will be thrown if this is attempted

## Credits

- Brian McKevett <bmckevett@yahoo.com> for providing technical documentation about the quiz boxes