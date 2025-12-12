<script setup>
import { ref } from 'vue'

const activeTab = ref('bracket')

// Mock Data
const bracket = ref({
    quarterfinals: [
        { id: 'QF1', team1: "Dragones", team2: "Kraken", score1: 2, score2: 0, status: 'finished' },
        { id: 'QF2', team1: "Viper", team2: "Fénix", score1: 1, score2: 2, status: 'finished' },
        { id: 'QF3', team1: "Alpha", team2: "Beta", score1: 0, score2: 0, status: 'scheduled' },
        { id: 'QF4', team1: "Gamma", team2: "Delta", score1: 0, score2: 0, status: 'scheduled' },
    ],
    semifinals: [
        { id: 'SF1', team1: "Dragones", team2: "Fénix", score1: 0, score2: 0, status: 'live' },
        { id: 'SF2', team1: "TBD", team2: "TBD", score1: 0, score2: 0, status: 'scheduled' },
    ],
    finals: [
        { id: 'FINAL', team1: "TBD", team2: "TBD", score1: 0, score2: 0, status: 'scheduled' },
    ]
})

const initialGroups = [
    { name: "Grupo A", teams: [{id:1, name:"Dragones", pts:9, w:3, l:0}, {id:2, name:"Fénix", pts:6, w:2, l:1}] },
    { name: "Grupo B", teams: [{id:5, name:"Viper", pts:7, w:2, l:0}, {id:6, name:"Kraken", pts:5, w:1, l:1}] }
]
</script>

<template>
  <div class="min-h-screen bg-[#050b14] text-slate-200 font-sans" style="background-image: linear-gradient(to bottom, #050b14, #0f172a);">
    <nav class="bg-[#0f172a] border-b border-white/10 p-4 sticky top-0 z-50 shadow-lg">
        <div class="max-w-7xl mx-auto flex justify-between items-center">
            <div class="flex items-center gap-3">
                <div class="bg-blue-600 text-white font-bold p-2 rounded">TC</div>
                <h1 className="text-2xl font-display text-white tracking-widest uppercase">Torneo Control</h1>
            </div>
            <div class="flex gap-4">
                <button @click="activeTab = 'groups'" :class="['px-4 py-2 rounded text-sm font-bold uppercase', activeTab === 'groups' ? 'bg-blue-600 text-white' : 'text-slate-400 hover:bg-white/5']">Grupos</button>
                <button @click="activeTab = 'bracket'" :class="['px-4 py-2 rounded text-sm font-bold uppercase', activeTab === 'bracket' ? 'bg-blue-600 text-white' : 'text-slate-400 hover:bg-white/5']">Playoffs</button>
            </div>
        </div>
    </nav>

    <main class="max-w-7xl mx-auto p-8">
        <!-- VISTA GRUPOS -->
        <div v-if="activeTab === 'groups'" class="grid md:grid-cols-2 gap-6">
            <div v-for="(group, i) in initialGroups" :key="i" class="bg-[#1e293b] rounded border border-slate-700 p-4">
                <h3 class="text-xl font-display font-bold text-white mb-4 border-l-4 border-blue-500 pl-2">{{ group.name }}</h3>
                <table class="w-full text-sm text-left">
                     <thead class="text-xs text-slate-500 uppercase bg-slate-900/50"><tr><th class="p-2">Team</th><th class="p-2 text-center">PTS</th></tr></thead>
                     <tbody class="divide-y divide-slate-700">
                        <tr v-for="t in group.teams" :key="t.id"><td class="p-2 font-bold">{{ t.name }}</td><td class="p-2 text-center font-bold text-white">{{ t.pts }}</td></tr>
                     </tbody>
                </table>
            </div>
        </div>

        <!-- VISTA BRACKETS -->
        <div v-else class="overflow-x-auto pb-20">
            <div class="flex flex-row gap-12 justify-center p-8 min-w-max">
                <!-- Columna Cuartos -->
                <div class="flex flex-col justify-around gap-8">
                    <div class="text-center text-slate-500 text-xs font-bold uppercase">Cuartos</div>
                    <div v-for="match in bracket.quarterfinals" :key="match.id" class="match-card relative" :class="match.status">
                        <div v-if="match.status === 'live'" class="absolute -top-2 -right-2 bg-red-600 text-white text-[9px] font-bold px-2 py-0.5 rounded animate-pulse">LIVE</div>
                        <div class="bg-[#1e293b] w-60 border-l-4 p-3 rounded shadow-lg" :class="match.status === 'live' ? 'border-red-500' : 'border-slate-600'">
                            <div class="flex justify-between items-center mb-1">
                                <span :class="match.score1 > match.score2 ? 'text-white' : 'text-slate-400'">{{ match.team1 }}</span>
                                <span class="font-mono font-bold text-blue-400">{{ match.score1 }}</span>
                            </div>
                            <div class="flex justify-between items-center">
                                <span :class="match.score2 > match.score1 ? 'text-white' : 'text-slate-400'">{{ match.team2 }}</span>
                                <span class="font-mono font-bold text-blue-400">{{ match.score2 }}</span>
                            </div>
                        </div>
                         <!-- Conector visual -->
                        <div class="absolute -right-6 top-1/2 w-6 h-0.5 bg-slate-600"></div>
                    </div>
                </div>

                <!-- Columna Semis -->
                <div class="flex flex-col justify-around gap-20">
                    <div class="text-center text-slate-500 text-xs font-bold uppercase">Semis</div>
                     <div v-for="match in bracket.semifinals" :key="match.id" class="relative">
                        <div class="bg-[#1e293b] w-60 border-l-4 border-slate-600 p-3 rounded shadow-lg">
                             <div class="flex justify-between mb-1"><span class="text-slate-400">{{ match.team1 }}</span><span class="text-slate-500">{{ match.score1 }}</span></div>
                             <div class="flex justify-between"><span class="text-slate-400">{{ match.team2 }}</span><span class="text-slate-500">{{ match.score2 }}</span></div>
                        </div>
                        <div class="absolute -right-6 top-1/2 w-6 h-0.5 bg-slate-600"></div>
                     </div>
                </div>

                <!-- Columna Final -->
                <div class="flex flex-col justify-center">
                    <div class="text-center text-yellow-500 text-xs font-bold uppercase mb-4">Gran Final</div>
                    <div class="bg-[#1e293b] w-60 border-l-4 border-yellow-500 p-4 rounded shadow-lg flex flex-col items-center gap-2">
                         <div class="text-4xl animate-bounce">🏆</div>
                         <div class="text-slate-400 text-sm">TBD vs TBD</div>
                    </div>
                </div>
            </div>
        </div>
    </main>
  </div>
</template>