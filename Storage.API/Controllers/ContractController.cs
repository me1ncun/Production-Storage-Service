using Equipment_Storage_Service.Attributes;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Exceptions;
using Storage.Application.Models;
using Storage.Application.Services;

namespace Equipment_Storage_Service.Controllers;

[ApiController]
[Route("/api/v1")]
public class ContractController : ControllerBase
{
    private readonly IContractService _contractService;
    private readonly IValidator<CreateContractModel> _validator;

    public ContractController(
        IContractService contractService,
        IValidator<CreateContractModel> validator)
    {
        _contractService = contractService;
        _validator = validator;
    }
    
    [HttpGet("/contracts")]
    [KeyRequired]
    [ProducesResponseType(type: typeof(IEnumerable<ContractResponseModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllContracts()
    {
        try
        {
            var contracts = await _contractService.GetAllAsync();

            return Ok(contracts);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost("/contracts")]
    [KeyRequired]
    [ProducesResponseType(type: typeof(IEnumerable<ContractResponseModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddContract([FromBody] CreateContractModel contractModel)
    {
        try
        {
            var validationResult = _validator.Validate(contractModel);
            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = validationResult.Errors });
            }

            await _contractService.CreateAsync(contractModel);

            return Ok();
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InsufficientSpaceException ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);;
        }
    }
}