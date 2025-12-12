<template>
  <nav class="glass-nav fixed top-0 w-full z-50 px-6 h-16 flex justify-between items-center">
    <div class="flex items-center gap-4">
      <NuxtLink to="/" class="flex items-center gap-2">
         <svg class="w-8 h-8" viewBox="0 0 100 100" fill="none">
            <path d="M20 80 L50 50 L80 20" stroke="#06B6D4" stroke-width="8" stroke-linecap="round" />
            <circle cx="20" cy="80" r="10" fill="#06B6D4" />
            <path d="M50 50 L80 50 L80 80 L50 80 Z" fill="#7C3AED" />
        </svg>
        <span class="font-display font-bold text-2xl tracking-tighter">RANK<span class="text-brand-cyan">IT</span></span>
      </NuxtLink>
      
      <div class="hidden md:flex gap-4 text-sm text-gray-400 ml-8 border-l border-white/10 pl-8">
        <NuxtLink to="/" class="hover:text-white transition" active-class="text-white font-bold border-b-2 border-brand-cyan">Inicio</NuxtLink>
        <NuxtLink to="/tournaments" class="hover:text-white transition" active-class="text-white font-bold border-b-2 border-brand-cyan">Torneos</NuxtLink>
        <NuxtLink to="/community" class="hover:text-white transition" active-class="text-white font-bold border-b-2 border-brand-cyan">Comunidad</NuxtLink>
      </div>
    </div>

    <div class="flex items-center gap-4">
      <div class="flex items-center gap-3 pl-4 border-l border-white/10">
          <div class="text-right hidden sm:block">
              <div class="text-xs font-bold text-brand-cyan">xSlayer99</div>
              <div class="text-[10px] text-gray-400">1,450 ELO</div>
          </div>
          <NuxtLink to="/profile" class="w-8 h-8 rounded-full bg-gray-700 border-2 border-brand-cyan overflow-hidden relative">
              <img src="https://i.pravatar.cc/150?img=33" alt="User">
          </NuxtLink>
      </div>
    </div>
  </nav>
</template>