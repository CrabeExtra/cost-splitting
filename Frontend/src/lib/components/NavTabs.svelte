<script>
	import { goto } from '$app/navigation';
	const { activeTab } = $props();

	const tabs = [
		{ id: "workflow", label: "Split bill (start here!)" },
		{ id: "expenses", label: "Historical expenses" },
		{ id: "people", label: "Add People" }
	];
	
</script>

<style>
	:global(body) {
		margin: 0;
		font-family:
			Inter,
			system-ui,
			sans-serif;

		background: #0f1115;
		color: #e5e7eb;
	}

	.navbar {
		display: flex;
		gap: 0.75rem;

		padding: 1rem 1.5rem;

		background: #161a22;

		border-bottom: 1px solid #252b36;

		box-shadow: 0 2px 12px rgba(0, 0, 0, 0.25);
	}

	.tab {
		border: none;

		background: transparent;

		color: #9ca3af;

		padding: 0.7rem 1.1rem;

		border-radius: 10px;

		font-size: 0.95rem;
		font-weight: 500;

		cursor: pointer;

		transition:
			background 0.2s ease,
			color 0.2s ease,
			transform 0.15s ease;
	}

	.tab:hover {
		background: #202632;
		color: #f3f4f6;
	}

	.tab:active {
		transform: scale(0.98);
	}

	.tab.active {
		background: #2b3548;
		color: #ffffff;

		box-shadow:
			inset 0 0 0 1px #3b82f6,
			0 0 12px rgba(59, 130, 246, 0.15);
	}
</style>

<nav class="navbar" style="position: fixed; width: 100vw; height: 40px; z-index: 1000;">
	{#each tabs as tab}
		<button
			class="tab"
			class:active={activeTab === tab.id}
			onclick={() => goto(`/${tab.id}`)}
		>
			{tab.label}
		</button>
	{/each}
</nav>