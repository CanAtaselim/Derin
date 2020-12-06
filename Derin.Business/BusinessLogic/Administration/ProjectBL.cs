using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using Derin.Data.Repository;
using Derin.Data.UnitOfWork.Derin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Business.BusinessLogic.Administration
{
    public class ProjectBL : BaseBL<Project, ProjectVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<Project> CRUD;

        public ProjectBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.ProjectRepository;
        }

        public override List<ProjectVM> GetVM(Expression<Func<Project, bool>> filter = null, Func<IQueryable<Project>, IOrderedQueryable<Project>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<Project, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy, take, skip, includes).Select(x => new ProjectVM
            {
                IdProject = x.IdProject,
                Title = x.Title,
                Detail = x.Detail,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Picture = x.Picture

            }).ToList();
        }
        public List<ProjectVM> GetVMExtended(Expression<Func<Project, bool>> filter = null, Func<IQueryable<Project>, IOrderedQueryable<Project>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<Project, object>>[] includes)
        {
            return CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection, includes).Select(x => new ProjectVM
            {
                IdProject = x.IdProject,
                Title = x.Title,
                Detail = x.Detail,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Picture = x.Picture

            }).ToList();
        }
        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
