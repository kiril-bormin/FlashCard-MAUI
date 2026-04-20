using FlashCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlashCard.Services
{
    public class JsonDataService
    {
        private readonly string _filePath;

        public JsonDataService()
        {
            // On récupère le chemin d'accès au fichier de la db
            _filePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "decks.json"
            );
        }

        public async Task<List<Deck>> LoadDecksAsync()
        {
            try
            {
                if (!File.Exists(_filePath)) // Si le fichier n'existe pas encore, on renvoi la liste vide
                {
                    return new List<Deck>();
                }

                string json = await File.ReadAllTextAsync(_filePath); // on lit le contenu du fichier de manière asynchrone
                List<Deck>? decks = JsonSerializer.Deserialize<List<Deck>>(json); // ? Signifie que la liste peut être nulle.
                                                                                  // Methode Deserialize fait que chaque bloc {} dans le fichier deck.json
                                                                                  // devient un objet Deck dans la RAM d'appareil
               
                return decks ?? new List<Deck>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading: {ex.Message}");
                return new List<Deck>(); // retourne une liste vide si n'a pas réussi de charger les données depuis le fichier
            }
        }

        public async Task SaveDecksAsync(List<Deck> decks) //reçois la liste des decks en paramètres
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions //on crée objet "options" de type Json..., dans lequel on change un paramètre
                {
                    WriteIndented = true //ajoute des retours à la ligne pour le meuilleur affichage
                };
                string json = JsonSerializer.Serialize(decks, options); // ça transforme chaque objet Deck de la lise en texte json,
                                                                        // et ça stock le résultat dans la variable json
                await File.WriteAllTextAsync(_filePath, json); // ça prend tout le json crée, et l'enregistre dans le fichier à _filePath (si le fichier existe, il le met à jour)
                   
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving: {ex.Message}");
            }
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}
