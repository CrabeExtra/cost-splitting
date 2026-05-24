<script>
	import { Toaster } from "svelte-sonner";

	import './layout.css';

	import NavTabs from '$lib/components/NavTabs.svelte';
	import { page } from '$app/state';

	const getActive = () => {
		const path = page.url.pathname;

		if (path.startsWith('/people')) return 'people';
		if (path.startsWith('/expenses')) return 'expenses';
		if (path.startsWith('/workflow')) return 'workflow';
		return 'workflow';
	}

	let { children } = $props();
</script>

<Toaster theme="dark" />
<NavTabs
	activeTab={getActive()}
/>
<main style="padding-top: 50px;">
	<slot/>
</main>
