

namespace ConsoleApp.Services
{
    using System;
    using HelloWorldInfrastructure.FrameworkWrappers;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Resources;
    using HelloWorldInfrastructure.Services;
    using RestSharp;

    public class HelloWorldWebService : IHelloWorldWebService
    {

        private readonly IAppSettings appSettings;


        private readonly ILogger logger;

        private readonly IRestClient restClient;


        private readonly IRestRequest restRequest;

        private readonly IUri uriService;

        public HelloWorldWebService(
            IRestClient restClient,
            IRestRequest restRequest,
            IAppSettings appSettings,
            IUri uriService,
            ILogger logger)
        {
            this.restClient = restClient;
            this.restRequest = restRequest;
            this.appSettings = appSettings;
            this.uriService = uriService;
            this.logger = logger;
        }


        public TodaysData GetTodaysData()
        {
            TodaysData todaysData = null;

            this.restClient.BaseUrl = this.uriService.GetUri(this.appSettings.Get(AppSettingsKeys.HelloWorldApiUrlKey));


            this.restRequest.Resource = "todaysdata";
            this.restRequest.Method = Method.GET;


            this.restRequest.Parameters.Clear();

            var todaysDataResponse = this.restClient.Execute<TodaysData>(this.restRequest);

            if (todaysDataResponse != null)
            {
       
                if (todaysDataResponse.Data != null)
                {
                    todaysData = todaysDataResponse.Data;
                }
                else
                {
                    var errorMessage = "Error in RestSharp, most likely in endpoint URL." + " Error message: "
                                       + todaysDataResponse.ErrorMessage + " HTTP Status Code: "
                                       + todaysDataResponse.StatusCode + " HTTP Status Description: "
                                       + todaysDataResponse.StatusDescription;

       
                    if (todaysDataResponse.ErrorMessage != null && todaysDataResponse.ErrorException != null)
                    {
  
                        this.logger.Error(errorMessage, null, todaysDataResponse.ErrorException);
                    }
                    else
                    {

                        this.logger.Error(errorMessage, null, new Exception(todaysDataResponse.Content));
                    }
                }
            }
            else
            {
 
                const string ErrorMessage =
                    "Did not get any response from the Hello World Web Api for the Method: GET /todaysdata";

                this.logger.Error(ErrorMessage, null, new Exception(ErrorMessage));
            }

            return todaysData;
        }
    }
}