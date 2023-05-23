namespace MyPersonalAccounting.ActorSystem.Portfolio;

public abstract record PortfolioMessage(PortfolioId PortfolioId);

public record AddBilling(PortfolioId PortfolioId, DateOnly Date, string Label, decimal Amount, bool Checked, string Comment, bool IsArchived, bool IsSaving) : PortfolioMessage(PortfolioId);
public record AddRepetitiveBilling(PortfolioId PortfolioId, DateOnly Date, string Label, decimal Amount, FrequenceMode Frequence) : PortfolioMessage(PortfolioId);
public record CreatePortfolio(string Name);
public record EditPortfolio(PortfolioId PortfolioId, string Name) : PortfolioMessage(PortfolioId);
public record DeletePortfolio(PortfolioId PortfolioId) : PortfolioMessage(PortfolioId);
public record DeleteBilling(PortfolioId PortfolioId, BillingId BillingId) : PortfolioMessage(PortfolioId);
public record DeleteRepetitiveBilling(PortfolioId PortfolioId, RepetitiveBillingId RepetitiveBillingId) : PortfolioMessage(PortfolioId);
public record EditBilling(PortfolioId PortfolioId, BillingId BillingId, DateOnly Date, string Label, decimal Amount, bool Checked, string Comment, bool IsArchived, bool IsSaving) : PortfolioMessage(PortfolioId);
public record EditRepetitiveBilling(PortfolioId PortfolioId, RepetitiveBillingId RepetitiveBillingId, DateOnly Date, string Label, decimal Amount, FrequenceMode Frequence) : PortfolioMessage(PortfolioId);
public record InsertNextBilling(PortfolioId PortfolioId, RepetitiveBillingId RepetitiveBillingId) : PortfolioMessage(PortfolioId);
public record MarkAsChecked(PortfolioId PortfolioId, BillingId BillingId, bool Checked) : PortfolioMessage(PortfolioId);