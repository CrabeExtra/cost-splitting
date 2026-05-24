<script>
    // notice: bit hacky here, just putting this together as quickly as possible.
    import { goto } from "$app/navigation";
    import GenericForm from "$lib/components/GenericForm.svelte";
    import { ApiClient } from "$lib/helpers/fetch";
    import { onMount } from "svelte";
    import { toast } from "svelte-sonner";

    let people = $state([]);

    let selectedPeople = $state([]);

    let title = 'Create Shared Expense';

    let subtitle =
        'Create an expense where all participants contribute equally.';

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

    // this done on frontend!?! security!! stop!! you violated the law!
    function splitEvenly(totalCents, people) {
        const base = Math.floor(totalCents / people.length);
        let remainder = totalCents % people.length;

        return people.map(p => {
            const share = base + (remainder > 0 ? 1 : 0);
            remainder--;

            return {
                ...p,
                share: (share / 100)
            };
        });
    }

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
        
        selectedPeople = splitEvenly(fields.totalCost * 100, selectedPeople);


        // create the expense.
        const expenseId = (await ApiClient.post('expenses', {
            name: fields['name'],
            cost: fields['totalCost'],
            type: 'Shared'
        })).data.id;
        
        let anyFailed = false;
        await Promise.all(selectedPeople.map(async (p) => {
            const response = await ApiClient.post('contributions', {
                name: `Contribution from ${p.value}.`,
                contribution: p.share, 
                personId: p.id,
                expenseId: expenseId
            });
            if(!response.success) {
                anyFailed = true;
                toast.error(`An error has occurred while adding expense for person: ${p.value}. Error: ${response.message}`)
            }

        }));
        
        if(!anyFailed) {
            toast.success('Successfully created expense!')
            goto(`/expenses/${expenseId}`);
        }
            
    };

    onMount(async () => {
        const res = await ApiClient.get(`/people`);
        people = res.data;
    })
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

        {#each selectedPeople as person}
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
            </div>
        {/each}
    </div>
</GenericForm>