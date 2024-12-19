using Equipment_Storage_Service.Controllers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Storage.Application.Exceptions;
using Storage.Application.Models;
using Storage.Application.Services;

namespace Storage.Api.UnitTests;

public class ContractControllerTests
{
    private readonly Mock<IContractService> _contractServiceMock;
    private readonly Mock<IValidator<CreateContractModel>> _createContractValidatorMock;
    private readonly ContractController _controller;

    public ContractControllerTests()
    {
        _contractServiceMock = new Mock<IContractService>();
        _createContractValidatorMock = new Mock<IValidator<CreateContractModel>>();
        _controller = new ContractController(
            _contractServiceMock.Object,
            _createContractValidatorMock.Object
        );
    }

    [Fact]
    public async Task GetAllContracts_ReturnsOkResultWithContracts()
    {
        // Arrange
        var contracts = new List<ContractResponseModel>
        {
            new ContractResponseModel
                { Id = 1, FacilityName = "Tech Park", EquipmentName = "3D Printer", EquipmentQuantity = 3 }
        };
        _contractServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(contracts);

        // Act
        var result = await _controller.GetAllContracts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(contracts, okResult.Value);
    }

    [Fact]
    public async Task GetAllContracts_Exception_ReturnsInternalServerError()
    {
        // Arrange
        _contractServiceMock.Setup(s => s.GetAllAsync())
            .ThrowsAsync(new Exception("Server error"));

        // Act
        var result = await _controller.GetAllContracts();

        // Assert
        var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, internalServerErrorResult.StatusCode);
        Assert.Equal("Server error", internalServerErrorResult.Value);
    }

    [Fact]
    public async Task AddContract_ReturnsOkResult()
    {
        // Arrange
        var contractModel = new CreateContractModel()
        { 
            EquipmentCode = 1,
            FacilityCode = 2,
            EquipmentQuantity = 2
        };
        _createContractValidatorMock.Setup(v => v.Validate(contractModel)).Returns(new ValidationResult());
        _contractServiceMock.Setup(s => s.CreateAsync(contractModel)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AddContract(contractModel);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task AddContract_InvalidModel_ReturnsBadRequestResult()
    {
        // Arrange
        var contractModel = new CreateContractModel()
        {
            EquipmentCode = 1,
            FacilityCode = 2,
            EquipmentQuantity = 2
        };
        var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Validation error")
        };
        _createContractValidatorMock.Setup(v => v.Validate(contractModel))
            .Returns(new ValidationResult(validationFailures));

        // Act
        var result = await _controller.AddContract(contractModel);

        // Assert
        var badResultRequest = Assert.IsType<ObjectResult>(result);
        Assert.Equal(400, badResultRequest.StatusCode);
        Assert.NotNull(badResultRequest.Value);
    }

    [Fact]
    public async Task AddContract_EntityNotFoundException_ReturnsNotFound()
    {
        // Arrange
        var contractModel = new CreateContractModel()
        {
            EquipmentCode = 1,
            FacilityCode = 2,
            EquipmentQuantity = 2
        };
        _createContractValidatorMock.Setup(v => v.Validate(contractModel)).Returns(new ValidationResult());
        _contractServiceMock.Setup(s => s.CreateAsync(contractModel))
            .ThrowsAsync(new EntityNotFoundException("Entity not found"));

        // Act
        var result = await _controller.AddContract(contractModel);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.Equal("Entity not found", notFoundResult.Value);
    }

    [Fact]
    public async Task AddContract_InsufficientSpaceException_ReturnsBadRequest()
    {
        // Arrange
        var contractModel = new CreateContractModel()
        {
            EquipmentCode = 1,
            FacilityCode = 2,
            EquipmentQuantity = 2
        };
        _createContractValidatorMock.Setup(v => v.Validate(contractModel)).Returns(new ValidationResult());
        _contractServiceMock.Setup(s => s.CreateAsync(contractModel))
            .ThrowsAsync(new InsufficientSpaceException("Insufficient space"));

        // Act
        var result = await _controller.AddContract(contractModel);

        // Assert
        var badResultRequest = Assert.IsType<ObjectResult>(result);
        Assert.Equal(400, badResultRequest.StatusCode);
        Assert.Equal("Insufficient space", badResultRequest.Value);
    }

    [Fact]
    public async Task AddContract_Exception_ReturnsInternalServerError()
    {
        // Arrange
        var contractModel = new CreateContractModel()
        {
            EquipmentCode = 1,
            FacilityCode = 2,
            EquipmentQuantity = 2
        };
        _createContractValidatorMock.Setup(v => v.Validate(contractModel)).Returns(new ValidationResult());
        _contractServiceMock.Setup(s => s.CreateAsync(contractModel))
            .ThrowsAsync(new Exception("Server error"));

        // Act
        var result = await _controller.AddContract(contractModel);

        // Assert
        var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, internalServerErrorResult.StatusCode);
        Assert.Equal("Server error", internalServerErrorResult.Value);
    }
}