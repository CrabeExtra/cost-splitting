using Round_2.Application.Exceptions;

namespace Round_2.Database.Entity;

public class Contributions : IDbEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; } 
    public int? TargetCostCents { get; set; } // for consolidated case, how much does this person WANT to have to pay.
    public int? Weight { get; set; } // for weighted cost, what weighting does this person need to pay.
    public double? Percentage { get; set; }
    public double Contribution => CalculateContributionCost();

    public required Guid PersonId { get; set; }
    public required Guid ExpenseId { get; set; }

    public People Person { get; set; } = null!;
    public Expenses Expense { get; set; } = null!;
    public List<Items> Items { get; set; } = [];

    private double CalculateContributionCost()
    {
        return this.Expense.Type switch
        {
            ExpenseType.Shared => SharedContributionAmount(),
            ExpenseType.Consolidated => ConsolidatedContributionAmount(),
            ExpenseType.Weighted => WeightedContributionAmount(),
            ExpenseType.Item => ItemContributionAmount(),
            ExpenseType.Percentage => PercentageContributionAmount(),
            _ => 0,
        };
    }
    
    private double SharedContributionAmount()
    {
        int totalCents = this.Expense.CostCents;
        int peopleCount = this.Expense.Contributions.Count; // number of people is # of expenses, 1:1

        int baseShare = totalCents / peopleCount;
        int remainder = totalCents % peopleCount;

        var shares = new List<int>();

        for (int i = 0; i < peopleCount; i++)
        {
            // Distribute leftover cents one-by-one
            int share = baseShare + (i < remainder ? 1 : 0);
            shares.Add(share);
        }

        
        double thisContributionAmount = this.DistributeContribution(shares);
        return thisContributionAmount;
    }

    // I know the calculations here are the same as weighted and this could be generic, but the conceptual difference exists.
    // Could also implement this so that the 'savings' are the same rather than a proportion amount to make it different, but I'm not sure if that is more fair.
    private double ConsolidatedContributionAmount()
    {

        int totalCents = this.Expense.CostCents;
        Console.WriteLine(this.Weight);
        var weights = this.Expense.Contributions
            .Select(c => c.TargetCostCents ?? 0)
            .ToList();

        int totalWeight = weights.Sum();

        var shares = new List<int>();

        int allocated = 0;

        for (int i = 0; i < weights.Count; i++)
        {
            int share = totalCents * weights[i] / totalWeight;
            shares.Add(share);
            allocated += share;
        }

        // distribute leftover cents due to rounding
        int remainder = totalCents - allocated;

        for (int i = 0; i < remainder; i++)
        {
            shares[i % shares.Count]++;
        }

        double thisContributionAmount = this.DistributeContribution(shares);
        return thisContributionAmount;
    }
    private double WeightedContributionAmount()
    {
        int totalCents = this.Expense.CostCents;

        var weights = this.Expense.Contributions
            .Select(c => c.Weight ?? 0)
            .ToList();

        int totalWeight = weights.Sum();

        var shares = new List<int>();

        int allocated = 0;

        for (int i = 0; i < weights.Count; i++)
        {
            int share = totalCents * weights[i] / totalWeight;
            shares.Add(share);
            allocated += share;
        }

        // distribute leftover cents due to rounding
        int remainder = totalCents - allocated;

        for (int i = 0; i < remainder; i++)
        {
            shares[i % shares.Count]++;
        }

        return this.DistributeContribution(shares);
    }
    private double ItemContributionAmount()
    {
        int thisCost = 0;
        this.Items.ForEach(i =>
        {
            thisCost += i.CostCents;
        });

        return thisCost / 100.0;
    }

    private double PercentageContributionAmount()
    {
        int totalCents = this.Expense.CostCents;

        var contributions = this.Expense.Contributions.ToList();

        var shares = new List<int>();

        int allocated = 0;

        for (int i = 0; i < contributions.Count; i++)
        {
            // Each contribution has a flat percentage (e.g. 0.25 for 25%)
            double percent = contributions[i].Percentage ?? 0.0;

            int share = (int)Math.Round(totalCents * percent / 100, MidpointRounding.AwayFromZero);

            shares.Add(share);
            allocated += share;
        }

        // fix rounding drift so total matches exactly
        int remainder = totalCents - allocated;

        for (int i = 0; i < remainder; i++)
        {
            shares[i % shares.Count]++;
        }

        return this.DistributeContribution(shares);
    }

    // distribute correctly so despite not calculating at an upper level in the program, distributed contributions are equal.
    private double DistributeContribution(List<int> shares)
    {
        List<Guid> contributions = [.. this.Expense.Contributions.Select(c => c.Id)];
        
        if(shares.Count != contributions.Count) 
            throw new ServiceException("Invalid use of calculating costs, number of shares provided do not match number of people.");
        
        var sortedContributions = contributions.OrderBy(c => c); // sort by Guid.

        var index = contributions.IndexOf(this.Id);

        return shares[index] / 100.0;
    }
}