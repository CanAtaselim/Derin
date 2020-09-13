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
    public class RoleSideMenuBL : BaseBL<RoleSideMenu, RoleSideMenuVM>
    {
        private IUnitOfWork _unitOfWork;

        public IGenericRepository<RoleSideMenu> CRUD;

        private Expression<Func<RoleSideMenu, RoleSideMenuVM>> entityToModel = x => new RoleSideMenuVM
        {
            IdRoleRef = x.IdRoleRef ?? 0,
            IdRoleSideMenu = x.IdRoleSideMenu,
            IdSideMenuRef = x.IdSideMenuRef ?? 0,
            RoleName = x.Role.RoleName,
            SideMenuName = x.SideMenu == null ? null : x.SideMenu.SideMenuName
        };

        private Expression<Func<RoleSideMenuVM, RoleSideMenu>> modelToEntity = x => new RoleSideMenu
        {
            IdRoleRef = x.IdRoleRef,
            IdRoleSideMenu = x.IdRoleSideMenu,
            IdSideMenuRef = x.IdSideMenuRef,
        };

        public RoleSideMenuBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.RoleSideMenuRepository;
        }

        public override List<RoleSideMenuVM> GetVM(Expression<Func<RoleSideMenu, bool>> filter = null, Func<IQueryable<RoleSideMenu>, IOrderedQueryable<RoleSideMenu>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<RoleSideMenu, object>>[] includes)
        {
            return CRUD.Get(filter, orderBy, take, skip, includes).Select(entityToModel.Compile()).ToList();
        }

        public List<RoleSideMenuVM> GetVMExtended(Expression<Func<RoleSideMenu, bool>> filter = null, Func<IQueryable<RoleSideMenu>, IOrderedQueryable<RoleSideMenu>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<RoleSideMenu, object>>[] includes)
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

        public IEnumerable<RoleSideMenuVM> Post(List<RoleSideMenuVM> roleSideMenus, HttpRequestInfo info)
        {
            var addedRoles = new List<RoleSideMenu>();

            roleSideMenus.ForEach(roleSideMenu =>
            {
                var entity = modelToEntity.Compile()(roleSideMenu);
                
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

        public IEnumerable<RoleSideMenuVM> Update(List<RoleSideMenuVM> roleSideMenus, HttpRequestInfo info)
        {
            var addedRoles = new List<RoleSideMenu>();

            roleSideMenus.ForEach(roleSideMenu =>
            {
                if (roleSideMenu.IdRoleSideMenu == 0) throw new System.Exception("IdRoleSideMenu alanı her zaman dolu olmalıdır.");

                var entity = modelToEntity.Compile()(roleSideMenu);

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

        public IEnumerable<RoleSideMenuVM> Delete(List<long> ids, HttpRequestInfo info)
        {
            var deletes = GetVM(q => ids.Any(id => id == q.IdRoleSideMenu));

            var result = Delete(deletes, info);

            return result;
        }
        
        public IEnumerable<RoleSideMenuVM> Delete(List<RoleSideMenuVM> roleSideMenus, HttpRequestInfo info)
        {
            var addedRoles = new List<RoleSideMenu>();

            roleSideMenus.ForEach(roleSideMenu =>
            {
                var entity = CRUD.GetById(roleSideMenu.IdRoleSideMenu);

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
