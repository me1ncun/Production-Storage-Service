namespace Storage.Core.Enitities;

public class Contract
{
    public int Id { get; set; }
    public int FacilityId { get; set; }
    public ProductionFacility ProductionFacility { get; set; }
    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
    public int Quantity { get; set; }
}