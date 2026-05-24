<script lang="ts" generics="T extends Record<string, any>">
  import type { Snippet } from 'svelte';

  type Column<T> = {
    key: keyof T;
    label: string;
    render?: (row: T) => Snippet;
  };

  let { data, columns } = $props<{
    data: T[];
    columns: Column<T>[];
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
      <tr>
        {#each columns as col}
          <td>
            {#if col.render}
              {@render col.render(row)}
            {:else}
              {row[col.key]}
            {/if}
          </td>
        {/each}
      </tr>
    {/each}
  </tbody>
</table>
