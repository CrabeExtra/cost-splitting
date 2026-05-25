<script>
    import { goto } from '$app/navigation';
// TODO, a list opf contributions for an expense along with names of people, name of contribution, contribution amount, amount as a percentage of the total
// AND the total cost at the top. Ability to create a new contribution and the calculation to be repeated as required also exists.
// I might have to change the calculation location as of now - 25/05/2026. Just so I don't repeat code.
    import { page } from '$app/state';
    import GenericTable from '$lib/components/GenericTable.svelte';
    import { ApiClient } from '$lib/helpers/fetch';
    import { Trash2 } from 'lucide-svelte';
    import { onMount } from 'svelte';

    let expenseId = page.params.id;

    let contributions = $state([]);
    let expenseData = $state([]);

    const columns = [
        { key: 'name', label: 'Name' },
        { key: 'contribution', label: 'Contribution' },
        { key: 'personName', label: 'Person' },
        { key: 'expenseName', label: 'Expense' },
        { key: 'targetCost', label: 'Wanted cost' },
        { key: 'weight', label: 'Weight' },
        { key: 'percentage', label: 'Percentage' },
        { key: 'action', label: 'Actions', render: actionSnippet }
    ];

    const loadContributions = async () => {
        contributions = (await ApiClient.get(`/contributions/expense/${expenseId}`)).data;
        
        contributions = contributions?.map(c => {
            c.targetCost = c.targetCostCents / 100;

            return c;
        });
    }

    const loadExpenseData = async () => {
        expenseData = (await ApiClient.get(`/expenses/${expenseId}`)).data;
    }

    // TODO, ensure calculation occurs on backend based on expense type so this doesn't result in inconsistencies.
    const deleteContribution = async (contributionId) => {
        await ApiClient.delete(`/contributions`, contributionId);
        await loadContributions();
    }

    onMount( async () => {
        loadContributions();
        loadExpenseData();
    });

</script>

<!-- Note to self: be aware, as currently costs are database fields rather than calculated dynamically, if I delete a contribution or add a contribution, the calculation or GUI must factor this -->
{#snippet actionSnippet(row)}
    <button class="action-btn delete-btn" onclick={() => deleteContribution(row.id)} aria-label="Delete">
        <Trash2 class="icon" />
    </button>
{/snippet}

<div class="content">
    <div class="card">
        <h2>Contributions</h2>
        <button style="margin: 1rem 0;" class="action" onclick={() => goto(`/expenses/${expenseId}/add`)}>
            Add Contribution
        </button>
        <h4>
            Total amount: <b style="color: lightblue;">{expenseData?.cost}</b>{'   '}Expense type: <b style="color: lightblue;">{expenseData.type}</b>
        </h4>
        <GenericTable data={contributions} columns={columns} />
    </div> 
</div>

