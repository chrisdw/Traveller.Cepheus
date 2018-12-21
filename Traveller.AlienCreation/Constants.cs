using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace org.DownesWard.Traveller.AlienCreation
{
    public enum EcologicalTypes
    {
        Scavenger,
        Herbivore,
        Omnivore,
        Carnivore
    }

    public enum EcologicalSubtypes
    {
        Filter,
        Intermittent,
        Grazer,
        Gatherer,
        Hunter,
        Eater,
        Pouncer,
        Trapper,
        Chaser,
        Siren,
        Killer,
        Reducer,
        Hijacker,
        Intimidator,
        CarrionEater
    }


    public enum Metabolisms
    {
        [Display(Name = "Warm Blooded")]
        WarmBlooded,
        [Display(Name = "Cold Blooded")]
        ColdBlooded
    }
    public enum Genders
    {
        [Display(Name = "Asexual")]
        Asexual,
        [Display(Name = "Hermaphroditic")]
        Hermaphroditic,
        [Display(Name = "Bi-Gender")]
        BiGender,
        [Display(Name = "Multi-Gender")]
        MultiGender
    }

    public enum ReproductionMethods
    {
        [Display(Name = "External Budding")]
        ExternalBudding,
        [Display(Name = "Live Bearing")]
        LiveBearing,
        [Display(Name = "Egg Laying")]
        EggLaying
    }

    public enum Sizes
    {
        Tiny,
        Small,
        Medium,
        Large,
        Huge
    }

    public enum Symmetries
    {
        Trilateral,
        Bilateral,
        Radial
    }

    public enum MovementRates
    {
        None,
        Slow,
        Average,
        Fast
    }

}
