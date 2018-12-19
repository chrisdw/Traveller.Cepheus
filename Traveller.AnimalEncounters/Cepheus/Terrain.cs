using System.Collections.Generic;

namespace org.DownesWard.Traveller.AnimalEncounters.Cepheus
{
    public class Terrain
    {
        public Regions Region { get; set; }
        public int SubtypeDM { get; set; }
        public int SizeDM { get; set; }

        public class SubTerrain
        {
            public Motions Motion { get; set; }
            public int SizeDM { get; set; }
        }

        public List<SubTerrain> SubTerrains { get; set; } = new List<SubTerrain>();

        public static List<Terrain> Terrains = new List<Terrain>
        {
            new Terrain() { Region = Regions.Clear, SizeDM = 0, SubtypeDM = 3, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Plain, SizeDM = 0, SubtypeDM = 4, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Desert, SizeDM = -3, SubtypeDM = 3, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Hills, SizeDM = 0, SubtypeDM = 0, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Mountain, SizeDM = 0, SubtypeDM = 0, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -2 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Forest, SizeDM = -4, SubtypeDM = -4, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Woods, SizeDM = -1, SubtypeDM = -2, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Jungle, SizeDM = -3, SubtypeDM = -4, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.RainForest, SizeDM = -2, SubtypeDM = -2, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Rough, SizeDM = -3, SubtypeDM = -3, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Swamp, SizeDM = 4, SubtypeDM = -2, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = -6 },
                    new SubTerrain() { Motion = Motions.Amphibious, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Beach, SizeDM = 2, SubtypeDM = 3, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 1 },
                    new SubTerrain() { Motion = Motions.Amphibious, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Riverbank, SizeDM = 1, SubtypeDM = 1, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Amphibious, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Walking, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Shallows, SizeDM = 1, SubtypeDM = 4, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 4 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Ocean, SizeDM = -4, SubtypeDM = 4, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 6 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 4 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -4 },
                    new SubTerrain() { Motion = Motions.Flying, SizeDM = -6 }
                }
            },
            new Terrain() { Region = Regions.Deeps, SizeDM = 2, SubtypeDM = 4, SubTerrains =
                {
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 8 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 6 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 4 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 2 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = 0 },
                    new SubTerrain() { Motion = Motions.Swimming, SizeDM = -2 }
                }
            }
        };
    }
}
