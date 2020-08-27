using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface ICodeMapperDetailCommand
    {
        Task<Result<int>> Save(IEnumerable<CodeMapperDetailItem> entity);
    }

    public interface ICodeMapperDetailQuery
    {
        Result<IEnumerable<CodeMapperDetailItem>> Find(object criteria = null);
        Task<Result<IEnumerable<CodeMapperDetailItem>>> FindAsync ();
        Result<IEnumerable<CodeMapperDetailItem>> GetDetails(int mapperId);
        Result<IEnumerable<CodeMapperDetailItem>> GenerateDetails(int mapperId);
    }

}