using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel;
using Derin.Business.ViewModel.Administration;
using Derin.Data.DataCommon;
using Derin.Data.Model;
using Derin.Data.Repository;
using Derin.Data.UnitOfWork.Derin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Derin.Common._Enumeration.IsOperationDeleted;

namespace Derin.Business.BusinessLogic
{
    public class RoleAuthorizationBL : BaseBL<RoleAuthorization, RoleAuthorizationVM>
    {
        private IUnitOfWork _unitOfWork;

        public IGenericRepository<RoleAuthorization> CRUD;

        private Expression<Func<RoleAuthorization, RoleAuthorizationVM>> entityToModel = x => new RoleAuthorizationVM
        {
            IdRoleAuthorization = x.IdRoleAuthorization,
            IdRoleRef = x.Role.IdRole,
            Controller = x.Controller,
            RoleName = x.Role.RoleName,
            RoleCode = x.Role.RoleCode,
            Action = x.Action,
            Area = x.Area,
            IsForbidden = x.IsForbidden
            
        };

        private Expression<Func<RoleAuthorizationVM, RoleAuthorization>> modelToEntity = x => new RoleAuthorization
        {

            IdRoleAuthorization = x.IdRoleAuthorization,
            IdRoleRef = x.IdRoleRef,
            Controller = x.Controller,
            Action = x.Action,
            Area = x.Area,
            IsForbidden = x.IsForbidden
        };

        public RoleAuthorizationBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.RoleAuthorizationRepository;
        }

        public override List<RoleAuthorizationVM> GetVM(Expression<Func<RoleAuthorization, bool>> filter = null, Func<IQueryable<RoleAuthorization>, IOrderedQueryable<RoleAuthorization>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<RoleAuthorization, object>>[] includes)
        {
            return CRUD.Get(filter, orderBy, take, skip, includes).Select(entityToModel.Compile()).ToList();
        }

        public List<RoleAuthorizationVM> GetVMExtended(Expression<Func<RoleAuthorization, bool>> filter = null, Func<IQueryable<RoleAuthorization>, IOrderedQueryable<RoleAuthorization>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<RoleAuthorization, object>>[] includes)
        {
            var q = CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection).Select(entityToModel);

            return q.ToList();
        }

        public IEnumerable<RoleAuthorizationVM> GetUserActions(long idUserRef)
        {
            return CRUD.Query(q => q.OperationIsDeleted == (short)Active).Select(entityToModel).ToList();
        }

        public IEnumerable<RoleAuthorizationVM> Post(List<RoleAuthorizationVM> roleAuthorizations, HttpRequestInfo info)
        {
            var addedRoleAuthorizations = new List<RoleAuthorization>();

            
            roleAuthorizations.ForEach(roleAuthorization =>
            {
                var entity = modelToEntity.Compile()(roleAuthorization);

                entity.Action.Trim();//Actionların boşluksuz kaydedilmesi 
                entity.OperationIsDeleted = (short)Active;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;
                
                

                CRUD.Insert(entity);
                addedRoleAuthorizations.Add(entity);
            });

            Save();

            return addedRoleAuthorizations.Select(entityToModel.Compile());
        }

        public IEnumerable<RoleAuthorizationVM> Update(List<RoleAuthorizationVM> roleAuthorizations, HttpRequestInfo info)
        {
            var addedRoleAuthorizations = new List<RoleAuthorization>();

            roleAuthorizations.ForEach(roleAuthorization =>
            {
                if (roleAuthorization.IdRoleAuthorization == 0) throw new System.Exception("IdRole alanı her zaman dolu olmalıdır.");

                var entity = modelToEntity.Compile()(roleAuthorization);

                entity.Action.Trim();//Actionların boşluksuz kaydedilmesi 
                entity.OperationIsDeleted = (short)Active;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                CRUD.Update(entity, info);
                addedRoleAuthorizations.Add(entity);
            });

            Save();

            return addedRoleAuthorizations.Select(entityToModel.Compile());
        }

        public IEnumerable<RoleAuthorizationVM> Delete(List<long> ids, HttpRequestInfo info)
        {
            var deletes = GetVM(q => ids.Any(id => id == q.IdRoleAuthorization));

            var result = Delete(deletes, info);

            return result;
        }

        public IEnumerable<RoleAuthorizationVM> Delete(List<RoleAuthorizationVM> roles, HttpRequestInfo info)
        {
            var addedRoleAuthorizations = new List<RoleAuthorization>();

            roles.ForEach(role =>
            {
                var entity = CRUD.GetById(role.IdRoleAuthorization);

                entity.OperationIsDeleted = (short)Deleted;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                //CRUD.Update(entity, info);

                addedRoleAuthorizations.Add(entity);
            });

            Save();

            return addedRoleAuthorizations.Select(entityToModel.Compile());
        }

        public override void Save()
        {
            _unitOfWork.Save();
        }

        //Rol Yetkilerinin Veritabanına Kaydedilmesi 
        public void InstertIfNotExists(List<RoleAuthorizationVM> roleAuts)
        {
            var olds = GetVM();

            var gointToBeInserted = roleAuts.Where(q => !olds.Any(o => o.IdRoleRef == q.IdRoleRef && 
            o.Area == q.Area && 
            o.Controller == q.Controller && 
            o.Action == q.Action &&
            o.IsForbidden == q.IsForbidden
            )).ToList();

            Post(gointToBeInserted, new HttpRequestInfo
            {
                IpAddress = ":1",
                UserID = 5,
            });
        }
    }
}
