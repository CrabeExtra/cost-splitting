<script>
    import { goto } from '$app/navigation';
    import { EXPENSE_TYPES } from '$lib/constants/WorkflowConstants';

    const workflows = EXPENSE_TYPES;

    const openWorkflow = (workflow) => {
        goto(`/workflow/${workflow.key}`);
    };
</script>

<div class="content">
    <div class="card">
        <h1>Click Type</h1>

        <div class="workflow-grid">
            {#each workflows as workflow}
                <button
                    class="workflow-card"
                    onclick={() => openWorkflow(workflow)}
                >
                    
                    <div class="icon-wrapper">
                        <workflow.icon
                            size={32}
                            strokeWidth={2.5}
                        />
                    </div>
                    <div class="content">
                        <h2>{workflow.name}</h2>
                        
                        <p class="description">
                            {workflow.description}
                        </p>

                        <div class="use-cases">
                            <h3>Good For</h3>

                            <ul>
                                {#each workflow.usages as usage}
                                    <li>{usage}</li>
                                {/each}
                            </ul>
                        </div>
                    </div>
                </button>
            {/each}
        </div>
    </div>
</div>

<style>
    .page {
        min-height: 100vh;
        background: #0f1115;
        color: #e5e7eb;
    }

    .content {
        padding: 2rem;
        max-width: 1400px;
        margin: 0 auto;
    }

    .header {
        margin-bottom: 2rem;
    }

    h1 {
        margin: 0 0 0.75rem 0;
        font-size: 2.4rem;
        font-weight: 700;
    }

    .header p {
        margin: 0;
        color: #9ca3af;
        line-height: 1.6;
        max-width: 700px;
    }

    .workflow-grid {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(360px, 1fr));
        gap: 1.5rem;
    }

    .workflow-card {
        background: #161a22;

        border: 1px solid #252b36;

        border-radius: 16px;

        padding: 1.5rem;

        text-align: left;

        color: inherit;

        cursor: pointer;

        box-shadow:
            0 4px 24px rgba(0, 0, 0, 0.25);

        transition:
            transform 0.15s ease,
            border-color 0.15s ease,
            background 0.15s ease,
            box-shadow 0.15s ease;

        display: flex;
        flex-direction: column;
        height: 300px;
        overflow-y: auto;
    }

    .workflow-card:hover {
        transform: translateY(-4px);

        border-color: #3b82f6;

        background: #1b2029;

        box-shadow:
            0 10px 32px rgba(0, 0, 0, 0.35);
    }

    .workflow-card:active {
        transform: scale(0.99);
    }

    .workflow-top {
        display: flex;
        align-items: center;
        gap: 1rem;

        margin-bottom: 1rem;
    }

    .icon-wrapper {
        width: 52px;
        height: 52px;

        display: flex;
        align-items: center;
        justify-content: center;

        border-radius: 12px;

        background: #0f1115;

        border: 1px solid #2f3746;

        color: #60a5fa;

        flex-shrink: 0;
    }

    h2 {
        margin: 0;
        font-size: 1.3rem;
        font-weight: 700;
        color: #e5e7eb;
    }

    .description {
        margin: 0 0 1.25rem 0;

        color: #9ca3af;

        line-height: 1.6;
    }

    .use-cases h3 {
        margin: 0 0 0.75rem 0;

        font-size: 0.9rem;

        color: #e5e7eb;

        letter-spacing: 0.02em;
    }

    ul {
        margin: 0;
        padding-left: 1.25rem;

        display: flex;
        flex-direction: column;
        gap: 0.6rem;
    }

    li {
        color: #9ca3af;
        line-height: 1.5;
    }
</style>