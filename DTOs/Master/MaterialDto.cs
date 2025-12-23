namespace ConstructionFinance.API.DTOs.Master
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DefaultUnitId { get; set; }
        public decimal? DefaultRate { get; set; }
    }

}
