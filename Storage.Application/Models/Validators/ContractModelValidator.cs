using FluentValidation;

namespace Storage.Application.Models.Validators;

public class ContractModelValidator : AbstractValidator<CreateContractModel>
{
    public ContractModelValidator()
    {
        RuleFor(x => x.EquipmentCode)
            .NotEmpty()
            .WithMessage("Process equipment code is required");

        RuleFor(x => x.FacilityCode)
            .NotEmpty()
            .WithMessage("Production facility code is required");

        RuleFor(x => x.EquipmentQuantity)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Equipment quantity is required and must be greater than 0");
    }
}