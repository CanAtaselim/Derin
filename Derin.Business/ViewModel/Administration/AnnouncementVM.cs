using Derin.Business.ViewModel.Base;
using Derin.Data.Model;
using Derin.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Business.ViewModel.Administration
{
    public class AnnouncementVM : BaseVM
    {
        public long IdAnnouncement { get; set; }
        [Required(ErrorMessage = "Bu Alan Gereklidir!")]
        [MinLength(5, ErrorMessage = "En Az 5 Karakter Girmelisiniz!")]
        //[RequiredIf("MessageSubject","abcdefghiii",ErrorMessage ="blasblas")]
        //[RequiredIfTrue("IsVisibleToMain",ErrorMessage ="bla bla")]
        public string MessageSubject { get; set; }
        [Required(ErrorMessage = "Bu Alan Gereklidir!")]
        [MinLength(10, ErrorMessage = "En Az 10 Karakter Girmelisiniz!")]
        public string MessageContent { get; set; }
        [Required(ErrorMessage = "Bu Alan Gereklidir!")]
        public DateTime? MessageDate { get; set; }
        public string MessageIcon { get; set; }
        [Required(ErrorMessage = "Bu Alan Gereklidir!")]
        public _Enumeration._AnnouncementPriority Priority { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Bu Alan Gereklidir!")]
        public string Area { get; set; }
        public bool IsVisibleToMain { get; set; }
    }
}
