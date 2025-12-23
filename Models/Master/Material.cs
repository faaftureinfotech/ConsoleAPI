namespace ConstructionFinance.API.Models.Master
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DefaultUnitId { get; set; }
        public Unit DefaultUnit { get; set; }
        public decimal? DefaultRate { get; set; }
    }

}
