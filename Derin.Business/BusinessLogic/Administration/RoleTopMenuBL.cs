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
using static Derin.Common._Enumeration.IsOperationDeleted;

namespace Derin.Business.BusinessLogic
{
    public class RoleTopMenuBL : BaseBL<RoleTopMenu, RoleTopMenuVM>
    {
        private IUnitOfWork _unitOfWork;

        public IGenericRepository<RoleTopMenu> CRUD;

        private Expression<Func<RoleTopMenu, RoleTopMenuVM>> entityToModel = x => new RoleTopMenuVM
        {
            IdRoleRef = x.IdRoleRef,
            IdRoleTopMenu = x.IdRoleTopMenu,
            IdTopMenuRef = x.IdTopMenuRef ?? 0,
            RoleName = x.Role.RoleName,
            TopMenuName = x.TopMenu == null ? null : x.TopMenu.TopMenuName
        };

        private Expression<Func<RoleTopMenuVM, RoleTopMenu>> modelToEntity = x => new RoleTopMenu {
            IdRoleRef = x.IdRoleRef,
            IdRoleTopMenu = x.IdRoleTopMenu,
            IdTopMenuRef = x.IdTopMenuRef,
        };

        public RoleTopMenuBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.RoleTopMenuRepository;
        }

        public override List<RoleTopMenuVM> GetVM(Expression<Func<RoleTopMenu, bool>> filter = null, Func<IQueryable<RoleTopMenu>, IOrderedQueryable<RoleTopMenu>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<RoleTopMenu, object>>[] includes)
        {
            return CRUD.Get(filter, orderBy, take, skip, includes).Select(entityToModel.Compile()).ToList();
        }

        public List<RoleTopMenuVM> GetVMExtended(Expression<Func<RoleTopMenu, bool>> filter = null, Func<IQueryable<RoleTopMenu>, IOrderedQueryable<RoleTopMenu>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<RoleTopMenu, object>>[] includes)
        {
            var q = CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection).Select(entityToModel);

            return q.ToList();
        }

        public List<Role_List_Result> GetUserRoleDetails(Guid? tbsUserId, long? userId = 0)
        {
            return EntityForSP.Role_List(userId).ToList();
        }

        //public List<DistributedAuthoritiesByUserList_Result> GetDistributedAuthoritiesByUserList(long? userId = 0)
        //{
        //    return EntityForSP.DistributedAuthoritiesByUserList(userId).ToList();
        //}

        public IEnumerable<RoleTopMenuVM> Post(List<RoleTopMenuVM> roleTopMenus, HttpRequestInfo info)
        {
            var addedRoles = new List<RoleTopMenu>();

            roleTopMenus.ForEach(roleTopMenu =>
            {
                var entity = modelToEntity.Compile()(roleTopMenu);
                
                    entity.OperationIsDeleted = (short)Active;
                    entity.OperationIP = info.IpAddress;
                    entity.OperationIdUserRef = info.UserID;
                    entity.OperationDate = DateTime.Now;

                    CRUD.Insert(entity);
                    addedRoles.Add(entity);
            });

            Save();

            return addedRoles.Select(entityToModel.Compile());
        }

        public IEnumerable<RoleTopMenuVM> Update(List<RoleTopMenuVM> roleTopMenus, HttpRequestInfo info)
        {
            var addedRoles = new List<RoleTopMenu>();

            roleTopMenus.ForEach(roleTopMenu =>
            {
                if (roleTopMenu.IdRoleTopMenu == 0) throw new System.Exception("IdRoleTopMenu alanı her zaman dolu olmalıdır.");

                var entity = modelToEntity.Compile()(roleTopMenu);

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

        public IEnumerable<RoleTopMenuVM> Delete(List<long> ids, HttpRequestInfo info)
        {
            var deletes = GetVM(q => ids.Any(id => id == q.IdRoleTopMenu));

            var result = Delete(deletes, info);

            return result;
        }
        
        public IEnumerable<RoleTopMenuVM> Delete(List<RoleTopMenuVM> roleTopMenus, HttpRequestInfo info)
        {
            var addedRoles = new List<RoleTopMenu>();

            roleTopMenus.ForEach(roleTopMenu =>
            {
                var entity = CRUD.GetById(roleTopMenu.IdRoleTopMenu);

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
