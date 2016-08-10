namespace ConsoleApp.Application
{
    using ConsoleApp.Services;
    using HelloWorldInfrastructure.Services;


    public class HelloWorldConsoleApp : IHelloWorldConsoleApp
    {

        private readonly IHelloWorldWebService helloWorldWebService;

        private readonly ILogger logger;

        public HelloWorldConsoleApp(IHelloWorldWebService helloWorldWebService, ILogger logger)
        {
            this.helloWorldWebService = helloWorldWebService;
            this.logger = logger;
        }

        public void Run(string[] arguments)
        {

            var todaysData = this.helloWorldWebService.GetTodaysData();

            this.logger.Info(todaysData != null ? todaysData.Data : "No data was found!", null);
        }
    }
}