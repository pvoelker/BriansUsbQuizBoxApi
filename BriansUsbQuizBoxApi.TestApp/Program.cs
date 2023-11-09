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
    var bk = Console.BackgroundColor;
    Console.BackgroundColor = ConsoleColor.DarkRed;

    Console.WriteLine("ERROR: Disconnection occurred in background read thread! Program will need to be restarted...");

    Console.BackgroundColor = bk;
}

void Api_GameStarted(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Magenta;

    Console.WriteLine("Game mode started.  Wait for yellow light to come on and press a paddle!");

    Console.ForegroundColor = ConsoleColor.White;
}

void Api_GameLightOn(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Magenta;

    Console.WriteLine("Yellow light on!");

    Console.ForegroundColor = ConsoleColor.White;
}

void Api_GameFirstBuzzIn(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Magenta;

    Console.WriteLine("Game first buzz in!");

    Console.ForegroundColor = ConsoleColor.White;
}

void Api_GameDone(object? sender, GameDoneEventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Magenta;

    Console.WriteLine("Game done!");
    Console.WriteLine($"Red 1 Time = {e.Red1Time}ms");
    Console.WriteLine($"Red 2 Time = {e.Red2Time}ms");
    Console.WriteLine($"Red 3 Time = {e.Red3Time}ms");
    Console.WriteLine($"Red 4 Time = {e.Red4Time}ms");
    Console.WriteLine($"Green 1 Time = {e.Green1Time}ms");
    Console.WriteLine($"Green 2 Time = {e.Green2Time}ms");
    Console.WriteLine($"Green 3 Time = {e.Green3Time}ms");
    Console.WriteLine($"Green 4 Time = {e.Green4Time}ms");

    Console.ForegroundColor = ConsoleColor.White;

    Console.WriteLine();
    Console.WriteLine("Press Reset to continue...");
}

void Api_BuzzInStats(object? sender, BuzzInStatsEventArgs e)
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;

    Console.WriteLine("Buzz in statistics:");
    Console.WriteLine($"Red 1 Time = {(e.Red1TimeDelta.HasValue ? e.Red1TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 2 Time = {(e.Red2TimeDelta.HasValue ? e.Red2TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 3 Time = {(e.Red3TimeDelta.HasValue ? e.Red3TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Red 4 Time = {(e.Red4TimeDelta.HasValue ? e.Red4TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 1 Time = {(e.Green1TimeDelta.HasValue ? e.Green1TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 2 Time = {(e.Green2TimeDelta.HasValue ? e.Green2TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 3 Time = {(e.Green3TimeDelta.HasValue ? e.Green3TimeDelta + "ms" : "-no buzz in-")}");
    Console.WriteLine($"Green 4 Time = {(e.Green4TimeDelta.HasValue ? e.Green4TimeDelta + "ms" : "-no buzz in-")}");

    Console.ForegroundColor = ConsoleColor.White;
}

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

    Console.WriteLine("Paddle lockout timer started");

    Console.ForegroundColor = ConsoleColor.White;
}

void Api_LockoutTimerExpired(object? sender, EventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Paddle lockout timer expired");

    Console.ForegroundColor = ConsoleColor.White;
}

if (api.Connect() == false)
{
    var bk = Console.BackgroundColor;
    Console.BackgroundColor = ConsoleColor.DarkRed;

    Console.WriteLine("ERROR: Unable to connect to a quiz box");

    Console.BackgroundColor = bk;

    Console.WriteLine("Press [ENTER] to exit program...");
}
else
{
    api.Reset();

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