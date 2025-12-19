<template>
  <div class="bg-[#0B0C15] min-h-screen pb-20 text-white font-sans">
    <nav class="fixed top-0 z-50 flex items-center justify-between w-full h-16 px-6 border-b glass-nav border-white/10">
        <div class="flex items-center gap-4">
            <NuxtLink to="/" class="flex items-center gap-2">
                 <i class="text-xl fas fa-trophy text-brand-purple"></i>
                 <span class="text-2xl font-bold tracking-tighter font-display">RANK<span class="text-brand-cyan">IT</span></span>
            </NuxtLink>
            <div class="hidden gap-4 pl-8 ml-8 text-sm text-gray-400 border-l md:flex border-white/10">
                <NuxtLink to="/" class="transition hover:text-white">Inicio</NuxtLink>
                <a href="#" class="font-bold text-white border-b-2 border-brand-cyan">Explorar</a>
            </div>
        </div>
        <div class="flex items-center gap-4">
            <div class="hidden md:flex items-center gap-2 bg-white/5 px-3 py-1.5 rounded-full border border-white/10 focus-within:border-brand-cyan transition">
                <i class="text-xs text-gray-400 fas fa-search"></i>
                <input type="text" placeholder="Buscar torneo..." class="w-48 text-xs text-white placeholder-gray-500 bg-transparent border-none outline-none">
            </div>
            <div class="relative w-8 h-8 overflow-hidden bg-gray-700 border-2 rounded-full border-brand-cyan">
                <img src="https://i.pravatar.cc/150?img=33" alt="User">
            </div>
        </div>
    </nav>

    <div class="px-4 pt-24 mx-auto max-w-7xl sm:px-6">
        <!-- Featured Slider -->
        <div class="relative w-full h-[450px] rounded-2xl overflow-hidden mb-12 group shadow-2xl shadow-brand-purple/20">
            <div class="absolute inset-0 bg-[url('https://store-images.s-microsoft.com/image/apps.21507.13663857844271189.4c1de202-3961-4c40-a0aa-7f4f1388775a.20ed7782-0eda-4f9d-b421-4cc47492edc6')] bg-cover bg-center transition duration-700 group-hover:scale-105"></div>
            <div class="absolute inset-0 bg-gradient-to-t from-[#0B0C15] via-[#0B0C15]/40 to-transparent"></div>
            <div class="absolute bottom-0 left-0 max-w-2xl p-8 md:p-12">
                <div class="flex items-center gap-3 mb-3">
                    <span class="bg-brand-purple text-white text-[10px] font-bold px-2 py-1 rounded uppercase tracking-widest">Torneo Oficial</span>
                </div>
                <h1 class="mb-2 text-4xl font-bold leading-none font-display md:text-6xl">VALORANT MASTERS</h1>
                <div class="flex gap-4 mt-6">
                    <button class="flex items-center gap-2 px-8 py-3 font-bold text-black transition rounded-full bg-brand-cyan hover:bg-cyan-400">
                        <i class="fas fa-play"></i> Inscribirse Ahora
                    </button>
                </div>
            </div>
        </div>

        <h2 class="flex items-center gap-2 mb-6 text-2xl font-bold font-display"><i class="text-orange-500 fas fa-fire"></i> Populares Ahora</h2>
        <div class="grid grid-cols-1 gap-6 mb-12 md:grid-cols-2 lg:grid-cols-4">
             <!-- Card 1 -->
             <div class="relative overflow-hidden transition border cursor-pointer h-80 rounded-2xl group border-white/5 hover:border-brand-cyan/50">
                <div class="absolute inset-0 bg-[url('https://assets.nintendo.com/image/upload/ar_16:9,c_lpad,w_1240/b_white/f_auto/q_auto/store/software/switch/70010000010192/bad0a23863b5cca0e9dc7bd023b98b21497e7711806504b589d5eef23de8609a')] bg-cover bg-center transition transform duration-500 group-hover:scale-110"></div>
                <div class="absolute inset-0 bg-gradient-to-t from-black via-transparent to-transparent opacity-90"></div>
                <div class="absolute bottom-0 w-full p-5">
                    <h3 class="text-xl font-bold leading-tight font-display">Fortnite Zero Build</h3>
                    <div class="flex items-center justify-between pt-4 mt-4 border-t border-white/10">
                        <span class="text-xs text-gray-400">Entry: <span class="text-white">$50</span></span>
                        <button class="bg-brand-purple hover:bg-purple-600 px-4 py-1.5 rounded-full text-xs font-bold transition">Unirse</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped>
.glass-nav { background: rgba(11, 12, 21, 0.9); backdrop-filter: blur(10px); }
</style>