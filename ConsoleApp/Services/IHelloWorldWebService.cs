
namespace ConsoleApp.Services
{
    using HelloWorldInfrastructure.Models;

    public interface IHelloWorldWebService
    {

        TodaysData GetTodaysData();
    }
}