using Derin.Business.ViewModel.Base;


namespace Derin.Business.ViewModel.Administration
{
    public class VillageVM : BaseVM
    {
        public long IdVillage { get; set; }
        public int? TakbisVillageCode { get; set; }
        public string VillageName { get; set; }
        public string TownName { get; set; }
        public string CityName { get; set; }
        public double? XMinPoint { get; set; }
        public double? XMaxPoint { get; set; }
        public double? YMinPoint { get; set; }
        public double? YMaxPoint { get; set; }
        public string TotalName { get; set; }
        public long? IdTownRef { get; set; }
        public long? IdCityRef { get; set; }
    }
}
