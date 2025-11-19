using AlchemyGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyGame.Model;

namespace AlchemyGame.ViewModel
{

    
        public class GameViewModel
        {

            public ObservableCollection<Element> Elements { get; set; }
            public List<Combination> Combinations { get; set; }


            public GameViewModel()
            {

            Elements = new ObservableCollection<Element>
{
    new Element { Name = "Fire",  IsUnlocked = true, IconPath = "pack://application:,,,/Images/fire.jpg" },
    new Element { Name = "Water", IsUnlocked = true, IconPath = "pack://application:,,,/Images/water.png" },
    new Element { Name = "Earth", IsUnlocked = true, IconPath = "pack://application:,,,/Images/earth.png" },
    new Element { Name = "Air",   IsUnlocked = true, IconPath = "pack://application:,,,/Images/air.jpg" }
};

            Combinations = new List<Combination>
            {

                new Combination {Element1 = "Fire", Element2="Water", Result="Steam"},
                new Combination {Element1 = "Earth", Element2="Water", Result="Mud"},
                new Combination {Element1 = "Fire", Element2="Air", Result="Energy"}//promijeniti ovo
            };
            }
        public string TryCombine(string e1, string e2)
        {
            var combo = Combinations.FirstOrDefault(c =>
            (c.Element1 == e1 && c.Element2 == e2) ||
            (c.Element1 == e2 && c.Element2 == e1));

            if (combo != null)
            {
                // Check if this element is ALREADY unlocked
                if (!Elements.Any(e => e.Name == combo.Result))
                {
                    // FIRST TIME - add it and return name (popup will show)
                    Elements.Add(new Element
                    {
                        Name = combo.Result,
                        IsUnlocked = true,
                        IconPath = GetIconPath(combo.Result) // Add proper icon path
                    });
                    return combo.Result;
                }
                // Already exists - return null (no popup)
                return null;
            }
            return null;
        }

        // Helper method - add this to GameViewModel
        private string GetIconPath(string elementName)
        {
            const string basePath = "pack://application:,,,/Images/";
            return elementName switch
            {
                "Steam" => basePath + "steam.png",
                "Mud" => basePath + "mud.jpg",
                "Energy" => basePath + "energy.png",
                _ => basePath + "unknown.png"
            };
        }

    }
    
}
