<script setup>
// Dashboard Home
</script>

<template>
  <div>
      <!-- Topbar Adaptativo -->
      <header class="h-20 border-b flex items-center justify-between px-8 backdrop-blur-md sticky top-0 z-30 transition-colors duration-300
          bg-white/80 border-gray-200 
          dark:bg-[#050505]/90 dark:border-[#333]">
          <div>
              <h2 class="text-2xl font-bold tracking-tight text-gray-900 uppercase transition-colors font-display dark:text-white">Resumen <span class="text-[var(--rankit-neon)]">General</span></h2>
              <p class="mt-1 font-mono text-xs text-gray-500">BIENVENIDO, ORGANIZADOR.</p>
          </div>
          <div class="flex items-center gap-6">
                <div class="relative cursor-pointer group">
                    <i class="text-xl text-gray-400 transition ph ph-bell group-hover:text-black dark:group-hover:text-white"></i>
                    <span class="absolute -top-1 -right-1 w-2 h-2 bg-[var(--rankit-neon)] rounded-full animate-pulse"></span>
                </div>
                <button class="px-5 py-2 text-xs font-bold tracking-wider uppercase btn-skew">
                    <span class="flex items-center gap-2 btn-content">
                        <i class="text-lg ph ph-plus-circle"></i> Crear Torneo
                    </span>
                </button>
            </div>
      </header>

      <div class="w-full p-8 mx-auto max-w-7xl">
          <!-- Stats Grid -->
          <div class="grid grid-cols-1 gap-6 mb-8 md:grid-cols-4">
               <!-- Stat 1 -->
               <div class="p-6 brutal-card group">
                  <div class="flex items-start justify-between mb-2">
                      <div class="text-gray-500 text-[10px] uppercase font-bold tracking-[0.2em]">Torneos Activos</div>
                      <i class="ph ph-trophy text-[var(--rankit-neon)] text-xl"></i>
                  </div>
                  <div class="mb-2 text-4xl font-bold text-gray-900 transition-colors font-display dark:text-white">3</div>
                  <div class="flex items-center gap-1 text-xs font-bold text-green-500">
                      <i class="ph ph-arrow-up-right"></i> 1 nuevo esta semana
                  </div>
              </div>
              <!-- Stat 2 -->
              <div class="p-6 brutal-card group">
                  <div class="flex items-start justify-between mb-2">
                      <div class="text-gray-500 text-[10px] uppercase font-bold tracking-[0.2em]">Inscritos Totales</div>
                      <!-- Fix: Icono más oscuro en modo claro para visibilidad -->
                      <i class="text-xl text-gray-400 transition-colors ph ph-users dark:text-white group-hover:text-black dark:group-hover:text-white"></i>
                  </div>
                  <div class="mb-2 text-4xl font-bold text-gray-900 transition-colors font-display dark:text-white">248</div>
                  <div class="flex items-center gap-1 text-xs font-bold text-green-500">
                      <i class="ph ph-trend-up"></i> 12% vs mes anterior
                  </div>
              </div>
              <!-- Stat 3 -->
              <div class="p-6 brutal-card group">
                  <div class="flex items-start justify-between mb-2">
                      <div class="text-gray-500 text-[10px] uppercase font-bold tracking-[0.2em]">Ingresos (Fees)</div>
                      <i class="text-xl text-gray-400 transition-colors ph ph-currency-dollar dark:text-white group-hover:text-black dark:group-hover:text-white"></i>
                  </div>
                  <div class="mb-2 text-4xl font-bold text-gray-900 transition-colors font-display dark:text-white">$12,450</div>
                  <div class="font-mono text-xs text-gray-500">MXN acumulados este mes</div>
              </div>
              <!-- Stat 4 -->
              <div class="brutal-card p-6 group border-[var(--rankit-neon)] shadow-[0_0_15px_rgba(191,0,255,0.1)]">
                  <div class="flex items-start justify-between mb-2">
                      <div class="text-[var(--rankit-neon)] text-[10px] uppercase font-bold tracking-[0.2em]">Vistas en Vivo</div>
                      <div class="w-2 h-2 bg-red-500 rounded-full animate-pulse shadow-[0_0_10px_red]"></div>
                  </div>
                  <div class="mb-2 text-4xl font-bold text-gray-900 transition-colors font-display dark:text-white">1.2k</div>
                  <div class="text-xs font-bold tracking-wider text-red-500 uppercase">
                      Streaming Live
                  </div>
              </div>
          </div>

          <!-- Charts & Activity Row -->
          <div class="grid grid-cols-1 gap-6 mb-8 lg:grid-cols-3">
                <!-- Chart Area -->
                <div class="p-6 brutal-card lg:col-span-2">
                    <h3 class="flex items-center justify-between mb-6 text-lg font-bold text-gray-900 uppercase transition-colors font-display dark:text-white">
                        Rendimiento
                        <!-- Fix: Select adaptativo -->
                        <select class="border text-xs p-1 outline-none focus:border-[var(--rankit-neon)] font-sans transition-colors rounded
                            bg-gray-50 border-gray-200 text-gray-700
                            dark:bg-black dark:border-[#333] dark:text-gray-400">
                            <option>Últimos 30 días</option>
                            <option>Este año</option>
                        </select>
                    </h3>
                    <div class="h-64 w-full flex items-end justify-between gap-2 px-4 pb-2 border-b relative transition-colors
                        border-gray-200 dark:border-[#333]">
                        <!-- Mock Bars Adaptativas -->
                        <div v-for="h in [30, 45, 35, 60, 75, 90, 85]" :key="h" 
                             class="w-full transition-all duration-300 relative group
                                bg-gray-200 hover:bg-gray-400
                                dark:bg-[#1a1a1a] dark:hover:bg-[var(--rankit-neon)]" 
                             :style="`height: ${h}%`">
                             <!-- Tooltip -->
                             <div class="absolute px-2 py-1 text-xs font-bold text-white transition-opacity -translate-x-1/2 bg-black opacity-0 -top-8 left-1/2 group-hover:opacity-100 whitespace-nowrap arrow-bottom dark:bg-white dark:text-black">
                                 {{ h }} Jugadores
                             </div>
                        </div>
                    </div>
                    <div class="flex justify-between text-[10px] text-gray-400 mt-3 font-mono uppercase">
                        <span>Lun</span><span>Mar</span><span>Mie</span><span>Jue</span><span>Vie</span><span>Sab</span><span>Dom</span>
                    </div>
                </div>

                <!-- Activity Feed -->
                <div class="p-6 brutal-card">
                    <h3 class="mb-6 text-lg font-bold text-gray-900 uppercase transition-colors font-display dark:text-white">Actividad Reciente</h3>
                    <div class="relative space-y-6">
                        <!-- Linea conectora -->
                        <div class="absolute left-[15px] top-2 bottom-2 w-[1px] bg-gray-200 dark:bg-[#333]"></div>

                        <div class="relative z-10 flex items-start gap-4">
                            <div class="w-8 h-8 border flex items-center justify-center text-[var(--rankit-neon)] text-sm shrink-0 transition-colors rounded-full
                                bg-white border-gray-200 dark:bg-black dark:border-[#333]">
                                <i class="ph ph-user-plus"></i>
                            </div>
                            <div>
                                <p class="text-sm leading-tight text-gray-600 dark:text-gray-300">
                                    <span class="font-bold text-gray-900 dark:text-white">xSlayer99</span> se inscribió en <span class="text-[var(--rankit-neon)]">Valorant Cup</span>
                                </p>
                                <p class="text-[10px] text-gray-400 font-mono mt-1">HACE 2 MIN</p>
                            </div>
                        </div>
                        <div class="relative z-10 flex items-start gap-4">
                            <div class="w-8 h-8 border flex items-center justify-center text-green-500 text-sm shrink-0 transition-colors rounded-full
                                bg-white border-gray-200 dark:bg-black dark:border-[#333]">
                                <i class="ph ph-money"></i>
                            </div>
                            <div>
                                <p class="text-sm leading-tight text-gray-600 dark:text-gray-300">
                                    Pago de inscripción recibido <span class="font-bold text-gray-900 dark:text-white">($50 MXN)</span>
                                </p>
                                <p class="text-[10px] text-gray-400 font-mono mt-1">HACE 15 MIN</p>
                            </div>
                        </div>
                        <div class="relative z-10 flex items-start gap-4">
                            <div class="w-8 h-8 border flex items-center justify-center text-yellow-500 text-sm shrink-0 transition-colors rounded-full
                                bg-white border-gray-200 dark:bg-black dark:border-[#333]">
                                <i class="ph ph-trophy"></i>
                            </div>
                            <div>
                                <p class="text-sm leading-tight text-gray-600 dark:text-gray-300">
                                    <span class="font-bold text-gray-900 dark:text-white">Team Liquid</span> ganó la Ronda 2
                                </p>
                                <p class="text-[10px] text-gray-400 font-mono mt-1">HACE 1 HORA</p>
                            </div>
                        </div>
                    </div>
                    <button class="w-full mt-6 text-xs font-bold uppercase tracking-widest transition border-t pt-4
                        text-gray-500 border-gray-200 hover:text-black
                        dark:text-gray-500 dark:border-[#333] dark:hover:text-white">
                        Ver historial completo <i class="ml-1 ph ph-arrow-right"></i>
                    </button>
                </div>
          </div>
      </div>
  </div>
</template>