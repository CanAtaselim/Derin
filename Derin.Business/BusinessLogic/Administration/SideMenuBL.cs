using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.UnitOfWork.Derin;
using Derin.Data.Repository;
using static Derin.Common._Enumeration.IsOperationDeleted;
using System.Web.Mvc;
using Derin.Data.DataCommon;

namespace Derin.Business.BusinessLogic.Administration
{
    public class SideMenuBL : BaseBL<SideMenu, SideMenuVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<SideMenu> CRUD;

        public SideMenuBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.SideMenuRepository;
        }

        private Expression<Func<SideMenu, SideMenuVM>> entityToModel = x => new SideMenuVM
        {
            IdSideMenu = x.IdSideMenu,
            OperationIdUserRef = x.OperationIdUserRef,
            OperationIP = x.OperationIP,
            OperationIsDeleted = x.OperationIsDeleted,
            IdParentSideMenu = x.IdParentSideMenu,
            IdTopMenuRef = x.IdTopMenuRef,
            Area = x.Area,
            Controller = x.Controller,
            Action = x.Action,
            SideMenuName = x.SideMenuName,
            SideMenuDescription = x.SideMenuDescription,
            SideMenuStatus = x.SideMenuStatus,
            SideMenuOrder = x.SideMenuOrder,
            IconUrl = x.IconUrl,
            ItemBackgroundColor = x.ItemBackgroundColor,
            ItemTextColor = x.ItemTextColor,
            ItemIconColor = x.ItemIconColor,
        };

        private Expression<Func<SideMenuVM, SideMenu>> modelToEntity = x => new SideMenu
        {
            IdSideMenu = x.IdSideMenu,
            OperationIdUserRef = x.OperationIdUserRef,
            OperationIP = x.OperationIP,
            OperationIsDeleted = x.OperationIsDeleted,
            IdParentSideMenu = x.IdParentSideMenu,
            IdTopMenuRef = x.IdTopMenuRef,
            Area = x.Area,
            Controller = x.Controller,
            Action = x.Action,
            SideMenuName = x.SideMenuName,
            SideMenuDescription = x.SideMenuDescription,
            SideMenuStatus = x.SideMenuStatus,
            SideMenuOrder = x.SideMenuOrder,
            IconUrl = x.IconUrl,
            ItemBackgroundColor = x.ItemBackgroundColor,
            ItemTextColor = x.ItemTextColor,
            ItemIconColor = x.ItemIconColor,

        };

        public override List<SideMenuVM> GetVM(Expression<Func<SideMenu, bool>> filter = null, Func<IQueryable<SideMenu>, IOrderedQueryable<SideMenu>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<SideMenu, object>>[] includes)
        {
            return CRUD.Get(filter, orderBy, take, skip, includes).Select(entityToModel.Compile()).ToList();
        }

        public List<SideMenuVM> GetVMExtended(Expression<Func<SideMenu, bool>> filter = null, Func<IQueryable<SideMenu>, IOrderedQueryable<SideMenu>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<SideMenu, object>>[] includes)
        {
            var q = CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection).Select(entityToModel);

            return q.ToList();
        }

        //SideMenulerin DropDownList için döndürülmesi
        public IEnumerable<SelectListItem> GetSideMenuSelectList(long idParentTopMenu)
        {
            return CRUD.Query(q => 
                q.OperationIsDeleted == (short)Active &&
                q.IdTopMenuRef == idParentTopMenu
            ).Select(q => new SelectListItem
            {
                Text = q.SideMenuName,
                Value = q.IdSideMenu.ToString()
            });
        }

        //public List<SystemUserSideMenu_List_Result> GetSideMenuWithPermission(long? idTopMenuRef,long idUserRef)
        //{
        //    return EntityForSP.SystemUserSideMenu_List(idUserRef,idTopMenuRef).OrderBy(q => q.SideMenuOrder).ToList();
        //}

        public IEnumerable<SideMenuVM> Post(List<SideMenuVM> sideMenus, HttpRequestInfo info)
        {
            var addedSideMenus = new List<SideMenu>();

            sideMenus.ForEach(sideMenu =>
            {
                var entity = modelToEntity.Compile()(sideMenu);

                entity.OperationIsDeleted = (short)Active;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                CRUD.Insert(entity);
                addedSideMenus.Add(entity);
            });

            Save();

            return addedSideMenus.Select(entityToModel.Compile());
        }

        public IEnumerable<SideMenuVM> Update(List<SideMenuVM> sideMenus, HttpRequestInfo info)
        {
            var addedSideMenus = new List<SideMenu>();

            sideMenus.ForEach(sideMenu =>
            {
                if (sideMenu.IdSideMenu == 0) throw new System.Exception("IdSideMenu alanı her zaman dolu olmalıdır.");

                var entity = modelToEntity.Compile()(sideMenu);

                entity.OperationIsDeleted = (short)Active;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                CRUD.Update(entity, info);
                addedSideMenus.Add(entity);
            });

            Save();

            return addedSideMenus.Select(entityToModel.Compile());
        }

        public IEnumerable<SideMenuVM> Delete(List<long> ids, HttpRequestInfo info)
        {
            var deletes = GetVM(q => ids.Any(id => id == q.IdSideMenu));

            var result = Delete(deletes, info);

            return result;
        }

        public IEnumerable<SideMenuVM> Delete(List<SideMenuVM> sideMenus, HttpRequestInfo info)
        {
            var addedSideMenus = new List<SideMenu>();

            sideMenus.ForEach(topMenu =>
            {
                var entity = CRUD.GetById(topMenu.IdSideMenu);

                entity.OperationIsDeleted = (short)Deleted;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                //CRUD.Update(entity, info);

                addedSideMenus.Add(entity);
            });

            Save();

            return addedSideMenus.Select(entityToModel.Compile());
        }
        
        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
