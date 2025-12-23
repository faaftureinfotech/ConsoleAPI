namespace ConstructionFinance.API.Models.Master
{
    public class BOQMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int? DefaultUnitId { get; set; }
        public Unit DefaultUnit { get; set; }

        public decimal? DefaultRate { get; set; }
        public string Description { get; set; }
    }

}
