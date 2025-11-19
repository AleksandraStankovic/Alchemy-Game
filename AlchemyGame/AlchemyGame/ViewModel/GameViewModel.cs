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
    new Element { Name = "Fire",  IsUnlocked = true, IconPath = "pack://application:,,,/Images/fire.png" },
    new Element { Name = "Water", IsUnlocked = true, IconPath = "pack://application:,,,/Images/water.png" },
    new Element { Name = "Earth", IsUnlocked = true, IconPath = "pack://application:,,,/Images/earth.png" },
    new Element { Name = "Air",   IsUnlocked = true, IconPath = "pack://application:,,,/Images/air.png" },
        new Element { Name = "Life",   IsUnlocked = true, IconPath = "pack://application:,,,/Images/life.png" }


};

            Combinations = new List<Combination>
            {

                new Combination {Element1 = "Fire", Element2="Water", Result="Steam"},
                new Combination {Element1 = "Earth", Element2="Water", Result="Mud"},
                new Combination {Element1 = "Air", Element2="Water", Result="Cloud"},
                new Combination {Element1 = "Fire", Element2="Fire", Result="Light"}, 
                new Combination {Element1 = "Earth", Element2="Heat", Result="Brick"},
                 new Combination {Element1 = "Brick", Element2="Brick", Result="House"},
                  new Combination {Element1 = "Fire", Element2="Earth", Result="Lava"},
                   new Combination {Element1 = "Earth", Element2="Air", Result="Dust"},
                    new Combination {Element1 = "Water", Element2="Air", Result="Rain"},
                     new Combination {Element1 = "Rain", Element2="Cloud", Result="Storm"},
                      new Combination {Element1 = "Earth", Element2="Life", Result="Plant"},
                 new Combination {Element1 = "Plant", Element2="Earth", Result="Tree"},
                  new Combination {Element1 = "Tree", Element2="Tree", Result="Forest"},
                   new Combination {Element1 = "Earth", Element2="Fire", Result="Metal"},
                    new Combination {Element1 = "Metal", Element2="Tree", Result="Axe"},
                     new Combination {Element1 = "Light", Element2="Rain", Result="Rainbow"},
                        new Combination {Element1 = "House", Element2="Life", Result="Family"},
                           new Combination {Element1 = "Fire", Element2="Air", Result="Energy"},
                              new Combination {Element1 = "Light", Element2="Rain", Result="Rainbow"},
                     new Combination {Element1 = "Axe", Element2="Tree", Result="Wood"},

              
                 
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
                "Mud" => basePath + "mud.png",
                "Energy" => basePath + "energy.png",
                "Cloud" => basePath + "cloud.png",
                "Heat" => basePath + "heat.png",
                "Brick" => basePath + "brick.png",
                "House" => basePath + "house.png",
                "Lava" => basePath + "lava.png",
                "Dust" => basePath + "dust.png",
                "Rain" => basePath + "rain.png",
                "Storm" => basePath + "storm.png",
                "Plant" => basePath + "plant.png",
                "Tree" => basePath + "tree.png",
                "Forest" => basePath + "forest.png",
                "Metal" => basePath + "metal.png",
                "Axe" => basePath + "axe.png",
                "Wood" => basePath + "wood.png",
                "Smoke" => basePath + "smoke.png",
                "Rainbow" => basePath + "rainbow.png",
                "Family" => basePath + "family.png",

                _ => basePath + "unknown.png"
            };
        }

    }
    
}
