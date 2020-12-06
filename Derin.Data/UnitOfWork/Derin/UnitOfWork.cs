using Derin.Data.Model;
using Derin.Data.Repository;


namespace Derin.Data.UnitOfWork.Derin
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Administration Repos

        private IGenericRepository<SystemUserTicket> _systemUserTicketRepository;
        public IGenericRepository<SystemUserTicket> SystemUserTicketRepository
        {
            get { return _systemUserTicketRepository ?? (_systemUserTicketRepository = new GenericRepository<SystemUserTicket>(_context)); }
        }
        private IGenericRepository<SystemUserRole> _systemUserRoleRepository;
        public IGenericRepository<SystemUserRole> SystemUserRoleRepository
        {
            get { return _systemUserRoleRepository ?? (_systemUserRoleRepository = new GenericRepository<SystemUserRole>(_context)); }
        }
        private IGenericRepository<SystemUserRoleLocation> _systemUserRoleLocationRepository;
        public IGenericRepository<SystemUserRoleLocation> SystemUserRoleLocationRepository
        {
            get { return _systemUserRoleLocationRepository ?? (_systemUserRoleLocationRepository = new GenericRepository<SystemUserRoleLocation>(_context)); }
        }
        private IGenericRepository<ConnectionLog> _connectionLogRepository;
        public IGenericRepository<ConnectionLog> ConnectionLogRepository
        {
            get { return _connectionLogRepository ?? (_connectionLogRepository = new GenericRepository<ConnectionLog>(_context)); }
        }
        private IGenericRepository<ANNOUNCEMENT> _announcementRepository;
        public IGenericRepository<ANNOUNCEMENT> AnnouncementRepository
        {
            get { return _announcementRepository ?? (_announcementRepository = new GenericRepository<ANNOUNCEMENT>(_context)); }
        }
        private IGenericRepository<City> _cityRepository;
        public IGenericRepository<City> CityRepository
        {
            get { return _cityRepository ?? (_cityRepository = new GenericRepository<City>(_context)); }
        }
        private IGenericRepository<SystemUser> _systemUserRepository;
        public IGenericRepository<SystemUser> SystemUserRepository
        {
            get { return _systemUserRepository ?? (_systemUserRepository = new GenericRepository<SystemUser>(_context)); }
        }
        private IGenericRepository<Country> _countryRepository;
        public IGenericRepository<Country> CountryRepository
        {
            get { return _countryRepository ?? (_countryRepository = new GenericRepository<Country>(_context)); }
        }

        private IGenericRepository<Town> _townRepository;
        public IGenericRepository<Town> TownRepository
        {
            get { return _townRepository ?? (_townRepository = new GenericRepository<Town>(_context)); }
        }

        private IGenericRepository<Role> _roleRepository;
        public IGenericRepository<Role> RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new GenericRepository<Role>(_context)); }
        }

        private IGenericRepository<RoleAuthorization> _roleAuthorizationRepository;
        public IGenericRepository<RoleAuthorization> RoleAuthorizationRepository
        {
            get { return _roleAuthorizationRepository ?? (_roleAuthorizationRepository = new GenericRepository<RoleAuthorization>(_context)); }
        }

        private IGenericRepository<RoleTopMenu> _roleTopMenuRepository;
        public IGenericRepository<RoleTopMenu> RoleTopMenuRepository
        {
            get { return _roleTopMenuRepository ?? (_roleTopMenuRepository = new GenericRepository<RoleTopMenu>(_context)); }
        }

        private IGenericRepository<RoleSideMenu> _roleSideMenuRepository;
        public IGenericRepository<RoleSideMenu> RoleSideMenuRepository
        {
            get { return _roleSideMenuRepository ?? (_roleSideMenuRepository = new GenericRepository<RoleSideMenu>(_context)); }
        }


        private IGenericRepository<TopMenu> _topMenuRepository;
        public IGenericRepository<TopMenu> TopMenuRepository
        {
            get { return _topMenuRepository ?? (_topMenuRepository = new GenericRepository<TopMenu>(_context)); }
        }

        private IGenericRepository<SideMenu> _sideMenuRepository;
        public IGenericRepository<SideMenu> SideMenuRepository
        {
            get { return _sideMenuRepository ?? (_sideMenuRepository = new GenericRepository<SideMenu>(_context)); }
        }
        private IGenericRepository<ExceptionFeedBack> _exceptionFeedBackRepository;
        public IGenericRepository<ExceptionFeedBack> ExceptionFeedBackRepository
        {
            get { return _exceptionFeedBackRepository ?? (_exceptionFeedBackRepository = new GenericRepository<ExceptionFeedBack>(_context)); }
        }
        private IGenericRepository<CDC> _cdcRepository;
        public IGenericRepository<CDC> CDCRepository
        {
            get { return _cdcRepository ?? (_cdcRepository = new GenericRepository<CDC>(_context)); }
        }
        private IGenericRepository<Village> _villageRepository;
        public IGenericRepository<Village> VillageRepository => _villageRepository ?? (_villageRepository = new GenericRepository<Village>(_context));

        private IGenericRepository<AboutUs> _aboutUsRepository;
        public IGenericRepository<AboutUs> AboutUsRepository => _aboutUsRepository ?? (_aboutUsRepository = new GenericRepository<AboutUs>(_context));

        private IGenericRepository<Person> _personRepository;
        public IGenericRepository<Person> PersonRepository => _personRepository ?? (_personRepository = new GenericRepository<Person>(_context));


        private IGenericRepository<ContactUs> _contactUsRepository;
        public IGenericRepository<ContactUs> ContactUsRepository => _contactUsRepository ?? (_contactUsRepository = new GenericRepository<ContactUs>(_context));

        private IGenericRepository<Services> _servicesRepository;
        public IGenericRepository<Services> ServicesRepository => _servicesRepository ?? (_servicesRepository = new GenericRepository<Services>(_context));

        private IGenericRepository<Banner> _bannerRepository;
        public IGenericRepository<Banner> BannerRepository => _bannerRepository ?? (_bannerRepository = new GenericRepository<Banner>(_context));

        private IGenericRepository<Project> _projectRepository;
        public IGenericRepository<Project> ProjectRepository => _projectRepository ?? (_projectRepository = new GenericRepository<Project>(_context));


        #endregion

        private readonly DerinEntities _context;
        public UnitOfWork(DerinEntities context)
        {
            _context = context;
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveBulk()
        {
            _context.BulkSaveChanges();

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
