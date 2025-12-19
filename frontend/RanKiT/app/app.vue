<script setup>
useHead({
  title: 'Rankit - The Competitive Ecosystem',
  meta: [
    { name: 'description', content: 'El sistema operativo para organizadores de torneos profesionales.' },
    { name: 'viewport', content: 'width=device-width, initial-scale=1.0' }
  ],
  script: [
    { src: 'https://unpkg.com/@phosphor-icons/web' }
  ],
  link: [
    { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Chakra+Petch:wght@300;400;500;600;700&family=Archivo:wght@300;400;600;800&display=swap' }
  ]
})
</script>

<template>
  <div class="antialiased selection:bg-[var(--rankit-neon)] selection:text-white">
    <NuxtLayout>
      <NuxtPage />
    </NuxtLayout>
  </div>
</template>

<style>
:root {
  --rankit-neon: #bf00ff;
  --rankit-black: #050505;
  --rankit-panel: #111111;
}

/* Fuentes Globales */
body {
  font-family: 'Archivo', sans-serif;
  background-color: var(--rankit-black);
  color: white;
}

h1, h2, h3, h4, h5, h6, .font-display {
  font-family: 'Chakra Petch', sans-serif;
}

/* Estilos Brutalistas Globales */
.brutal-card { 
    position: relative;
    transition: all 0.2s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    background: #0a0a0a;
    border: 1px solid #333;
}

html:not(.dark) .brutal-card {
    background: #ffffff;
    border: 1px solid #e5e5e5;
    box-shadow: 4px 4px 0px #00000010;
}

.brutal-card:hover { 
    border-color: var(--rankit-neon); 
    transform: translate(-4px, -4px);
    box-shadow: 6px 6px 0px var(--rankit-neon);
}

html:not(.dark) .brutal-card:hover {
    box-shadow: 6px 6px 0px var(--rankit-neon), 6px 6px 0px 2px black;
}

/* Botones Sesgados (Skew) */
.btn-skew {
    background-color: var(--rankit-neon);
    color: white;
    transform: skewX(-10deg);
    transition: all 0.2s;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
}

.btn-skew:hover { 
    background-color: white; 
    color: black; 
    box-shadow: 0 0 15px var(--rankit-neon);
}

.btn-skew-outline {
    background: transparent;
    color: currentColor;
    border: 1px solid currentColor;
    transform: skewX(-10deg);
    transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    display: inline-flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    position: relative;
    overflow: hidden;
}

.btn-skew-outline::before {
    content: '';
    position: absolute;
    top: 0;
    left: -100%;
    width: 100%;
    height: 100%;
    background: currentColor;
    transition: all 0.3s ease;
    z-index: -1;
    opacity: 0.1;
}

.btn-skew-outline:hover {
    transform: skewX(-10deg) translateY(-2px);
    box-shadow: 4px 4px 0px var(--rankit-neon);
    border-color: var(--rankit-neon);
    color: var(--rankit-neon);
}

.btn-skew-outline:hover::before {
    left: 0;
}

.btn-content { transform: skewX(10deg); }

/* Inputs Brutalistas */
.brutal-input {
    width: 100%;
    background: transparent;
    border-bottom: 2px solid #333;
    padding: 1rem 0;
    font-family: 'Archivo', sans-serif;
    font-weight: 600;
    outline: none;
    transition: all 0.3s;
    color: white;
}

html:not(.dark) .brutal-input { color: black; border-color: #e5e5e5; }

.brutal-input:focus {
    border-color: var(--rankit-neon);
    padding-left: 1rem;
}

/* Scrollbar */
::-webkit-scrollbar { width: 6px; }
::-webkit-scrollbar-track { background: #050505; }
::-webkit-scrollbar-thumb { background: #333; border-radius: 3px; }
::-webkit-scrollbar-thumb:hover { background: var(--rankit-neon); }
</style>