namespace Storage.Application.Models;

public class CreateContractModel
{
    public int Id { get; set; }
    public int FacilityCode { get; set; }
    public int EquipmentCode { get; set; }
    public int EquipmentQuantity { get; set; }
}