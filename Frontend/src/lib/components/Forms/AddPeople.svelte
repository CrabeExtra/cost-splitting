<script>
    import { goto } from "$app/navigation";
    import GenericForm from "$lib/components/GenericForm.svelte";
    import { ApiClient } from "$lib/helpers/fetch";

    const { 
        personData = undefined
    } = $props();

    let title = $derived(personData && personData.name ? `Modify person` : "Add person");
    let subtitle = $derived(personData && personData.name ? `Modify ${personData.name}` : "Add person");
    let inputs = $derived([
        { 
            key: 'name',
            label: 'Name',
            initialValue: personData?.name ?? '',
            placeholder: 'Enter person name' 
        }
    ]);
    let submissionText = 'Confirm';

    let handleSubmit = async (fields) => {
        fields['id'] = personData.id;
        await ApiClient.post('/people', fields);
        goto('/people');
    }
    
</script>

<GenericForm
    title={title}
    subtitle={subtitle}
    inputs={inputs}
    submissionText={submissionText}
    handleSubmit={handleSubmit}
/>

<style></style>