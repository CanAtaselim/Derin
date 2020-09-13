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
    public class TopMenuBL : BaseBL<TopMenu, TopMenuVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<TopMenu> CRUD;

        public TopMenuBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.TopMenuRepository;
        }

        private Expression<Func<TopMenu,TopMenuVM>> entityToModel = x => new TopMenuVM
        {
            IdTopMenu = x.IdTopMenu,
            OperationIdUserRef = x.OperationIdUserRef,
            OperationIP = x.OperationIP,
            OperationIsDeleted = x.OperationIsDeleted,
            Area = x.Area,
            Controller = x.Controller,
            Action = x.Action,
            TopMenuName = x.TopMenuName,
            TopMenuDescription = x.TopMenuDescription,
            TopMenuStatus = x.TopMenuStatus,
            TopMenuOrder = x.TopMenuOrder,
            IconUrl = x.IconUrl,
            ItemBackgroundColor = x.ItemBackgroundColor,
            ItemTextColor = x.ItemTextColor,
            ItemIconColor = x.ItemIconColor,
        };

        private Expression<Func<TopMenuVM, TopMenu>> modelToEntity = x => new TopMenu
        {
            IdTopMenu = x.IdTopMenu,
            OperationIdUserRef = x.OperationIdUserRef,
            OperationIP = x.OperationIP,
            OperationIsDeleted = x.OperationIsDeleted,
            Area = x.Area,
            Controller = x.Controller,
            Action = x.Action,
            TopMenuName = x.TopMenuName,
            TopMenuDescription = x.TopMenuDescription,
            TopMenuStatus = x.TopMenuStatus,
            TopMenuOrder = x.TopMenuOrder,
            IconUrl = x.IconUrl,
            ItemBackgroundColor = x.ItemBackgroundColor,
            ItemTextColor = x.ItemTextColor,
            ItemIconColor = x.ItemIconColor,

        };

        //TopMenulerin DropDownList için döndürülmesi
        public IEnumerable<SelectListItem> GetTopMenuSelectList()
        {
            return CRUD.Query(q => q.OperationIsDeleted == (short)Active).Select(q => new SelectListItem {
                Text = q.TopMenuName,
                Value = q.IdTopMenu.ToString()
            });
        }

        public override List<TopMenuVM> GetVM(Expression<Func<TopMenu, bool>> filter = null, Func<IQueryable<TopMenu>, IOrderedQueryable<TopMenu>> orderBy = null, int? take = default(int?), int? skip = default(int?), params Expression<Func<TopMenu, object>>[] includes)
        {
            return CRUD.Get(filter, orderBy, take, skip, includes).Select(entityToModel.Compile()).ToList();
        }

        public List<TopMenuVM> GetVMExtended(Expression<Func<TopMenu, bool>> filter = null, Func<IQueryable<TopMenu>, IOrderedQueryable<TopMenu>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<TopMenu, object>>[] includes)
        {
            var q = CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection).Select(entityToModel);

            return q.ToList();
        }

        //public List<SystemUserTopMenu_List_Result> GetTopMenuWithPermission(long idUserRef)
        //{
        //    return EntityForSP.SystemUserTopMenu_List(idUserRef).ToList();
        //}

        public IEnumerable<TopMenuVM> Post(List<TopMenuVM> topMenus, HttpRequestInfo info)
        {
            var addedTopMenus = new List<TopMenu>();

            topMenus.ForEach(topMenu =>
            {
                var entity = modelToEntity.Compile()(topMenu);

                entity.OperationIsDeleted = (short)Active;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                CRUD.Insert(entity);
                addedTopMenus.Add(entity);
            });

            Save();

            return addedTopMenus.Select(entityToModel.Compile());
        }

        public IEnumerable<TopMenuVM> Update(List<TopMenuVM> topMenus, HttpRequestInfo info)
        {
            var addedTopMenus = new List<TopMenu>();

            topMenus.ForEach(topMenu =>
            {
                if (topMenu.IdTopMenu == 0) throw new System.Exception("IdTopMenu alanı her zaman dolu olmalıdır.");

                var entity = modelToEntity.Compile()(topMenu);

                entity.OperationIsDeleted = (short)Active;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                CRUD.Update(entity, info);
                addedTopMenus.Add(entity);
            });

            Save();

            return addedTopMenus.Select(entityToModel.Compile());
        }

        public IEnumerable<TopMenuVM> Delete(List<long> ids, HttpRequestInfo info)
        {
            var deletes = GetVM(q => ids.Any(id => id == q.IdTopMenu));

            var result = Delete(deletes, info);

            return result;
        }

        public IEnumerable<TopMenuVM> Delete(List<TopMenuVM> topMenus, HttpRequestInfo info)
        {
            var addedTopMenus = new List<TopMenu>();

            topMenus.ForEach(topMenu =>
            {
                var entity = CRUD.GetById(topMenu.IdTopMenu);

                entity.OperationIsDeleted = (short)Deleted;
                entity.OperationIP = info.IpAddress;
                entity.OperationIdUserRef = info.UserID;
                entity.OperationDate = DateTime.Now;

                //CRUD.Update(entity, info);

                addedTopMenus.Add(entity);
            });

            Save();

            return addedTopMenus.Select(entityToModel.Compile());
        }
        
        public override void Save()
        {
            _unitOfWork.Save();
        }


    }
}
