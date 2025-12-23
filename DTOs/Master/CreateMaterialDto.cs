namespace ConstructionFinance.API.DTOs.Master
{
    public class CreateMaterialDto
    {
        public string Name { get; set; }
        public int? DefaultUnitId { get; set; }
        public decimal? DefaultRate { get; set; }
    }

}
