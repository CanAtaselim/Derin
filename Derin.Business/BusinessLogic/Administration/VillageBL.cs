using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.Repository;
using Derin.Data.UnitOfWork.Derin;

namespace Derin.Business.BusinessLogic.Administration
{
    public class VillageBL : BaseBL<Village, VillageVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<Village> CRUD;
        public VillageBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.VillageRepository;
        }
        public override List<VillageVM> GetVM(Expression<Func<Village, bool>> filter = null, Func<IQueryable<Village>, IOrderedQueryable<Village>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<Village, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy, take, skip, includes).Select(x => new VillageVM
            {
                XMinPoint = x.BBox_MinPoint.StartPoint.XCoordinate,
                YMinPoint = x.BBox_MinPoint.StartPoint.YCoordinate,
                XMaxPoint = x.BBox_MaxPoint.StartPoint.XCoordinate,
                YMaxPoint = x.BBox_MaxPoint.StartPoint.YCoordinate,
                IdTownRef = x.IdTownRef,
                IdCityRef = x.Town.IdCityRef,
                CityName = x.Town.City.CityName,
                IdVillage = x.IdVillage,
                TakbisVillageCode = x.TakbisVillageCode,
                TownName = x.Town.TownName,
                VillageName = x.VillageName

            }).ToList();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
