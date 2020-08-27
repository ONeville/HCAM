using System;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Services
{
    public class ActionCodeServices : IActionCodeServices
    {
        private readonly IActionCodeCommand _repository;

        public ActionCodeServices (IActionCodeCommand repository)
        {
            _repository = repository;
        }
        public Result<int> Add (ActionCodeItem entity)
        {
            var fieldCode = 0;
            if (entity.IsFee)
            {
                fieldCode = 10000;
            }
            if (entity.IsAsset)
            {
                fieldCode = 50000;
            }
            if (entity.IsEquity)
            {
                fieldCode = 90050;
            }
            if (entity.IsLiability)
            {
                fieldCode = 30000;
            }

            return _repository.GetLastCode(fieldCode).Bind(result => Save(entity, result, fieldCode));
        }
        private Result<int> Save(ActionCodeItem entity, long latestCode, int fieldCode)
        {
            if (latestCode >= fieldCode)
            {
                latestCode = latestCode + 1;
            }
            else
            {
                latestCode = fieldCode;
            }
            entity.Code = latestCode.MapTo<int>();
            return _repository.Add(entity);
        }
    }
}
