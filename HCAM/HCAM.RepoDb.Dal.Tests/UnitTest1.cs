using System;
using Xunit;

namespace HCAM.RepoDb.Dal.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var blValue = new DateTime();
            var dataValue = "1899-12-30";

            var result = DateTime.TryParse(dataValue, out blValue);

            Assert.True(result);
            //Assert.True(blValue);
        }
    }
}
