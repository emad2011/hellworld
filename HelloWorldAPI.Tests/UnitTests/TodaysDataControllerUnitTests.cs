

namespace HelloWorldAPI.Tests.UnitTests
{
    using System.Configuration;
    using System.IO;
    using Controllers;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Services;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class TodaysDataControllerUnitTests
    {

        private Mock<IDataService> dataServiceMock;

        private TodaysDataController todaysDataController;

 
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.dataServiceMock = new Mock<IDataService>();

            // Create object to test
            this.todaysDataController = new TodaysDataController(this.dataServiceMock.Object);
        }

        #region Get Tests

        [Test]
        public void UnitTestTodaysDataControllerGetSuccess()
        {
            // Create the expected result
            var expectedResult = GetSampleTodaysData();

            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetTodaysData()).Returns(expectedResult);

            // Call the method to test
            var result = this.todaysDataController.Get();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }


        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException))]
        public void UnitTestTodaysDataControllerGetSettingsPropertyNotFoundException()
        {
            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetTodaysData()).Throws(new SettingsPropertyNotFoundException("Error!"));

            // Call the method to test
            this.todaysDataController.Get();
        }


        [Test]
        [ExpectedException(ExpectedException = typeof(IOException))]
        public void UnitTestTodaysDataControllerGetIOException()
        {
            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetTodaysData()).Throws(new IOException("Error!"));

            // Call the method to test
            this.todaysDataController.Get();
        }
        #endregion

        #region Helper Methods

        private static TodaysData GetSampleTodaysData()
        {
            return new TodaysData()
            {
                Data = "Hello World!"
            };
        }
        #endregion
    }
}