using Derin.Data.Model;
using Derin.Data.Repository;

namespace Derin.Data.UnitOfWork.Derin
{
    public interface IUnitOfWork
    {
        #region Administration Repos
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<RoleTopMenu> RoleTopMenuRepository { get; }
        IGenericRepository<RoleAuthorization> RoleAuthorizationRepository { get; }
        IGenericRepository<RoleSideMenu> RoleSideMenuRepository { get; }
        IGenericRepository<CDC> CDCRepository { get; }
        IGenericRepository<Country> CountryRepository { get; }
        IGenericRepository<City> CityRepository { get; }
        IGenericRepository<Town> TownRepository { get; }
        IGenericRepository<Village> VillageRepository { get; }
        IGenericRepository<TopMenu> TopMenuRepository { get; }
        IGenericRepository<SideMenu> SideMenuRepository { get; }
        IGenericRepository<SystemUser> SystemUserRepository { get; }
        IGenericRepository<SystemUserRole> SystemUserRoleRepository { get; }
        IGenericRepository<SystemUserRoleLocation> SystemUserRoleLocationRepository { get; }
        IGenericRepository<ANNOUNCEMENT> AnnouncementRepository { get; }
        IGenericRepository<ConnectionLog> ConnectionLogRepository { get; }
        IGenericRepository<SystemUserTicket> SystemUserTicketRepository { get; }
        IGenericRepository<ExceptionFeedBack> ExceptionFeedBackRepository { get; }
        IGenericRepository<AboutUs> AboutUsRepository { get; }
        IGenericRepository<Person> PersonRepository { get; }
        IGenericRepository<ContactUs> ContactUsRepository { get; }
        IGenericRepository<Services> ServicesRepository { get; }
        IGenericRepository<Banner> BannerRepository { get; }

        #endregion 

        void Save();
        void SaveBulk();
    }
}
