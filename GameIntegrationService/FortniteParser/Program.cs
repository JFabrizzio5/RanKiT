using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using FortniteReplayReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    // Clase auxiliar para calcular los resultados
    public class PlayerResult
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "Desconocido";
        public bool IsBot { get; set; }
        public int Kills { get; set; }
        public int Placement { get; set; } = 999; 
        
        // Sistema de Puntos: 
        // - 2 puntos por Kill
        // - 1 punto por cada puesto escalado dentro del Top 40 (Top 40 = 1pto ... Top 1 = 40ptos)
        public int Points 
        {
            get 
            {
                int placementPoints = 0;
                if (Placement > 0 && Placement <= 40)
                {
                    placementPoints = 41 - Placement;
                }
                return (Kills * 2) + placementPoints;
            }
        }
    }

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("⚠️  Falta el archivo. Uso: dotnet run -- \"ruta/archivo.replay\"");
            return;
        }

        string replayPath = args[0];
        if (!File.Exists(replayPath))
        {
            Console.WriteLine($"❌ No se encontró el archivo: {replayPath}");
            return;
        }

        var serviceCollection = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Warning));
        var provider = serviceCollection.BuildServiceProvider();
        var logger = provider.GetRequiredService<ILogger<ReplayReader>>();

        var reader = new ReplayReader(logger);

        try
        {
            Console.WriteLine($"📖 Procesando: {Path.GetFileName(replayPath)}...");
            var replay = reader.ReadReplay(replayPath);
            Console.WriteLine("✅ Replay analizado. Generando reportes separados...");

            // ---------------------------------------------------------
            // 1. Guardar JSON de depuración (Opcional)
            // ---------------------------------------------------------
            try 
            {
                var jsonOptions = new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };
                string fullJson = JsonSerializer.Serialize(replay, jsonOptions);
                File.WriteAllText("ReplayDebug.json", fullJson);
            }
            catch { }

            // =========================================================
            // CÁLCULOS (Lógica central)
            // =========================================================
            
            var playersDict = new Dictionary<string, PlayerResult>(StringComparer.OrdinalIgnoreCase);

            // 1. Llenar diccionario con nombres conocidos
            if (replay.PlayerData != null)
            {
                foreach (var p in replay.PlayerData)
                {
                    if (!string.IsNullOrEmpty(p.EpicId))
                    {
                        if (!playersDict.ContainsKey(p.EpicId))
                        {
                            playersDict.Add(p.EpicId, new PlayerResult 
                            { 
                                Id = p.EpicId, 
                                Name = string.IsNullOrEmpty(p.PlayerName) ? p.EpicId : p.PlayerName,
                                IsBot = p.IsBot
                            });
                        }
                        else if (!string.IsNullOrEmpty(p.PlayerName) && playersDict[p.EpicId].Name == p.EpicId)
                        {
                            playersDict[p.EpicId].Name = p.PlayerName;
                        }
                    }
                }
            }

            // 2. Procesar Kills y Orden de Muerte
            var deathOrder = new List<string>();

            if (replay.Eliminations != null)
            {
                foreach (var elim in replay.Eliminations)
                {
                    if (!string.IsNullOrEmpty(elim.Eliminator))
                    {
                        if (!playersDict.ContainsKey(elim.Eliminator))
                            playersDict[elim.Eliminator] = new PlayerResult { Id = elim.Eliminator, Name = elim.Eliminator };
                        
                        playersDict[elim.Eliminator].Kills++;
                    }

                    if (!string.IsNullOrEmpty(elim.Eliminated))
                    {
                        if (!playersDict.ContainsKey(elim.Eliminated))
                            playersDict[elim.Eliminated] = new PlayerResult { Id = elim.Eliminated, Name = elim.Eliminated };
                        
                        deathOrder.Add(elim.Eliminated);
                    }
                }
            }

            // 3. Calcular Top/Posiciones
            var uniqueDeaths = deathOrder
                .GroupBy(id => id, StringComparer.OrdinalIgnoreCase)
                .Select(g => g.Last()) 
                .ToList(); 

            // Identificar sobrevivientes
            var allKnownIds = playersDict.Keys.ToList();
            var survivors = allKnownIds.Where(k => !uniqueDeaths.Contains(k, StringComparer.OrdinalIgnoreCase)).ToList();
            
            int currentRank = 1;

            // Ranks 1 al N para sobrevivientes
            foreach (var survivorId in survivors)
            {
                playersDict[survivorId].Placement = 1; 
            }
            if (survivors.Count > 0) currentRank += survivors.Count; 
            else currentRank = 1;

            // Ranks para eliminados (del último al primero)
            uniqueDeaths.Reverse(); 
            foreach (var victimId in uniqueDeaths)
            {
                if (playersDict.ContainsKey(victimId))
                    playersDict[victimId].Placement = currentRank;
                currentRank++;
            }

            // Lista ordenada para el Leaderboard
            var leaderboard = playersDict.Values
                .OrderByDescending(p => p.Points)
                .ThenBy(p => p.Placement)
                .ThenByDescending(p => p.Kills)
                .ToList();

            // =========================================================
            // GENERACIÓN DE ARCHIVOS
            // =========================================================

            // ARCHIVO 1: Estadísticas del Equipo (TeamStats.txt)
            if (replay.TeamStats != null)
            {
                using (StreamWriter sw = new StreamWriter("TeamStats.txt"))
                {
                    sw.WriteLine("--- INFO DE LA PARTIDA ---");
                    sw.WriteLine($"Posición Final del Equipo: {replay.TeamStats.Position}");
                    sw.WriteLine($"Total Jugadores: {replay.TeamStats.TotalPlayers}");
                }
                Console.WriteLine("📄 'TeamStats.txt' creado.");
            }

            // ARCHIVO 2: Tabla de Clasificación (Leaderboard.txt)
            using (StreamWriter sw = new StreamWriter("Leaderboard.txt"))
            {
                int totalPlayers = replay.TeamStats?.TotalPlayers > 0 ? (int)replay.TeamStats.TotalPlayers : playersDict.Count;
                sw.WriteLine($"--- TABLA DE POSICIONES Y PUNTOS ---");
                sw.WriteLine($"Jugadores Estimados: {totalPlayers}");
                sw.WriteLine($"Reglas: +2 pts por Kill | +1 pto por cada Top a partir del 40");
                sw.WriteLine(new string('-', 85));
                sw.WriteLine($"| {"POS",-4} | {"NOMBRE",-35} | {"KILLS",-5} | {"TOP",-4} | {"PUNTOS",-6} |");
                sw.WriteLine(new string('-', 85));

                int rankCounter = 1;
                foreach (var p in leaderboard)
                {
                    // Filtro: Mostrar solo si tienen puntos o quedaron en buen puesto
                    if (p.Points > 0 || p.Placement <= 50) 
                    {
                        string botTag = p.IsBot ? "*" : " ";
                        string displayName = (p.Name.Length > 33) ? p.Name.Substring(0, 30) + "..." : p.Name;
                        sw.WriteLine($"| #{rankCounter,-3} | {displayName + botTag,-34} | {p.Kills,-5} | #{p.Placement,-3} | {p.Points,-6} |");
                    }
                    rankCounter++;
                }
                sw.WriteLine(new string('-', 85));
                sw.WriteLine("* Indica Bot/IA.");
            }
            Console.WriteLine("📄 'Leaderboard.txt' creado.");

            // ARCHIVO 3: Lista Completa de Jugadores (Players.txt)
            if (replay.PlayerData != null && replay.PlayerData.Any())
            {
                using (StreamWriter sw = new StreamWriter("Players.txt"))
                {
                    sw.WriteLine($"--- LISTA COMPLETA DE JUGADORES ({replay.PlayerData.Count()} encontrados) ---");
                    foreach (var p in replay.PlayerData)
                    {
                        var botStr = p.IsBot ? "[BOT]" : "";
                        var epicIdStr = string.IsNullOrEmpty(p.EpicId) ? "" : $"ID: {p.EpicId}";
                        sw.WriteLine($"{p.PlayerName} {botStr} \t{epicIdStr}");
                    }
                }
                Console.WriteLine("📄 'Players.txt' creado.");
            }

            Console.WriteLine("\n🏆 ¡Proceso completado! Revisa los 3 archivos .txt generados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
}