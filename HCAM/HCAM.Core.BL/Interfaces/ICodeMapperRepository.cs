using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface ICodeMapperCommand
    {
        Result<int> Save(CodeMapperItem entity);
    }

    public interface ICodeMapperQuery
    {
        Result<IEnumerable<CodeMapperItem>> Find(object criteria = null);
        Task<Result<IEnumerable<CodeMapperItem>>> FindAsync(object filter = null);
        Result<CodeMapperItem> GetById(int id);
    }
}