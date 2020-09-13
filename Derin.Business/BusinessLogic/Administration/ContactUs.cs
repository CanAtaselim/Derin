using Derin.Business.BusinessLogic.Base;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using Derin.Data.Repository;
using Derin.Data.UnitOfWork.Derin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Derin.Business.BusinessLogic.Administration
{

    public class ContactUsBL : BaseBL<ContactUs, ContactUsVM>
    {
        private IUnitOfWork _unitOfWork;
        public IGenericRepository<ContactUs> CRUD;

        public ContactUsBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CRUD = _unitOfWork.ContactUsRepository;
        }

        public override List<ContactUsVM> GetVM(Expression<Func<ContactUs, bool>> filter = null, Func<IQueryable<ContactUs>, IOrderedQueryable<ContactUs>> orderBy = null, int? take = null, int? skip = null, params Expression<Func<ContactUs, object>>[] includes)
        {
            return CRUD.Query(filter, orderBy, take, skip, includes).Select(x => new ContactUsVM
            {
                IdContactUs = x.IdContactUs,
                Address = x.Address,
                Email = x.Email,
                Phone = x.Phone,
                GSM = x.GSM,
                Fax = x.Fax,
                Facebook = x.Facebook,
                Twitter = x.Twitter,
                Instagram = x.Instagram,
                Youtube = x.Youtube,
                Linkedin = x.Linkedin,
                GooglePlus = x.GooglePlus,
                Department = x.Department

            }).ToList();
        }
        public List<ContactUsVM> GetVMExtended(Expression<Func<ContactUs, bool>> filter = null, Func<IQueryable<ContactUs>, IOrderedQueryable<ContactUs>> orderBy = null, int? take = default(int?), int? skip = default(int?), string orderByS = null, short? orderByDirection = null, params Expression<Func<ContactUs, object>>[] includes)
        {
            return CRUD.QueryExtended(filter, orderBy, take, skip, orderByS, orderByDirection, includes).Select(x => new ContactUsVM
            {
                IdContactUs = x.IdContactUs,
                Address = x.Address,
                Email = x.Email,
                Phone = x.Phone,
                GSM = x.GSM,
                Fax = x.Fax,
                Facebook = x.Facebook,
                Twitter = x.Twitter,
                Instagram = x.Instagram,
                Youtube = x.Youtube,
                Linkedin = x.Linkedin,
                GooglePlus = x.GooglePlus,
                Department = x.Department
            }).ToList();
        }
        public override void Save()
        {
            _unitOfWork.Save();
        }
    }
}
