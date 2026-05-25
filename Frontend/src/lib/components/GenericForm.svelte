<script>
    import { onMount } from "svelte";

    const { 
        title,
        subtitle = '',
        inputs = [], // { key, label, initialValue, type?, placeholder?}
        submissionText = 'Confirm',
        backText = 'Back',
        handleSubmit,
        handleGoBack = () => {
            window.history.back();
        },
    } = $props();

    let formFields = $state();

    const formatData = () => {
        const formattedFields = {};
        
        formFields?.map(f => {
            formattedFields[f.key] = f.value;
        })
        handleSubmit(formattedFields)
    }

    const buildFormFields = (inputs) => {
        return inputs?.map(input => ({ key: input.key, value: input.initialValue ?? '' }))
    }

    $effect(() => {
        formFields = buildFormFields(inputs);
    });
</script>


<div class="content">
    <div class="card">
        <h2>{title}</h2>

        <p>{subtitle}</p> 

        {#each inputs as input}
            {#if input.label}  
                <p style="margin: 0; padding-left: 10px; color: #fff">{input.label}</p>
            {/if}

            <input
                type={input.type || 'text'}
                placeholder={input.placeholder || ''}
                oninput={(e) => {
                    formFields = formFields?.map(f =>
                        f.key === input.key
                            ? { ...f, value: e.target.value }
                            : f
                    );
                }}
                value={formFields?.find(f => f.key === input.key)?.value ?? ''}
            />
        {/each}

        <slot />

        <button class="action" onclick={handleGoBack}>
           {backText}
        </button>
        <button class="action" onclick={formatData}>
            {submissionText}
        </button>
    </div> 
</div>