using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.UnitOfWork.Derin;
using Derin.Data.Repository;

namespace Derin.Business.BusinessLogic.Administration
{
    public class SystemUserBL : BaseBL<SystemUser, SystemUserVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<SystemUser> CRUD;
        public SystemUserBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.SystemUserRepository;
        }
        public override List<SystemUserVM> GetVM(Expression<Func<SystemUser, bool>> filter = null, Func<IQueryable<SystemUser>, IOrderedQueryable<SystemUser>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<SystemUser, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy, take, skip, includes).Select(s => new SystemUserVM
            {
                Email = s.Email,
                FirstName = s.FirstName,
                IdUser = s.IdUser,
                LastName = s.LastName,
                Address = s.Address,
                IdNo = s.IdNo,
                BirthDate = s.BirthDate,
                MobilePhone = s.MobilePhone,
                LastPasswordChangeDate = s.LastPasswordChangeDate,
                ProfilePictureURL = s.ProfilePictureURL,
                TBSID = s.TbsUserId
            }).ToList();
        }
        public List<SystemUserVM> GetVMExtended(Expression<Func<SystemUser, bool>> filter = null, Func<IQueryable<SystemUser>, IOrderedQueryable<SystemUser>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<SystemUser, object>>[] includes)
        {
            return CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection, includes).Select(s => new SystemUserVM
            {
                Email = s.Email,
                FirstName = s.FirstName,
                IdUser = s.IdUser,
                LastName = s.LastName,
                Address = s.Address,
                IdNo = s.IdNo,
                BirthDate = s.BirthDate,
                MobilePhone = s.MobilePhone,
                LastPasswordChangeDate = s.LastPasswordChangeDate,
                ProfilePictureURL = s.ProfilePictureURL,
                TBSID = s.TbsUserId
            }).ToList();
        }

        //public List<UserAdditionalInfo_Result> GetTBSUserAdditionalInfo(string fin)
        //{
        //     return TbsEntityForSP.UserAdditionalInfo(fin).ToList();
        //}

        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
