<script setup>
// Forzamos transparencia a nivel de HTML y BODY para OBS
// Esto sobrescribe cualquier estilo por defecto del navegador
useHead({
  htmlAttrs: {
    style: 'background-color: transparent !important; background: none !important;'
  },
  bodyAttrs: {
    style: 'background-color: transparent !important; background: none !important; overflow: hidden;'
  }
})
</script>

<template>
  <div class="min-h-screen bg-transparent">
    <slot />
  </div>
</template>