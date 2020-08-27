using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Services
{
    public class PostingServices : IPostingServices
    {
        private readonly IPostingCommand _repositoryCommand;
        private readonly IPostingQuery _repositoryQuery;
        private readonly IReadCodeMapperServices _servicesCodeMapperServices;
        private readonly IPostingDetailServices _serviceDetails;

        public PostingServices(IReadCodeMapperServices services, IPostingDetailServices serviceDetails, IPostingQuery repositoryQuery, IPostingCommand repositoryCommand)
        {
            _servicesCodeMapperServices = services;
            _serviceDetails = serviceDetails;
            _repositoryQuery = repositoryQuery;
            _repositoryCommand = repositoryCommand;
        }

        public Result<IEnumerable<PostingItem>> Find (PostingFilter criteria = null) => _repositoryQuery.Find(criteria);

        public Task<Result<IEnumerable<PostingItem>>> FindAsync(PostingFilter criteria) => _repositoryQuery.FindAsync(criteria);
        
        public Result<PostingItem> GetById (int id)
        {
            return _repositoryQuery.GetById(id).Bind<PostingItem>(result =>
                _serviceDetails.GetDetails(result.Id).Bind(details =>
                {
                    result.DetailItems = details;
                    return Result<PostingItem>.Ok(result);
                }));
        }

        public Result<PostingItem> Save (PostingItem entity)
        {
            switch (entity.EntityState)
            {
                case ItemState.Added:
                    return _repositoryCommand.Save(entity).Bind(id => _repositoryQuery.GetById(id))
                        .Bind<PostingItem>(item => _serviceDetails.AddItemDetails(item));
                case ItemState.Modified:
                    return GetById(entity.Id);
                case ItemState.Delete:
                    return Result<PostingItem>.Fail<PostingItem>("Not Implemented Exception on Delete state");
                case ItemState.Unchanged:
                    return Result<PostingItem>.Fail<PostingItem>("Not Implemented Exception on Unchanged state");
                default:
                    return Result<PostingItem>.Fail<PostingItem>("Wrong entity state");
            }
        }

        public Result<PostingItem> New ()
        {
            return _servicesCodeMapperServices.GetDefault(false).Bind(mapper => Result<PostingItem>.Ok(new PostingItem
            {
                CodeMappingId = mapper.Id,
                CodeMapping = mapper.MapperName,
                StartDate = DateTime.Today.Date,
                EndDate = DateTime.Now.AddMonths(1).AddDays(-1).Date,
                EntityState = ItemState.Added
            }));
        }
    }
}