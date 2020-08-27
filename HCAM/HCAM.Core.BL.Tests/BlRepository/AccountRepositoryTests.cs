using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.BLL;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.IoC;
using HCAM.Core.BL.Models;
using HCAM.RepoDb.Dal.Interfaces;
using HCAM.RepoDb.Dal.Models;
using Moq;
using Xunit;

namespace HCAM.Core.BL.Tests.BlRepository
{
    public class AccountRepositoryTests
    {

        private readonly Mock<ICommandRepository<Accounts>> _commandRepository;
        private readonly Mock<IQueryRepository<Accounts>> _queryRepository;
        private readonly Mock<IAccountCommand> _sutCommand;
        private readonly Mock<IAccountQuery> _sutQuery;

        public AccountRepositoryTests ()
        {
            var mapper = new IoCTest();
            //Contain
            _commandRepository = new Mock<ICommandRepository<Accounts>>();
            _queryRepository = new Mock<IQueryRepository<Accounts>>();
            _sutCommand = new Mock<IAccountCommand>();
            _sutQuery = new Mock<IAccountQuery>();
        }

        private void BuildRepository (AccountItem item)
        {
            var account = item.MapTo<Accounts>();
            var result = Result<object>.Ok(1);
            _commandRepository.Setup(x => x.Add(account)).Verifiable();// .Returns(result);
            _commandRepository.Setup(x => x.Delete(1)).Returns(Result<int>.Ok(1));
        }

        [Fact]
        public void Add_to_Return_Success ()
        {
            var newAccount = new AccountItem {Id = 1, AccountName = "Account Test 1", AccountTypeId = 1,};

            BuildRepository(newAccount);
            var sut = new AccountRepository(_commandRepository.Object, _queryRepository.Object);

            //var result = sut.Delete(1);
            var result = sut.Add(newAccount);


            result.Success.Should().BeTrue();
            result.Value.Should().Be(newAccount.Id);
        }
    }
}
