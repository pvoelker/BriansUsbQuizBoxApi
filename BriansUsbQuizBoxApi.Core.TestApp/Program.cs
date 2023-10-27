using BriansUsbQuizBoxApi;
using BriansUsbQuizBoxApi.Protocols;

Console.WriteLine("--- Brian's Boxes Core API Test ---");

Console.WriteLine("A quiz box must be connected...");

var api = new QuizBoxCoreApi();

Console.WriteLine("Connecting...");

if (api.Connect() == false)
{
    Console.WriteLine("Unable to connect to a quiz box");
}
else
{
    Console.WriteLine("Writing clear command...");

    api.WriteCommand(new BoxCommand { CommandHeader = CommandHeaderByte.CLEAR });

    Console.WriteLine("Read status...");

    api.WriteCommand(new BoxCommand { CommandHeader = CommandHeaderByte.STATUS_REQUEST });

    Thread.Sleep(100);

    var status = await api.ReadStatusAsync();

    var statusText = status == null ? "[null]" : status.ToString();
    Console.WriteLine($"Status: {statusText}");
}

Console.WriteLine("Press [ENTER] to stop...");

Console.ReadLine();