<script>
    import { onMount } from 'svelte';
    
    import AddPeople from "$lib/components/Forms/AddPeople.svelte";
    import { ApiClient } from "$lib/helpers/fetch";
    import { page } from '$app/state';

    let id = page.params.id;

    let data = $state();

    onMount(async () => {
        const res = await ApiClient.get(`/people/${id}`);
        data = res.data;
    });

</script>

<AddPeople personData={data ?? {}} /> 