using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces.Services
{
    public interface IActionCodeServices
    {
        Result<int> Add(ActionCodeItem entity);
    }
}