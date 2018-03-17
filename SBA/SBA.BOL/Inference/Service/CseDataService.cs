using SBA.BOL.Common.Factory;
using SBA.BOL.Inference.Models;
using SBA.DAL.Context.InferenceDb.Entity;
using SBA.DAL.Context.InferenceDb.Repository.CseData;
using System;

namespace SBA.BOL.Inference.Service
{
    public interface ICseDataService
    {
        void Add(CseDataModel cseDataModel);
    }

    public class CseDataService : ICseDataService
    {
        private readonly ICseDataRepository _cseDataRepository;

        public CseDataService() => 
            _cseDataRepository = SimpleFactory.Get<CseDataRepository, ICseDataRepository>();

        public void Add(CseDataModel cseDataModel)
        {
            _cseDataRepository.Add(new CseData
            {
                Id = cseDataModel.Id,
                Query = cseDataModel.Query,
                RawJsonQueryResult = cseDataModel.RawJsonQueryResult,
                ObjectType = cseDataModel.ObjectType,
                InsertTime = DateTime.Now
            });
            _cseDataRepository.SaveChanges();
        }
    }
}
