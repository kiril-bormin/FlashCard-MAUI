using FlashCard.Models;

namespace FlashCard;

public static class DeckSeeder
{
    public static List<Deck> GetDefaultDecks()
    {
        return new List<Deck>
        {
            new Deck
            {
                Id = 1,
                Name = "Questions C335",
                Cards = new List<Card>
                {
                    new Card { Front = "Que veut dire l'acronyme MAUI?", Back = "Multi-platform App UI" },
                    new Card { Front = "Comment cela se fait-il que le C# fonctionne sur Android ?", Back = "Le code c# est compilé en language intermédiare, et puis exécuté par une VM qui communique avec API native Android" },
                    new Card { Front = "Comment tester une application MAUI Android sans smartphone et comment cela fonctionne-t-il ?", Back = "On va utiliser un émulateur d'un device Android sur notre ordinateur" },
                    new Card { Front = "Citer 3 alternatives à MAUI pour faire du développement mobile multi-plateforme", Back = "Flutter, React Native, Kotlin" },
                    new Card { Front = "Citer le type d'application qui permet d'avoir deux options de navigation principales, les deux options et illustrer leur rendu et fonctionnement", Back = "Flyout - menu latéral et Tabs - les onglets en bas" },
                    new Card { Front = "Avec une navigation standard (non shell), comment passer d'une page A vers une page A1 qui propose des détails sur la page A (et revenir ensuite)", Back = "Aller sur la page A1 - await Navigation.PushAsync(new PageA1()); Retour - await Navigation.PopAsync()" },
                    new Card { Front = "Avec une navigation en mode shell, comment passer de la page /etml/formations/informaticien à la page /etml/autre/noel?", Back = "await Shell.Current.GoToAsync(\"//etml/autre/noel\"); , on utilise // pour réinitialiser la pile de navigation" },
                    new Card { Front = "Citer les 4 layouts de base et décrire leur comportement selon le contenu", Back = "StackLayout - Organise les éléments en une seule ligne (horizonrale ou verticale), Grid - pour les lignes et colonnes, FlexLayout - similaire à StackLayout mais s'adapte si manque de place, AbsoluteLayout - placement des éléments avec des coordonnées fixes." },
                    new Card { Front = "À quoi servent respectivement les fichiers d'extension .xaml et .xaml.cs ?", Back = ".xaml sert à définir l'affichage de la page, et .xaml.cs sert à créer la logie en c# (code behind)" },
                    new Card { Front = "Comment peut-on limiter un champ texte à des caractères numériques?", Back = "On va utiliser <Entry Keyboard=\"Numeric\"/>" },
                    new Card { Front = "Que veut dire le \"async\" de Shell.Current.GoToAsync ou de DisplayPromptAsync?", Back = "C'est une méthode asynchrone, qui permet d'éviter que l'application \"gèle\" durant l'attente" },
                    new Card { Front = "Citer 2 alternatives au code behind", Back = "Le pattern MVVM (Model-View-ViewModel), Le Data Binding (Liaison de données)." },
                    new Card { Front = "Comment faire pour que le contenu d'une liste soit sauvegardé entre les redémarrage de l'application ?", Back = "Il faut utiliser une db en locale sur l'appareil" },
                    new Card { Front = "À quelle fréquence les données de l'accéléromètre sont transmises à une application Android ?", Back = "La fréquance n'est pas fixe, on peut la choisir lors d'activation du capteur SensorSpeed, Les options sont : Default (fréquance standard, 200ms), UI (màj de l'interface sans trop drainer la batterie, 60 ms), Game(conçu pour les jeux, 20ms), Fastest(le plus rapide, 0ms)" },
                    new Card { Front = "L'accéléromètre permet de détecter les mouvements sur quels axes ?", Back = "Il détecte l'accélération sur 3 axes tridimensionnels : X (latéral / gauche-droite), Y (vertical / haut-bas) et Z (avant-arrière)" },
                    new Card { Front = "En quoi les capteurs peuvent impacter particulièrement négativement l'autonomie d'un téléphone", Back = "Laisser les capteurs actifs (surtout avec une vitesse élevée comme Fastest) empêche le processeur du téléphone de se mettre en veille (Deep Sleep), ce qui draine énormément la batterie." }
                }
            }
        };
    }
}