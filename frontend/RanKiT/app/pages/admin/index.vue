<script setup>
// No necesitamos layout: 'admin' explícito porque esta página es hija de admin.vue (Nested Route)
</script>

<template>
  <div>
      <!-- Topbar -->
      <header class="h-20 border-b border-gray-800 flex items-center justify-between px-8 bg-[#0B0C15]/90 backdrop-blur-md sticky top-0 z-30">
          <div>
              <h2 class="font-display text-2xl font-bold">Resumen General</h2>
              <p class="text-gray-500 text-xs">Bienvenido de nuevo, organizador.</p>
          </div>
          <div class="flex items-center gap-6">
                <div class="relative">
                    <i class="fas fa-bell text-gray-400 text-lg hover:text-white cursor-pointer"></i>
                    <span class="absolute -top-1 -right-1 w-2 h-2 bg-red-500 rounded-full"></span>
                </div>
                <button class="bg-gradient-to-r from-brand-purple to-brand-cyan hover:opacity-90 text-white px-5 py-2 rounded-lg font-bold flex items-center gap-2 shadow-lg shadow-purple-900/40 text-xs transition transform hover:scale-105">
                    <i class="fas fa-plus"></i> Crear Torneo
                </button>
            </div>
      </header>

      <div class="p-8 max-w-7xl mx-auto w-full">
          <!-- Stats Grid -->
          <div class="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
               <!-- Stat 1 -->
               <div class="glass-panel bg-[#151725] border border-white/5 p-5 rounded-2xl relative overflow-hidden group hover:border-brand-cyan/50 transition">
                  <div class="absolute right-0 top-0 w-24 h-24 bg-brand-cyan/10 rounded-full blur-2xl -mr-10 -mt-10 transition group-hover:bg-brand-cyan/20"></div>
                  <div class="text-gray-400 mb-1 text-xs uppercase font-bold tracking-wider">Torneos Activos</div>
                  <div class="text-3xl font-display font-bold text-white">3</div>
                  <div class="text-green-400 text-xs mt-2 flex items-center gap-1"><i class="fas fa-arrow-up"></i> 1 nuevo esta semana</div>
              </div>
              <!-- Stat 2 -->
              <div class="glass-panel bg-[#151725] border border-white/5 p-5 rounded-2xl relative overflow-hidden group hover:border-brand-purple/50 transition">
                  <div class="absolute right-0 top-0 w-24 h-24 bg-brand-purple/10 rounded-full blur-2xl -mr-10 -mt-10 transition group-hover:bg-brand-purple/20"></div>
                  <div class="text-gray-400 mb-1 text-xs uppercase font-bold tracking-wider">Inscritos Totales</div>
                  <div class="text-3xl font-display font-bold text-white">248</div>
                  <div class="text-green-400 text-xs mt-2 flex items-center gap-1"><i class="fas fa-arrow-up"></i> 12% vs mes anterior</div>
              </div>
              <!-- Stat 3 -->
              <div class="glass-panel bg-[#151725] border border-white/5 p-5 rounded-2xl relative overflow-hidden group hover:border-yellow-500/50 transition">
                  <div class="absolute right-0 top-0 w-24 h-24 bg-yellow-500/10 rounded-full blur-2xl -mr-10 -mt-10 transition group-hover:bg-yellow-500/20"></div>
                  <div class="text-gray-400 mb-1 text-xs uppercase font-bold tracking-wider">Ingresos (Fees)</div>
                  <div class="text-3xl font-display font-bold text-white">$12,450</div>
                  <div class="text-gray-500 text-xs mt-2">MXN acumulados este mes</div>
              </div>
              <!-- Stat 4 -->
              <div class="glass-panel bg-[#151725] border border-white/5 p-5 rounded-2xl relative overflow-hidden group hover:border-red-500/50 transition">
                  <div class="absolute right-0 top-0 w-24 h-24 bg-red-500/10 rounded-full blur-2xl -mr-10 -mt-10 transition group-hover:bg-red-500/20"></div>
                  <div class="text-gray-400 mb-1 text-xs uppercase font-bold tracking-wider">Vistas en Vivo</div>
                  <div class="text-3xl font-display font-bold text-white">1.2k</div>
                  <div class="text-red-400 text-xs mt-2 flex items-center gap-1"><i class="fas fa-circle text-[8px] animate-pulse"></i> Live ahora</div>
              </div>
          </div>

          <!-- Charts & Activity Row -->
          <div class="grid grid-cols-1 lg:grid-cols-3 gap-6 mb-8">
                <!-- Chart Area (Simulado visualmente para no depender de librerías externas en este paso) -->
                <div class="glass-panel bg-[#151725] border border-white/5 p-6 rounded-2xl lg:col-span-2">
                    <h3 class="font-bold text-lg mb-4 flex items-center justify-between text-white">
                        Rendimiento de Inscripciones
                        <select class="bg-black border border-gray-700 rounded px-2 py-1 text-xs text-gray-400 outline-none">
                            <option>Últimos 30 días</option>
                            <option>Este año</option>
                        </select>
                    </h3>
                    <div class="h-64 w-full flex items-end justify-between gap-2 px-4 pb-2 border-b border-l border-gray-700 relative">
                        <!-- Mock Bars -->
                        <div class="w-full bg-brand-cyan/20 hover:bg-brand-cyan/40 transition rounded-t relative group" style="height: 30%">
                            <div class="absolute -top-6 left-1/2 -translate-x-1/2 bg-black text-xs px-2 py-1 rounded text-white opacity-0 group-hover:opacity-100 transition">12</div>
                        </div>
                        <div class="w-full bg-brand-cyan/20 hover:bg-brand-cyan/40 transition rounded-t relative group" style="height: 45%">
                             <div class="absolute -top-6 left-1/2 -translate-x-1/2 bg-black text-xs px-2 py-1 rounded text-white opacity-0 group-hover:opacity-100 transition">19</div>
                        </div>
                        <div class="w-full bg-brand-cyan/20 hover:bg-brand-cyan/40 transition rounded-t relative group" style="height: 35%">
                             <div class="absolute -top-6 left-1/2 -translate-x-1/2 bg-black text-xs px-2 py-1 rounded text-white opacity-0 group-hover:opacity-100 transition">15</div>
                        </div>
                        <div class="w-full bg-brand-cyan/20 hover:bg-brand-cyan/40 transition rounded-t relative group" style="height: 60%">
                             <div class="absolute -top-6 left-1/2 -translate-x-1/2 bg-black text-xs px-2 py-1 rounded text-white opacity-0 group-hover:opacity-100 transition">25</div>
                        </div>
                        <div class="w-full bg-brand-cyan/20 hover:bg-brand-cyan/40 transition rounded-t relative group" style="height: 75%">
                             <div class="absolute -top-6 left-1/2 -translate-x-1/2 bg-black text-xs px-2 py-1 rounded text-white opacity-0 group-hover:opacity-100 transition">32</div>
                        </div>
                        <div class="w-full bg-brand-cyan/20 hover:bg-brand-cyan/40 transition rounded-t relative group" style="height: 90%">
                             <div class="absolute -top-6 left-1/2 -translate-x-1/2 bg-black text-xs px-2 py-1 rounded text-white opacity-0 group-hover:opacity-100 transition">40</div>
                        </div>
                        <div class="w-full bg-brand-cyan/20 hover:bg-brand-cyan/40 transition rounded-t relative group" style="height: 85%">
                             <div class="absolute -top-6 left-1/2 -translate-x-1/2 bg-black text-xs px-2 py-1 rounded text-white opacity-0 group-hover:opacity-100 transition">38</div>
                        </div>
                    </div>
                    <div class="flex justify-between text-xs text-gray-500 mt-2 px-2">
                        <span>Lun</span><span>Mar</span><span>Mie</span><span>Jue</span><span>Vie</span><span>Sab</span><span>Dom</span>
                    </div>
                </div>

                <!-- Activity Feed -->
                <div class="glass-panel bg-[#151725] border border-white/5 p-6 rounded-2xl">
                    <h3 class="font-bold text-lg mb-4 text-white">Actividad Reciente</h3>
                    <div class="space-y-4">
                        <div class="flex gap-3 items-start">
                            <div class="w-8 h-8 rounded-full bg-brand-cyan/20 text-brand-cyan flex items-center justify-center text-xs mt-1 shrink-0"><i class="fas fa-user-plus"></i></div>
                            <div>
                                <p class="text-sm text-gray-300"><span class="font-bold text-white">xSlayer99</span> se inscribió en <span class="text-brand-purple">Valorant Cup</span></p>
                                <p class="text-[10px] text-gray-500">Hace 2 min</p>
                            </div>
                        </div>
                        <div class="flex gap-3 items-start">
                            <div class="w-8 h-8 rounded-full bg-green-500/20 text-green-500 flex items-center justify-center text-xs mt-1 shrink-0"><i class="fas fa-money-bill-wave"></i></div>
                            <div>
                                <p class="text-sm text-gray-300">Pago de inscripción recibido ($50 MXN)</p>
                                <p class="text-[10px] text-gray-500">Hace 15 min</p>
                            </div>
                        </div>
                        <div class="flex gap-3 items-start">
                            <div class="w-8 h-8 rounded-full bg-yellow-500/20 text-yellow-500 flex items-center justify-center text-xs mt-1 shrink-0"><i class="fas fa-trophy"></i></div>
                            <div>
                                <p class="text-sm text-gray-300"><span class="font-bold text-white">Team Liquid</span> ganó la Ronda 2</p>
                                <p class="text-[10px] text-gray-500">Hace 1 hora</p>
                            </div>
                        </div>
                    </div>
                    <button class="w-full mt-4 text-xs text-brand-cyan hover:text-white transition underline">Ver todo el historial</button>
                </div>
          </div>
      </div>
  </div>
</template>