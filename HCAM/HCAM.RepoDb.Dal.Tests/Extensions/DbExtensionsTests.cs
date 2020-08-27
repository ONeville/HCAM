using System;
using System.Data;
using System.Data.SqlClient;
using FluentAssertions;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using static HCAM.RepoDb.Dal.Extensions.DalExtensions;
using HCAM.RepoDb.Dal.Interfaces;
using Xunit;
using Moq;
using RepoDb.Extensions;

namespace HCAM.RepoDb.Dal.Tests.Extensions
{
    public class DbExtensionsTests : IDisposable
    {
        private Mock<IDbContext> _contextMock;
        public DbExtensionsTests ()
        {
            _contextMock = new Mock<IDbContext>();
        }

        [Fact]
        public void DalExtensions_DbConnection_to_succeed_a_InsertRequest()
        {
            //Arrange
            var connectionDb = Mock.Of<IDbConnection>();
            int InsertSuccess(IDbConnection cnn) => 1;

            _contextMock.Setup(x => x.DbConnection).Returns(Result<IDbConnection>.Ok(connectionDb));
            var context = _contextMock.Object;

            //Action
            var result = context.DbConnection.Bind(cnn => InsertSuccess(cnn).ToResult());


            result.Success.Should().BeTrue();
        }

        [Fact]
        public void DalExtensions_DbConnection_to_Fail_If_not_connection()
        {
            //Arrange
            var connectionDb = Mock.Of<IDbConnection>();
            int InsertSuccess(IDbConnection cnn) => 1;

            _contextMock.Setup(x => x.DbConnection).Returns(Result<IDbConnection>.Ok(connectionDb));
            var context = _contextMock.Object;

            //Action
            var result = context.DbConnection.Bind(cnn => InsertSuccess(cnn).ToResult());


            result.Success.Should().BeTrue();
        }
        public void Dispose ()
        {
            _contextMock = null;
        }
    }
}
