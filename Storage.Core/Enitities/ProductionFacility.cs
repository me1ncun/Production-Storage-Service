namespace Storage.Core.Enitities;

public class ProductionFacility
{
    public int Code { get; set; }
    public string Name { get; set; }
    public float StandartArea { get; set; }
    public ICollection<Contract> Contracts { get; set; }
}