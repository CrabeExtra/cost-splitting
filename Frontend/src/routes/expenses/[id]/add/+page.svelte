<script>
    import { goto } from "$app/navigation";
    import GenericForm from "$lib/components/GenericForm.svelte";
    import { ApiClient } from "$lib/helpers/fetch";
    import { onMount } from "svelte";
    import { page } from '$app/state';

    let expenseId = page.params.id;

    let title = "Add contribution";
    let subtitle = $derived(`Add a new contribution to expense: '${expenseData?.name}'`);
    // TODO: add to this to include required fields from specified expense type. 
    let inputs = $state([
        { 
            key: 'Name',
            label: 'Name',
            placeholder: 'Enter contribution name' 
        }
    ]);
    
    let submissionText = 'Confirm';
    let expenseData = $state();
    let people = $state();
    let person = $state();
    let items = $state([]);

    let handleSubmit = async (fields) => {
        let personId = people?.find(p => p.name === person)?.id;
        if(!personId) {
            personId = (await ApiClient.post('/people', { name: person }))?.data?.id;
        }
        
        fields['personId'] = personId;
        fields['expenseId'] = expenseData.id;
        if(fields['targetCostCents'])
            fields['targetCostCents'] = fields['targetCostCents'] * 100;
        const response = await ApiClient.post('/contributions', fields);
        console.log(items)
        if(items?.length > 0)
            await Promise.all(await items?.map(async i => {
                await ApiClient.post('items', {
                    name: i.name,
                    costCents: i.costCents * 100,
                    contributionId: response.data.id
                });
            }));
        goto(`/expenses/${expenseData.id}`);
    }

    const loadPeople = async () => {
        const res = await ApiClient.get(`/people`);
        people = res.data;
    } 

    const loadExpenseData = async () => {
        const res = await ApiClient.get(`/expenses/${expenseId}`);
        expenseData = res.data;

        switch (expenseData.type) {
            case 'Consolidated': 
                inputs.push({ 
                    key: 'targetCostCents',
                    label: 'Wanted price',
                    placeholder: 'Enter the price that this contributor wanted.' 
                });
            break;
            case 'Weighted': 
                inputs.push({ 
                    key: 'weight',
                    label: 'Weight',
                    placeholder: 'Enter the weight factor for this contribution.' 
                });
            break;
            case 'Percentage':
                inputs.push({
                    key: 'percentage',
                    label: 'Percentage',
                    placeholder: 'Enter the percentage that this contributor will pay.' 
                }); 
            break;
                
        }
    }

    onMount(async () => {
        loadPeople();
        loadExpenseData();
    })
    
</script>

<GenericForm
    title={title}
    subtitle={subtitle}
    inputs={inputs}
    submissionText={submissionText}
    handleSubmit={handleSubmit}
>
    <div>
        <p style="color: #fff; margin: 0px; margin-left: 8px;">Person</p>
        <input
            list={expenseData?.id ?? 0}
            placeholder="Search or enter person"
            value={person}
            oninput={(e) =>
                person = e.target.value
            }
        />

        <datalist id={expenseData?.id ?? 0}>
            {#each people as existingPerson}
                <option value={existingPerson.name} />
            {/each}
        </datalist>
        
        {#if expenseData && expenseData.type === 'Item'}
            <button
                type="button"
                class="action"
                onclick={() => {
                    items = [
                        ...(items ?? []),
                        {
                            localId: crypto.randomUUID(),
                            name: '',
                            costCents: 0
                        }
                    ]
                }}
            >
                Add Item
            </button>
            {#each items as item, index}
                <div >
                    Item {index + 1}
                </div>
                <div style="margin-top: 8px;">
                    <input
                        placeholder="Item name"
                        value={item.name}
                        oninput={(e) =>{
                            items = items?.map(i =>
                                i.localId === item.localId
                                    ? { ...i, ['name']: e.target.value }
                                    : i
                            )
                        }}
                    />

                    <input
                        type="number"
                        placeholder="Cost (cents)"
                        value={item.costCents}
                        oninput={(e) =>{
                            items = items?.map(i =>
                                i.localId === item.localId
                                    ? { ...i, ['costCents']: e.target.value }
                                    : i
                            )
                        }}
                    />
                </div>
            {/each}
        {/if}
    </div>
</GenericForm>

<style></style>