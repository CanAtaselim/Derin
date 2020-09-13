using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel;
using Derin.Data.DataCommon;
using Derin.Data.Model;
using Derin.Data.Repository;
using Derin.Data.UnitOfWork.Derin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using static Derin.Common._Enumeration.IsOperationDeleted;

namespace Derin.Business.BusinessLogic
{
    public class RoleBL : BaseBL<Role, RoleVM>
    {
        private IUnitOfWork _unitOfWork;

        public IGenericRepository<Role> CRUD;

        private Expression<Func<Role, RoleVM>> entityToModel = x => new RoleVM
        {
            IdParentRoleRef = x.IdParentRoleRef,
            IdRole = x.IdRole,
            RoleDescription = x.RoleDescription,
            RoleName = x.RoleName,
            StaticRole = x.StaticRole,
            LocationAdmin = x.LocationAdmin ?? false,
            IsRoleDistribution = x.IsRoleDistribution ?? false,
            RoleCode = x.RoleCode,
        };

        private Expression<Func<RoleVM, Role>> modelToEntity = x => new Role
        {
            IdParentRoleRef = x.IdParentRoleRef,
            IdRole = x.IdRole,
            RoleDescription = x.RoleDescription,
            RoleName = x.RoleName,
            StaticRole = x.StaticRole,
            LocationAdmin = x.LocationAdmin,
            IsRoleDistribution = x.IsRoleDistribution,
            RoleCode = x.RoleCode,

        };

        public RoleBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.RoleRepository;
        }
        
        //Rollerin DropDownList için döndrürlmesi
        public IEnumerable<SelectListItem> GetRoleSelectList() 
        {
            return CRUD.Query(q => q.OperationIsDeleted == (short)Active).Select(q => new SelectListItem {
                Text = q.RoleName,
                Value = q.IdRole.ToString()
            });
        }

        //RoleCode'ların yetki sayfası için döndürülmesi
        public IEnumerable<SelectListItem> GetRoleCodeSelectList()
        {
            return CRUD.Query(q => q.OperationIsDeleted == (short)Active).Select(q => new SelectListItem
            {
                Text = q.RoleCode,
                Value = q.IdRole.ToString()
            });
        }
        
        public override List<RoleVM> GetVM(Expression<Func<Role, bool>> filter = null, Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<Role, object>>[] includes)
        {
            return CRUD.Get(filter, orderBy, take, skip, includes).Select(entityToModel.Compile()).ToList();
        }

        public List<RoleVM> GetVMExtended(Expression<Func<Role, bool>> filter = null, Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<Role, object>>[] includes)
        {
            var q = CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection).Select(entityToModel);

            return q.ToList();
        }

        public List<Role_List_Result> GetUserRoleDetails(long? userId = 0)
        {
            return EntityForSP.Role_List(userId).ToList();
        }

        //public List<DistributedAuthoritiesByUserList_Result> GetDistributedAuthoritiesByUserList(long? userId = 0)
        //{
        //    return EntityForSP.DistributedAuthoritiesByUserList(userId).ToList();
        //}

        public IEnumerable<RoleVM> Post(List<RoleVM> roles, HttpRequestInfo info)
        {
            var addedRoles = new List<Role>();

            roles.ForEach(role =>
            {
                var entity = modelToEntity.Compile()(role);

                entity.OperationIsDeleted = (short)Active;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;
                entity.RoleTopMenu = new List<RoleTopMenu>();
                
                

                CRUD.Insert(entity);
                addedRoles.Add(entity);
            });

            Save();

            return addedRoles.Select(entityToModel.Compile());
        }

        public IEnumerable<RoleVM> Update(List<RoleVM> roles, HttpRequestInfo info)
        {
            var addedRoles = new List<Role>();

            roles.ForEach(role =>
            {
                if (role.IdRole == 0) throw new System.Exception("IdRole alanı her zaman dolu olmalıdır.");

                var entity = modelToEntity.Compile()(role);

                entity.OperationIsDeleted = (short)Active;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;
              
                CRUD.Update(entity, info);
                addedRoles.Add(entity);
            });

            Save();

            return addedRoles.Select(entityToModel.Compile());
        }

        public IEnumerable<RoleVM> Delete(List<long> ids, HttpRequestInfo info)
        {
            var deletes = GetVM(q => ids.Any(id => id == q.IdRole));

            var result = Delete(deletes, info);

            return result;
        }
        
        public IEnumerable<RoleVM> Delete(List<RoleVM> roles, HttpRequestInfo info)
        {
            var addedRoles = new List<Role>();

            roles.ForEach(role =>
            {
                var entity = CRUD.GetById(role.IdRole);

                entity.OperationIsDeleted = (short)Deleted;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                //CRUD.Update(entity, info);

                addedRoles.Add(entity);
            });

            Save();

            return addedRoles.Select(entityToModel.Compile());
        }

        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
