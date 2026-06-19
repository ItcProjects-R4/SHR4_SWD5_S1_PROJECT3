using System;
using System.IO;
using System.Text.Json;

namespace Etmen_BLL.Helpers
{
    public static class OutbreakSettingsHelper
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "outbreak_settings.json");

        public static double ClusteringRadiusKm { get; set; } = 15.0;
        public static int MinCasesPerZone { get; set; } = 5;

        static OutbreakSettingsHelper()
        {
            Load();
        }

        public static void Load()
        {
            try
            {
                string path = FilePath;
                if (!File.Exists(path))
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "outbreak_settings.json");
                }
                if (!File.Exists(path))
                {
                    // Fallback to project root directory
                    var parent = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
                    while (parent != null)
                    {
                        var candidate = Path.Combine(parent.FullName, "Etmen_PL", "outbreak_settings.json");
                        if (File.Exists(candidate))
                        {
                            path = candidate;
                            break;
                        }
                        parent = parent.Parent;
                    }
                }

                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    using var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("ClusteringRadiusKm", out var rProp))
                        ClusteringRadiusKm = rProp.GetDouble();
                    if (doc.RootElement.TryGetProperty("MinCasesPerZone", out var mProp))
                        MinCasesPerZone = mProp.GetInt32();
                }
            }
            catch { }
        }

        public static void Save(double radius, int minCases)
        {
            try
            {
                ClusteringRadiusKm = radius;
                MinCasesPerZone = minCases;
                var json = JsonSerializer.Serialize(new { ClusteringRadiusKm, MinCasesPerZone }, new JsonSerializerOptions { WriteIndented = true });
                
                // Try saving in multiple places to ensure persistence
                File.WriteAllText(FilePath, json);

                var localPath = Path.Combine(Directory.GetCurrentDirectory(), "outbreak_settings.json");
                File.WriteAllText(localPath, json);

                var parent = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
                while (parent != null)
                {
                    var candidateDir = Path.Combine(parent.FullName, "Etmen_PL");
                    if (Directory.Exists(candidateDir))
                    {
                        File.WriteAllText(Path.Combine(candidateDir, "outbreak_settings.json"), json);
                        break;
                    }
                    parent = parent.Parent;
                }
            }
            catch { }
        }
    }
}
