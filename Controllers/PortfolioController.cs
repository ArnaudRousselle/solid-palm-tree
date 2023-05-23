using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyPersonalAccounting.ActorSystem;
using MyPersonalAccounting.ActorSystem.Portfolio;
using MyPersonalAccounting.ActorSystem.Projections.Billings;
using MyPersonalAccounting.ActorSystem.Projections.Portfolios;
using MyPersonalAccounting.ActorSystem.Projections.RepetitiveBillings;
using MyPersonalAccounting.Controllers.Model;

namespace MyPersonalAccounting.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly ILogger<PortfolioController> _logger;
    private readonly ICommandHub _commandHub;

    public PortfolioController(
        ILogger<PortfolioController> logger,
        ICommandHub commandHub)
    {
        _logger = logger;
        _commandHub = commandHub;
    }

    [HttpGet("GetPortfolios")]
    public async Task<RequestResult<PortfolioItem[]>> GetPortfolios()
    {
        return await _commandHub.RequestAsync(new GetPortfolios());
    }

    [HttpGet("GetBillings/{portfolioId}")]
    public async Task<RequestResult<BillingItem[]>> GetBillings([Required] string portfolioId)
    {
        return await _commandHub.RequestAsync(new GetBillings(new PortfolioId(portfolioId)));
    }

    [HttpGet("GetRepetitiveBillings/{portfolioId}")]
    public async Task<RequestResult<RepetitiveBillingItem[]>> GetRepetitiveBillings([Required] string portfolioId)
    {
        return await _commandHub.RequestAsync(new GetRepetitiveBillings(new PortfolioId(portfolioId)));
    }

    [HttpPut("CreatePortfolio")]
    public void CreatePortfolio([Required] CreatePortfolioRequest request)
    {
        _commandHub.Send(new CreatePortfolio(request.Name));
    }

    [HttpPost("EditPortfolio")]
    public void EditPortfolio([Required] EditPortfolioRequest request)
    {
        _commandHub.Send(new EditPortfolio(request.PortfolioId, request.Name));
    }

    [HttpDelete("DeletePortfolio")]
    public void DeletePortfolio([Required] DeletePortfolioRequest request)
    {
        _commandHub.Send(new DeletePortfolio(request.PortfolioId));
    }

    [HttpPut("AddBilling")]
    public void AddBilling([Required] AddBillingRequest request)
    {
        _commandHub.Send(new AddBilling(request.PortfolioId, request.Date, request.Name, request.Amount, request.Checked, request.Comment, request.IsArchived, request.IsSaving));
    }

    [HttpPost("EditBilling")]
    public void EditBilling([Required] EditBillingRequest request)
    {
        _commandHub.Send(new EditBilling(request.PortfolioId, request.BillingId, request.Date, request.Name, request.Amount, request.Checked, request.Comment, request.IsArchived, request.IsSaving));
    }

    [HttpDelete("DeleteBilling")]
    public void DeleteBilling([Required] DeleteBillingRequest request)
    {
        _commandHub.Send(new DeleteBilling(request.PortfolioId, request.BillingId));
    }

    [HttpPut("AddRepetitiveBilling")]
    public void AddRepetitiveBilling([Required] AddRepetitiveBillingRequest request)
    {
        _commandHub.Send(new AddRepetitiveBilling(request.PortfolioId, request.Date, request.Name, request.Amount, request.FrequenceMode));
    }

    [HttpPost("EditRepetitiveBilling")]
    public void AddBilling([Required] EditRepetitiveBillingRequest request)
    {
        _commandHub.Send(new EditRepetitiveBilling(request.PortfolioId, request.RepetitiveBillingId, request.Date, request.Name, request.Amount, request.FrequenceMode));
    }

    [HttpDelete("DeleteRepetitiveBilling")]
    public void DeleteBilling([Required] DeleteRepetitiveBillingRequest request)
    {
        _commandHub.Send(new DeleteRepetitiveBilling(request.PortfolioId, request.RepetitiveBillingId));
    }

    [HttpPut("InsertNextBilling")]
    public void InsertNextBilling([Required] InsertNextBillingRequest request)
    {
        _commandHub.Send(new InsertNextBilling(request.PortfolioId, request.RepetitiveBillingId));
    }

    [HttpPost("MarkAsChecked")]
    public void MarkAsChecked([Required] MarkAsCheckedRequest request)
    {
        _commandHub.Send(new MarkAsChecked(request.PortfolioId, request.BillingId, request.Checked));
    }

}




