

namespace HelloWorldAPI.Tests.UnitTests
{
    using HelloWorldInfrastructure.Mappers;
    using HelloWorldInfrastructure.Models;
    using NUnit.Framework;


    [TestFixture]
    public class HelloWorldMapperUnitTests
    {

        private HelloWorldMapper helloWorldMapper;


        [TestFixtureSetUp]
        public void InitTestSuite()
        {

            this.helloWorldMapper = new HelloWorldMapper();
        }

        #region StringToTodaysData Tests

        [Test]
        public void UnitTestHelloWorldMapperStringToTodaysDataNormalSuccess()
        {
            const string Data = "Hello World!";

      
            var expectedResult = GetSampleTodaysData(Data);

         
            var result = this.helloWorldMapper.StringToTodaysData(Data);

            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }


        [Test]
        public void UnitTestHelloWorldMapperStringToTodaysDataNullSuccess()
        {
            const string Data = null;

            
            var expectedResult = GetSampleTodaysData(Data);

     
            var result = this.helloWorldMapper.StringToTodaysData(Data);

        
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }
        #endregion

        #region Helper Methods
 
        private static TodaysData GetSampleTodaysData(string data)
        {
            return new TodaysData()
            {
                Data = data
            };
        }
        #endregion
    }
}