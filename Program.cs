namespace Uke_1_d2;

public class Program
{
    public static void Main(string[] args)
    {
        // inital setup
        var builder = WebApplication.CreateBuilder(args);
        // set up logging middleware
        builder.Logging.AddConsole();

        // build the application
        var app = builder.Build();
        app.Logger.LogInformation("Server running..");
        // use default & static files
        app.UseDefaultFiles();
        // with this line of code, we route directly to index.html as our first endpoint
        app.UseStaticFiles();
        // routes
        app.MapGet("/api/status", () =>
        {
            app.Logger.LogCritical("critical");
            app.Logger.LogInformation("information");
            app.Logger.LogError("Error");
            return "Hello C# Client!";
        });
        // lambda expression that returns a single string
        app.MapGet("/api/secret", () => "Hello friend!");
        // example of a simple url endpoint where we use a lambda expression to create a new Object internally
        app.MapGet("/api/json", () => new
        {
            message = "Hello",
            id = 1

        });
        // we implement our class SimpleResponse to generate our JSON data on the new endpoint
        app.MapGet("api/simpleres", () => new SimpleResponse
        {
            Message = "Hello World!",
            Id = 2
        });

        app.MapPost("/api/simplerequest", (SimpleRequest request) =>
        {
            Console.WriteLine($"Data recieved from client: {request.Message}");
            return "Hello Client! Message recieved.";
        });
        app.Run();
    }
}
