using Derin.Business.ViewModel.Base;


namespace Derin.Business.ViewModel.Administration
{
    public class CountryVM : BaseVM
    {
        public long IdCountry { get; set; }
        public string CountryName { get; set; }
        public string CountryNameEng { get; set; }
        public string ISOCodeA2 { get; set; }
        public string CountryGroup { get; set; }
    }
}
