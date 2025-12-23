namespace ConstructionFinance.API.DTOs.Master
{
    public class CreateBOQMasterDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int? DefaultUnitId { get; set; }
        public decimal? DefaultRate { get; set; }
        public string Description { get; set; }
    }

}
