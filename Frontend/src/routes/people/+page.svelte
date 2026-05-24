<script >
    import { toast } from "svelte-sonner";
    import { Pencil, PencilIcon, Trash2 } from "lucide-svelte";

    import GenericTable from '$lib/components/GenericTable.svelte';
    import { goto } from '$app/navigation';
    import { ApiClient } from "$lib/helpers/fetch";
    import { onMount } from "svelte";

    let people = $state([]);

    const columns = [
        { key: 'name', label: 'Name' },
        { key: 'action', label: 'Actions', render: actionSnippet }
    ];

    const deletePerson = async (id) => {
        await ApiClient.delete('/people', id);
        await loadPeople();
    }

    const loadPeople = async () => {
        const response = await ApiClient.get('/people');
        people = response.data;
    }

    onMount(async () => {
        await loadPeople();
    })

</script>
{#snippet actionSnippet(row)}
    <button class="action-btn delete-btn" onclick={() => deletePerson(row.id)} aria-label="Delete">
        <Trash2 class="icon" />
    </button>
     <button class="action-btn edit-btn" onclick={() => goto(`/people/add/${row.id}`)} aria-label="Update">
        <PencilIcon class="icon" />
    </button>
{/snippet}

<div class="content">
    <div class="card">
        <h2>People</h2>
        <button style="margin: 1rem 0;" class="action" onclick={() => goto('/people/add')}>
            Add Person
        </button>
        <GenericTable data={people} columns={columns} />
    </div> 
</div>

<style>
    .action-btn {
        display: inline-flex;
        align-items: center;
        justify-content: center;

        width: 36px;
        height: 36px;

        border: 1px solid rgba(255, 255, 255, 0.08);
        background: rgba(255, 255, 255, 0.04);

        border-radius: 8px;
        cursor: pointer;

        transition: all 0.15s ease;

        opacity: 0.8;
    }

    .edit-btn {
        color: #fff;
    }

    .delete-btn {
        color: #ff4d4f;
    }

    .action-btn:hover {
        transform: translateY(-1px);
        opacity: 1; 
    }

    .action-btn:active {
        transform: translateY(0px);
    }

    .icon {
        width: 18px;
        height: 18px;
    }
</style>