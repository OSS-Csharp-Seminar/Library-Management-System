using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Core.Enums
{
    public enum Genre
    {
        [Display(Name = "Action and Adventure")]
        ActionAndAdventure,

        [Display(Name = "Art")]
        Art,

        [Display(Name = "Biography")]
        Biography,

        [Display(Name = "Children's")]
        Childrens,

        [Display(Name = "Comics and Graphic Novels")]
        ComicsAndGraphicNovels,

        [Display(Name = "Cookbooks")]
        Cookbooks,

        [Display(Name = "Crime")]
        Crime,

        [Display(Name = "Fantasy")]
        Fantasy,

        [Display(Name = "Historical Fiction")]
        HistoricalFiction,

        [Display(Name = "Horror")]
        Horror,

        [Display(Name = "Humor and Comedy")]
        HumorAndComedy,

        [Display(Name = "Mystery")]
        Mystery,

        [Display(Name = "Poetry")]
        Poetry,

        [Display(Name = "Religion and Spirituality")]
        ReligionAndSpirituality,

        [Display(Name = "Romance")]
        Romance,

        [Display(Name = "Science Fiction")]
        ScienceFiction,

        [Display(Name = "Self-Help")]
        SelfHelp,

        [Display(Name = "Suspense")]
        Suspense,

        [Display(Name = "Thriller")]
        Thriller,

        [Display(Name = "Travel")]
        Travel
    }
}
