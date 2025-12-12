<script setup>
// Layout principal del admin
</script>

<template>
  <div class="flex h-screen overflow-hidden text-sm bg-[#0B0C15] text-white font-sans">
    <!-- Sidebar -->
    <aside class="w-64 bg-[#151725] border-r border-white/5 flex flex-col z-20 shadow-2xl shrink-0">
        <!-- Logo Header -->
        <div class="h-20 flex items-center px-6 border-b border-gray-800 gap-3">
             <svg class="w-8 h-8" viewBox="0 0 100 100" fill="none">
                <path d="M20 80 L50 50 L80 20" stroke="#06B6D4" stroke-width="8" stroke-linecap="round" />
                <circle cx="20" cy="80" r="10" fill="#06B6D4" />
                <path d="M50 50 L80 50 L80 80 L50 80 Z" fill="#7C3AED" />
            </svg>
            <div class="flex flex-col">
                <span class="font-display font-bold text-lg">RANK<span class="text-brand-cyan">IT</span></span>
                <span class="text-[9px] text-gray-500 uppercase">Panel Cometax</span>
            </div>
        </div>
        
        <!-- Navigation Links -->
        <div class="flex-1 py-6 space-y-1 overflow-y-auto">
            <!-- Usamos NuxtLink con active-class para replicar el estilo de pestaña activa -->
            <NuxtLink to="/admin" 
                class="flex items-center px-6 py-3 text-gray-400 hover:bg-white/5 hover:text-white transition group border-r-4 border-transparent" 
                active-class="!bg-brand-cyan/10 !border-brand-cyan !text-white">
                <i class="fas fa-chart-pie w-6 group-hover:text-brand-cyan transition"></i> Resumen
            </NuxtLink>

            <NuxtLink to="/admin/tournaments" 
                class="flex items-center px-6 py-3 text-gray-400 hover:bg-white/5 hover:text-white transition group border-r-4 border-transparent"
                active-class="!bg-brand-cyan/10 !border-brand-cyan !text-white">
                <i class="fas fa-trophy w-6 group-hover:text-brand-cyan transition"></i> Mis Torneos
            </NuxtLink>
            
            <div class="pt-6 pb-2 px-6 text-xs font-bold text-brand-cyan uppercase tracking-widest">Herramientas</div>
            
            <NuxtLink to="/admin/streaming" 
                class="flex items-center px-6 py-3 text-gray-400 hover:bg-white/5 hover:text-white transition group border-r-4 border-transparent"
                active-class="!bg-brand-cyan/10 !border-brand-cyan !text-white">
                <i class="fas fa-video w-6 group-hover:text-red-500 transition"></i> OBS Widgets
            </NuxtLink>

             <div class="pt-6 pb-2 px-6 text-xs font-bold text-gray-500 uppercase tracking-widest">Configuración</div>

             <NuxtLink to="/admin/settings" 
                class="flex items-center px-6 py-3 text-gray-400 hover:bg-white/5 hover:text-white transition group border-r-4 border-transparent"
                active-class="!bg-brand-cyan/10 !border-brand-cyan !text-white">
                <i class="fas fa-cog w-6 group-hover:text-brand-purple transition"></i> Ajustes de Torneo
            </NuxtLink>
        </div>
        
        <!-- User Profile Mini -->
        <div class="p-4 border-t border-gray-800 bg-black/20">
            <div class="flex items-center gap-3">
                <img src="https://i.pravatar.cc/150?img=60" class="w-9 h-9 rounded-full border border-brand-cyan">
                <div>
                    <div class="font-bold text-xs text-white">Cometax Org</div>
                    <div class="text-[10px] text-brand-cyan">Plan Enterprise</div>
                </div>
                <button class="ml-auto text-gray-500 hover:text-white"><i class="fas fa-sign-out-alt"></i></button>
            </div>
        </div>
    </aside>

    <!-- Main Content Area -->
    <main class="flex-1 flex flex-col relative overflow-y-auto bg-[url('https://www.transparenttextures.com/patterns/carbon-fibre.png')]">
        <!-- Aquí se cargarán las páginas hijas (index, tournaments, streaming, settings) -->
        <NuxtPage />
    </main>
  </div>
</template>