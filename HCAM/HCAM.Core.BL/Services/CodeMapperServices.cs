using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Services
{
    public class CodeMapperServices : ICodeMapperServices, IReadCodeMapperServices
    {
        private readonly ICodeMapperCommand _command;
        private readonly ICodeMapperQuery _repository;
        private readonly ICodeMapperDetailQuery _repositoryDetail;

        public CodeMapperServices(ICodeMapperQuery repository, ICodeMapperDetailQuery repositoryDetail, ICodeMapperCommand command)
        {
            _repository = repository;
            _repositoryDetail = repositoryDetail;
            _command = command;
        }
        public Result<int> Save (CodeMapperItem entity) => _command.Save(entity);
        public Result<IEnumerable<CodeMapperItem>> Find () => _repository.Find();

        public Task<Result<IEnumerable<CodeMapperItem>>> FindAsync () => _repository.FindAsync();
        public Result<CodeMapperItem> GetById (int id)
        {
           return _repository.GetById(id).Bind<CodeMapperItem>(result =>
                _repositoryDetail.GetDetails(result.Id).Bind(details =>
                {
                    result.DetailItems = details;
                    return Result<CodeMapperItem>.Ok(result);
                }));
        }
        public Result<CodeMapperItem> GetDefault (bool withDetail = true)
        {
            var defaultItem = _repository.FindAsync(new {IsDefault = true}).Result;
            return defaultItem.Bind(result =>
                _repositoryDetail.GetDetails(result.First().Id).Bind(details =>
                {
                    var item = result.First();
                    item.DetailItems = details;
                    return Result<CodeMapperItem>.Ok(item);
                }));
        }
    }
}
