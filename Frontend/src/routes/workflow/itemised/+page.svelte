<script>
/**
 * VERY MESSY CODE, I COULD AND SHOULD DO THIS GENERICALLY BUT I NEED IT DONE BY THIS WEEK 
 * TO THE READER: THIS IS ALREADY MORE COMPLEX THAN THE USE CASE, I COULD HAVE CREATED A SHALLOW NO-SERVER
 * DEMO DATA FRONTEND FOR THE PURPOSES.
*/
    // notice: bit hacky here, just putting this together as quickly as possible.
    import { goto } from "$app/navigation";
    import GenericForm from "$lib/components/GenericForm.svelte";
    import { ApiClient } from "$lib/helpers/fetch";
    import { onMount } from "svelte";
    import { toast } from "svelte-sonner";

    let people = $state([]);

    let selectedPeople = $state([]);

    let title = 'Create Itemised Expense';

    let subtitle =
        'Create an expense where each person pays for a described set of items.';

    let inputs = $derived([
        {
            key: 'name',
            label: 'Name, so you can refer back to this expense later',
            placeholder: 'Enter name',
        },
        {
            key: 'totalCost',
            label: 'Total Cost',
            placeholder: 'Enter total cost',
            type: 'number'
        }
    ]);

    let submissionText = 'Create Expense';

    const addPerson = () => {
        selectedPeople = [
            ...selectedPeople,
            {
                localId: crypto.randomUUID(),
                value: ''
            }
        ];
    };

    const updatePerson = async (localId, value) => {
        const existingPeople = await ApiClient.get(`/people/name/${value}`);
        let existingPerson;
        existingPeople?.data?.map(p => {
            if(p.name === value) {
                existingPerson = p;
            }
        });

        selectedPeople = selectedPeople.map(person =>
            person.localId === localId
                ? { ...person, value, ...(existingPerson ? ({ id: existingPerson.id }) : ({})) }
                : person
        );
    };

    const updateItem = (personId, itemId, field, value) => {
        selectedPeople = selectedPeople.map(p => {
            if (p.localId !== personId) return p;

            return {
                ...p,
                items: p.items.map(i =>
                    i.localId === itemId
                        ? { ...i, [field]: value }
                        : i
                )
            };
        });
    };

    const addItem = (personId) => {
        selectedPeople = selectedPeople.map(p =>
            p.localId === personId
                ? {
                    ...p,
                    items: [
                        ...(p.items ?? []),
                        {
                            localId: crypto.randomUUID(),
                            name: '',
                            costCents: 0
                        }
                    ]
                }
                : p
        );
    };

    /**
     * Very minimal error handling here, let it be known that this is a project I want to complete quickly.
     * This should all honestly be a dedicated businesslogic/service layer API.
     * @param fields
     */
    let handleSubmit = async (fields) => {
        if(selectedPeople.length === 0) {
            toast.warning('Please add at least one person to this expense.');
            return;
        }

        // create people that don't exist.
        selectedPeople = await Promise.all(
            selectedPeople.map(async p => {
                if(p.id) {
                    return p;
                }

                const newId = (await ApiClient.post('/people', { name: p.value })).data.id;
                return {...p, id: newId};
            })
        );

        // create the expense.
        const expenseId = (await ApiClient.post('expenses', {
            name: fields['name'],
            cost: fields['totalCost'] * 100, // * 100 to convert to cents
            type: 'Item'
        })).data.id;
        
        let anyFailed = false;
        await Promise.all(selectedPeople.map(async (p) => {
            const response = await ApiClient.post('contributions', {
                name: `Contribution from ${p.value}.`,
                personId: p.id,
                expenseId: expenseId
            });
            
            if(!response.success) {
                anyFailed = true;
                toast.error(`An error has occurred while adding expense for person: ${p.value}. Error: ${response.message}`)
            }
            
            p.items.map(async i => {
                await ApiClient.post('items', {
                    name: i.name,
                    costCents: i.costCents * 100,
                    contributionId: response.data.id
                });
            });

        }));

        
        if(!anyFailed) {
            toast.success('Successfully created expense!')
            goto(`/expenses/${expenseId}`);
        }
            
    };

    onMount(async () => {
        const res = await ApiClient.get(`/people`);
        people = res.data;
    });
</script>

<GenericForm
    title={title}
    subtitle={subtitle}
    inputs={inputs}
    submissionText={submissionText}
    handleSubmit={handleSubmit}
>
    <div class="people-section">
        <button
            class="action"
            type="button"
            onclick={addPerson}
            style="margin-bottom: 1rem;"
        >
            Add Person
        </button>

        {#each selectedPeople as person, index}
            <div style="margin-bottom: 5px; margin-left: 10px;">
                Person {index + 1}
            </div>
            <div>
                
                <input
                    list={person.localId}
                    placeholder="Search or enter person"
                    value={person.value}
                    oninput={(e) =>
                        updatePerson(person.localId, e.target.value)
                    }
                />

                <datalist id={person.localId}>
                    {#each people as existingPerson}
                        <option value={existingPerson.name} />
                    {/each}
                </datalist>
                <div style="margin-left: 12px; margin-top: 8px;">
                    <button
                        type="button"
                        class="action"
                        onclick={() => addItem(person.localId)}
                    >
                        Add Item
                    </button>

                    {#each person.items as item, index}
                        <div >
                            Item {index + 1}
                        </div>
                        <div style="margin-top: 8px;">
                            <input
                                placeholder="Item name"
                                value={item.name}
                                oninput={(e) =>
                                    updateItem(
                                        person.localId,
                                        item.localId,
                                        'name',
                                        e.target.value
                                    )
                                }
                            />

                            <input
                                type="number"
                                placeholder="Cost (cents)"
                                value={item.costCents}
                                oninput={(e) =>
                                    updateItem(
                                        person.localId,
                                        item.localId,
                                        'costCents',
                                        +e.target.value
                                    )
                                }
                            />
                        </div>
                    {/each}
                </div>
            </div>
        {/each}
    </div>
</GenericForm>