<script lang="ts" generics="T extends Record<string, any>">
  import type { Snippet } from 'svelte';

  type Column<T> = {
    key: keyof T;
    label: string;
    render?: (row: T) => Snippet;
  };

  let { data, columns, rowClick } = $props<{
    data: T[];
    columns: Column<T>[];
    rowClick: (row: T) => void;
  }>();

</script>

<table class="table">
  <thead>
    <tr>
      {#each columns as col}
        <th>{col.label}</th>
      {/each}
    </tr>
  </thead>

  <tbody>
    {#each data as row}
      <tr onclick={() => (rowClick ? rowClick(row) : () => {})}>
        {#each columns as col}
          <td>
            {#if col.render}
              {@render col.render(row)}
            {:else}
              {row[col.key] ?? 'N/A'}
            {/if}
          </td>
        {/each}
      </tr>
    {/each}
  </tbody>
</table>
