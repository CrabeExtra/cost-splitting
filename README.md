## Task description

#### People hate splitting costs in group settings because “fair” is subjective. Build a product
#### that helps groups settle shared expenses in a way that feels fair.

You are critiqued on the user experience, usability of the features you build.
Please hold on to your code as we will discuss the decisions you made in the building process.

# Ideas:

### Types of expenses

- Shared (equal)
- Consolidated (Mutually beneficial agreement where one person wants something more expensive, but both people save money by combining into one transaction)
- Weighted (Some parties benefit more from the expense and should pay more)
- Percentage (Flat percentage based on externally calculated factors)
- Item based (Who got what item, make this an easy process to account)

Database requirements:
- People (people within the group)
    - Name

- Expenses (cost amongst the group)
    - CostAmount
    - Type

- Items (item to be expensed separately)
    - Name
    - ContributionId

- Contributions ()
    - PeopleId
    - ExpenseId
    - ContributionAmount


### APIs required:
- Add person.
    - update if ID uncluded.
- Get people.
- Get person by Id.
- Gets expenses.
- Get expense by Id.
- Add Expense.
- Gets Items.
- Add item
- Add contribution
- Get contributions


### Things to mention
- Could add pagination and sorting as well as %LIKE% based searching for each field.
- Input sanitation.
- Code cleanup.
- optimisation.