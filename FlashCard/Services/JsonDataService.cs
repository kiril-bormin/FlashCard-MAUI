using FlashCard.Models;
using System.Text.Json;

namespace FlashCard.Services
{
    public class JsonDataService
    {
        private readonly string _filePath;

        public JsonDataService()
        {
            // Chemin d'accès au fichier de persistance
            _filePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "decks.json"
            );
        }

        public async Task<List<Deck>> LoadDecksAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<Deck>();
                }

                string json = await File.ReadAllTextAsync(_filePath);
                List<Deck>? decks = JsonSerializer.Deserialize<List<Deck>>(json);
               
                return decks ?? new List<Deck>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur de chargement : {ex.Message}");
                return new List<Deck>();
            }
        }

        public async Task SaveDecksAsync(List<Deck> decks)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(decks, options);
                await File.WriteAllTextAsync(_filePath, json);
                   
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur de sauvegarde : {ex.Message}");
            }
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}
