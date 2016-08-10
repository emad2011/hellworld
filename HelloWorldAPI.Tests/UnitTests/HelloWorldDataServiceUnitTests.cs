

namespace HelloWorldAPI.Tests.UnitTests
{
    using System;
    using System.Configuration;
    using System.IO;
    using HelloWorldInfrastructure.FrameworkWrappers;
    using HelloWorldInfrastructure.Mappers;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Resources;
    using HelloWorldInfrastructure.Services;
    using Moq;
    using NUnit.Framework;


    [TestFixture]
    public class HelloWorldDataServiceUnitTests
    {

        private Mock<IAppSettings> appSettingsMock;


        private Mock<IDateTime> dateTimeWrapperMock;

        private Mock<IFileIOService> fileIOServiceMock;

        private Mock<IHelloWorldMapper> helloWorldMapperMock;

        private HelloWorldDataService helloWorldDataService;


        [TestFixtureSetUp]
        public void InitTestSuite()
        {

            this.appSettingsMock = new Mock<IAppSettings>();
            this.dateTimeWrapperMock = new Mock<IDateTime>();
            this.fileIOServiceMock = new Mock<IFileIOService>();
            this.helloWorldMapperMock = new Mock<IHelloWorldMapper>();

            this.helloWorldDataService = new HelloWorldDataService(
                this.appSettingsMock.Object,
                this.dateTimeWrapperMock.Object,
                this.fileIOServiceMock.Object,
                this.helloWorldMapperMock.Object);
        }

        #region GetTodaysData Tests

        [Test]
        public void UnitTestHelloWorldDataServiceGetTodaysDataSuccess()
        {
            // Create return models for dependencies
            const string DataFilePath = "some/path";
            const string FileContents = "Hello World!";
            var nowDate = DateTime.Now;
            var rawData = FileContents + " as of " + nowDate.ToString("F");

            // Create the expected result
            var expectedResult = GetSampleTodaysData(rawData);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.TodayDataFileKey)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Returns(FileContents);
            this.dateTimeWrapperMock.Setup(m => m.Now()).Returns(nowDate);
            this.helloWorldMapperMock.Setup(m => m.StringToTodaysData(rawData)).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldDataService.GetTodaysData();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }


        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.TodaysDataFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetTodaysDataSettingKeyNull()
        {
            const string DataFilePath = null;

            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.TodayDataFileKey)).Returns(DataFilePath);

            this.helloWorldDataService.GetTodaysData();
        }


        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.TodaysDataFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetTodaysDataSettingKeyEmptyString()
        {

            var dataFilePath = string.Empty;


            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.TodayDataFileKey)).Returns(dataFilePath);

  
            this.helloWorldDataService.GetTodaysData();
        }


        [Test]
        [ExpectedException(ExpectedException = typeof(IOException))]
        public void UnitTestHelloWorldDataServiceGetTodaysDataIOException()
        {
       
            const string DataFilePath = "some/path";

    
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.TodayDataFileKey)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Throws(new IOException("Error!"));

        
            this.helloWorldDataService.GetTodaysData();
        }
        #endregion

        #region Helper Methods

        private static TodaysData GetSampleTodaysData(string data)
        {
            return new TodaysData { Data = data };
        }
        #endregion
    }
}