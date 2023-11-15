using AoC.Console;
using AoC.Console.Interfaces;
using AoC.Console.Services;
using SimpleInjector;
using System.Net;

static class Program
{
    static readonly Container container;
    private static readonly string[] secrets;

    static Program()
    {
        secrets = File.ReadAllLines("secrets.txt");

        container = new Container();

        container.Register<IConsoleWriter, ConsoleWriter>();
        container.Register<IInput, Input>();
        container.Register<IFileWriter, FileWriter>();
        container.Register<IFileReader, FileReader>();
        container.Register<IFileChecker, FileChecker>();

        container.Collection.Register(typeof(IDay), typeof(IDay).Assembly);

        container.Register<IDayExecuter, DayExecuter>();

        static HttpClient httpClientInstanceCreator() => new(GetHttpClientHandler())
        {
            BaseAddress = new Uri($"https://adventofcode.com/")
        };

        container.Register(httpClientInstanceCreator, Lifestyle.Singleton); ;

        container.Verify();
    }

    static async Task Main()
    {
        var dayExecuter = container.GetInstance<IDayExecuter>();

        await dayExecuter.Execute(int.Parse(secrets[0]), int.Parse(secrets[1]));

        container.Dispose();
    }

    private static HttpMessageHandler GetHttpClientHandler()
        => new HttpClientHandler
        {
            UseCookies = true,
            CookieContainer = GetCookieContainer()
        };

    private static CookieContainer GetCookieContainer()
    {
        var container = new CookieContainer();
        container.Add(new Cookie
        {
            Name = "session",
            Domain = ".adventofcode.com",
            Value = secrets[2]
        });

        return container;
    }
}
