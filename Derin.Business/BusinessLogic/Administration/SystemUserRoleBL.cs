using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Derin.Data.UnitOfWork.Derin;
using Derin.Data.Repository;
using System.Transactions;
using Derin.Data.DataCommon;

namespace Derin.Business.BusinessLogic
{
    public class SystemUserRoleBL : BaseBL<SystemUserRole, SystemUserRoleVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<SystemUserRole> CRUD;
        public SystemUserRoleBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.SystemUserRoleRepository;
        }
        public override List<SystemUserRoleVM> GetVM(Expression<Func<SystemUserRole, bool>> filter = null, Func<IQueryable<SystemUserRole>, IOrderedQueryable<SystemUserRole>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<SystemUserRole, object>>[] includes)
        {
            Expression<Func<SystemUserRole, object>>[] elem = new Expression<Func<SystemUserRole, object>>[] {
                    i=>i.Role,
                    i=>i.SystemUser,
                    i=>i.SystemUserRoleLocation
            };
            return CRUD.Query(filter, orderBy, take, skip, elem).Select(
               x => new SystemUserRoleVM
               {
                   IdParentRole = x.Role.IdParentRoleRef,
                   IdRole = x.IdRoleRef,
                   IdUser = x.IdSystemUserRef,
                   LocationAdmin = x.Role.LocationAdmin,
                   RoleCode = x.Role.RoleCode,
                   RoleName = x.Role.RoleName
               }
               ).ToList();
        }
        public List<SystemUserRoleVM> GetVMExtended(Expression<Func<SystemUserRole, bool>> filter = null, Func<IQueryable<SystemUserRole>, IOrderedQueryable<SystemUserRole>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<SystemUserRole, object>>[] includes)
        {
            Expression<Func<SystemUserRole, object>>[] elem = new Expression<Func<SystemUserRole, object>>[] {
                    i=>i.Role,
                    i=>i.SystemUser,
                    i=>i.SystemUserRoleLocation
            };
            return CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection, elem).Select(
               x => new SystemUserRoleVM
               {
                   IdParentRole = x.Role.IdParentRoleRef,
                   IdRole = x.IdRoleRef,
                   IdUser = x.IdSystemUserRef,
                   LocationAdmin = x.Role.LocationAdmin,
                   RoleCode = x.Role.RoleCode,
                   RoleName = x.Role.RoleName
               }
               ).ToList();
        }

        public bool AddUserRole(AddUserRoleVM item, HttpRequestInfo info)
        {
            Locator.AdministrationBLLocator _locator = new Locator.AdministrationBLLocator();
            var sur = CRUD.Query(x => x.IdRoleRef == item.IdRole && x.IdSystemUserRef == item.IdUser).FirstOrDefault();
            SystemUserRoleLocation surl = null;
            if (item.IdCity != null && sur != null)
            {
                surl = _locator.SystemUserRoleLocationBL.CRUD.Query(x => x.IdSystemUserRoleRef == sur.IdSystemUserRole && x.IdCityRef == item.IdCity && x.IdTownRef == item.IdTown && x.IdVillageRef == item.IdVillage).FirstOrDefault();
            }
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    if (sur != null)
                    {
                        sur.OperationIsDeleted = 1;
                        sur.OperationIP = info.IpAddress;
                        sur.OperationIdUserRef = info.UserID;
                        sur.OperationDate = DateTime.Now;
                        CRUD.Update(sur, info);
                        Save();
                    }
                    else
                    {
                        sur = new SystemUserRole();
                        sur.IdRoleRef = item.IdRole;
                        sur.IdSystemUserRef = item.IdUser;
                        sur.OperationDate = DateTime.Now;
                        sur.OperationIdUserRef = info.UserID;
                        sur.OperationIP = info.IpAddress;
                        sur.OperationIsDeleted = 1;
                        CRUD.Insert(sur);
                        Save();
                    }
                    if (item.IdCity != null)
                    {
                        if (surl != null)
                        {
                            surl.OperationIsDeleted = 1;
                            surl.OperationIP = info.IpAddress;
                            surl.OperationIdUserRef = info.UserID;
                            surl.OperationDate = DateTime.Now;
                            _locator.SystemUserRoleLocationBL.CRUD.Update(surl, info);
                            _locator.SystemUserRoleLocationBL.Save();
                        }
                        else
                        {
                            surl = new SystemUserRoleLocation();
                            surl.IdCityRef = item.IdCity;
                            surl.IdSystemUserRef = item.IdUser;
                            surl.IdSystemUserRoleRef = sur.IdSystemUserRole;
                            surl.IdTownRef = item.IdTown;
                            surl.IdVillageRef = item.IdVillage;
                            surl.OperationDate = DateTime.Now;
                            surl.OperationIdUserRef = info.UserID;
                            surl.OperationIP = info.IpAddress;
                            surl.OperationIsDeleted = 1;
                            _locator.SystemUserRoleLocationBL.CRUD.Insert(surl);
                            _locator.SystemUserRoleLocationBL.Save();
                        }
                        scope.Complete();

                    }
                    else
                    {
                        scope.Complete();
                    }
                    return true;
                }
                catch (System.Exception ex)
                {
                    scope.Dispose();
                    return false;
                }

            }
        }
        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
