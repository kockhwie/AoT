using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AOT.Services;



// same stable-id pattern as MangaArc — display text lives in resx via CharacterFactionKeys.
public enum CharacterFaction
{
    Main, Titans, SurveyCorps, SurveyCorpsSpecialOps, Garrison, MilitaryPolice,
    FirstInteriorSquad, RulingFamily, RoyalGovernment, Civilians, Marley,
    TyburFamily, Yeagerists, Other
}


public sealed class CharacterProfile
{
    public string Slug { get; init; } = "";// url segment, e.g. "mikasa-ackerman"
    public string NameKey { get; init; } = "";
    public string TitleKey { get; init; } = "";
    public string BioKey { get; init; } = "";
    public string Portrait { get; init; } = "";// wwwroot/img/characters/{Portrait}
    public List<CharacterFaction> Factions { get; init; } = new(); // was single Faction — a character appears on every faction shelf listed here
    public string Badge { get; init; } = "";
   
}

public static class CharacterData
{
    public static readonly List<CharacterProfile> Profiles = new()
    {

       /*                          
            <li class="menu-title"><span>@LocalizationService.GetString("Nav_Captains")</span></li>
            <li><a>@LocalizationService.GetString("Nav_LeviAckerman")</a></li>

 
            <li><a>@LocalizationService.GetString("Nav_LeviAckerman")</a></li>
            <li><a>@LocalizationService.GetString("Nav_EldJinn")</a></li>
            <li><a>@LocalizationService.GetString("Nav_OluoBozado")</a></li>
            <li><a>@LocalizationService.GetString("Nav_PetraRal")</a></li>
            <li><a>@LocalizationService.GetString("Nav_GntherSchultz")</a></li>


            <li><a>@LocalizationService.GetString("Nav_Hannes")</a></li>
            <li><a>@LocalizationService.GetString("Nav_DotPixis")</a></li>
            <li><a>@LocalizationService.GetString("Nav_RicoBrzenska")</a></li>
        */

        // --- Main / Survey Corps ---
        new CharacterProfile { Slug = "levi-ackerman", NameKey = "Nav_LeviAckerman", TitleKey = "C1Title", BioKey = "C1Bio", Portrait = "levi-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorpsSpecialOps, CharacterFaction.SurveyCorps], Badge = "S RANK" }, // Nav_Captains
        new CharacterProfile { Slug = "mikasa-ackerman", NameKey = "Nav_Mikasa", TitleKey = "C2Title", BioKey = "C2Bio", Portrait = "mikasa-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorps], Badge = "A+ RANK" },
        new CharacterProfile { Slug = "hange-zoe", NameKey = "Nav_HangeZoe", TitleKey = "C4Title", BioKey = "C4Bio", Portrait = "hange-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "A RANK" }, // Nav_Commanders
        new CharacterProfile { Slug = "erwin-smith", NameKey = "Nav_ErwinSmith", TitleKey = "C5Title", BioKey = "C5Bio", Portrait = "erwin-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "S RANK" }, // Nav_Commanders, Nav_SectionCommander
        new CharacterProfile { Slug = "jean-kirstein", NameKey = "C6Name", TitleKey = "C6Title", BioKey = "C6Bio", Portrait = "jean-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "A RANK" },
        new CharacterProfile { Slug = "armin-arlert", NameKey = "Nav_Armin", TitleKey = "C3Title", BioKey = "C3Bio", Portrait = "armin-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorps], Badge = "A RANK" }, // Nav_Commanders
        new CharacterProfile { Slug = "eren-yeager", NameKey = "Nav_Eren", TitleKey = "CharEren_Title", BioKey = "CharEren_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.Yeagerists], Badge = "A RANK" },
        new CharacterProfile { Slug = "historia-reiss", NameKey = "Nav_Historia", TitleKey = "CharHistoria_Title", BioKey = "CharHistoria_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.RulingFamily], Badge = "B RANK" },

        // --- Titans ---
        new CharacterProfile { Slug = "Founding-Titan",     NameKey = "Nav_FoundingTitan", TitleKey = "C3Title", BioKey = "C3Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "13M" },
        new CharacterProfile { Slug = "Final-Founding-Titan",     NameKey = "Nav_FinalFoundingTitan", TitleKey = "C3Title", BioKey = "C3Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "100M+" },
        new CharacterProfile { Slug = "Attack-Titan",       NameKey = "Nav_AttackTitan", TitleKey = "CharEren_Title", BioKey = "CharEren_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "15M" },
        new CharacterProfile { Slug = "Armored-Titan",      NameKey = "Nav_ArmoredTitan", TitleKey = "CharHistoria_Title", BioKey = "CharHistoria_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "15M" },
        new CharacterProfile { Slug = "Beast-Titan",        NameKey = "Nav_BeastTitan", TitleKey = "C3Title", BioKey = "C3Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "17M" },
        new CharacterProfile { Slug = "Cart-Titan",         NameKey = "Nav_CartTitan", TitleKey = "CharEren_Title", BioKey = "CharEren_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "4M" },
        new CharacterProfile { Slug = "Colossal-Titan",     NameKey = "Nav_ColossalTitan", TitleKey = "CharHistoria_Title", BioKey = "CharHistoria_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "60M" },
        new CharacterProfile { Slug = "Female-Titan",       NameKey = "Nav_FemaleTitan", TitleKey = "C3Title", BioKey = "C3Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "15M" },
        new CharacterProfile { Slug = "Jaw-Titan",          NameKey = "Nav_JawTitan", TitleKey = "CharEren_Title", BioKey = "CharEren_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "5M" },
        new CharacterProfile { Slug = "WarHammer-Titan",    NameKey = "Nav_WarHammerTitan", TitleKey = "CharHistoria_Title", BioKey = "CharHistoria_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "15M" },
        new CharacterProfile { Slug = "Sonny-and-Bean",     NameKey = "Nav_SonnyandBean", TitleKey = "CharEren_Title", BioKey = "CharEren_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "4M~7M" },
        new CharacterProfile { Slug = "Wall-Titans",        NameKey = "Nav_WallTitans", TitleKey = "CharHistoria_Title", BioKey = "CharHistoria_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "50M" },

        new CharacterProfile { Slug = "Eld-Jinn", NameKey = "Nav_EldJinn", TitleKey = "C2Title", BioKey = "C2Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps], Badge = "A+ RANK" },
        new CharacterProfile { Slug = "Oluo-Bozado", NameKey = "Nav_OluoBozado", TitleKey = "C2Title", BioKey = "C2Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps], Badge = "A+ RANK" },
        new CharacterProfile { Slug = "Petra-Ral", NameKey = "Nav_PetraRal", TitleKey = "C2Title", BioKey = "C2Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps], Badge = "A+ RANK" },
        new CharacterProfile { Slug = "Gnther-Schultz", NameKey = "Nav_GntherSchultz", TitleKey = "C2Title", BioKey = "C2Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps], Badge = "A+ RANK" },


        // --- Garrison ---
        new CharacterProfile { Slug = "hannes", NameKey = "Nav_Hannes", TitleKey = "CharGarrison_Title", BioKey = "CharGarrison_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Garrison], Badge = "B RANK" },
        new CharacterProfile { Slug = "Dot-Pixis", NameKey = "Nav_DotPixis", TitleKey = "CharGarrison_Title", BioKey = "CharGarrison_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Garrison], Badge = "B RANK" },
        new CharacterProfile { Slug = "Rico-Brzenska", NameKey = "Nav_RicoBrzenska", TitleKey = "CharGarrison_Title", BioKey = "CharGarrison_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Garrison], Badge = "B RANK" },
 

        // --- Military Police ---
        new CharacterProfile { Slug = "nile-dok", NameKey = "Nav_NileDok", TitleKey = "CharMP_Title", BioKey = "CharMP_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },
        new CharacterProfile { Slug = "Marlowe-Freudenberg", NameKey = "Nav_MarloweFreudenberg", TitleKey = "CharMP_Title", BioKey = "CharMP_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },
        new CharacterProfile { Slug = "Hitch-Dreyse", NameKey = "Nav_HitchDreyse", TitleKey = "CharMP_Title", BioKey = "CharMP_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },
        new CharacterProfile { Slug = "Boris-Feulner", NameKey = "Nav_BorisFeulner", TitleKey = "CharMP_Title", BioKey = "CharMP_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },

 
        // --- First Interior ---
        new CharacterProfile { Slug = "kenny-ackerman", NameKey = "Nav_KennyAckerman", TitleKey = "CharInterior_Title", BioKey = "CharInterior_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.FirstInteriorSquad], Badge = "S RANK" },
        new CharacterProfile { Slug = "Traute-Carven", NameKey = "Nav_TrauteCarven", TitleKey = "CharInterior_Title", BioKey = "CharInterior_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.FirstInteriorSquad], Badge = "S RANK" },
        new CharacterProfile { Slug = "Djel-Sanes", NameKey = "Nav_DjelSanes", TitleKey = "CharInterior_Title", BioKey = "CharInterior_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.FirstInteriorSquad], Badge = "S RANK" },
 

        // --- Ruling Family - Nav_RulingFamily ---
        new CharacterProfile { Slug = "Fritz", NameKey = "Nav_Fritz", TitleKey = "CharRoyal_Title", BioKey = "CharRoyal_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RulingFamily], Badge = "" },
        new CharacterProfile { Slug = "Rod-Reiss", NameKey = "Nav_RodReiss", TitleKey = "CharRoyal_Title", BioKey = "CharRoyal_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RulingFamily], Badge = "" },
        new CharacterProfile { Slug = "Frieda-Reiss", NameKey = "Nav_FriedaReiss", TitleKey = "CharRoyal_Title", BioKey = "CharRoyal_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RulingFamily], Badge = "" },
 

        // --- Royal Government — Nav_TheRoyalGovernment ---
        new CharacterProfile { Slug = "aurille", NameKey = "Nav_Aurille", TitleKey = "CharGov_Title", BioKey = "CharGov_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },
        new CharacterProfile { Slug = "Deltoff", NameKey = "Nav_Deltoff", TitleKey = "CharGov_Title", BioKey = "CharGov_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },
        new CharacterProfile { Slug = "Gerald", NameKey = "Nav_Gerald", TitleKey = "CharGov_Title", BioKey = "CharGov_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },
        new CharacterProfile { Slug = "Roderich", NameKey = "Nav_Roderich", TitleKey = "CharGov_Title", BioKey = "CharGov_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },


        // --- Civilians - Nav_Civilians ---
        new CharacterProfile { Slug = "grisha-yeager", NameKey = "Nav_GrishaYeager", TitleKey = "CharCivilian_Title", BioKey = "CharCivilian_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },
        new CharacterProfile { Slug = "Carla-Yeager", NameKey = "Nav_CarlaYeager", TitleKey = "CharCivilian_Title", BioKey = "CharCivilian_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },
        new CharacterProfile { Slug = "Minister-Nick", NameKey = "Nav_MinisterNick", TitleKey = "CharCivilian_Title", BioKey = "CharCivilian_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },
        new CharacterProfile { Slug = "Dimo-Reeves", NameKey = "Nav_DimoReeves", TitleKey = "CharCivilian_Title", BioKey = "CharCivilian_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },
        new CharacterProfile { Slug = "Kaya", NameKey = "Nav_Kaya", TitleKey = "CharCivilian_Title", BioKey = "CharCivilian_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },

        // --- Marley - Nav_Marley ---
        new CharacterProfile { Slug = "Calvi-The-Marshal", NameKey = "Nav_CalviTheMarshal", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "The Marshal" },
        new CharacterProfile { Slug = "Theo-Magath-Acommander", NameKey = "Nav_TheoMagathAcommander", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Commander" },
        new CharacterProfile { Slug = "Reiner-Braun", NameKey = "Nav_ReinerBraun", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Zeke-Yeager", NameKey = "Nav_ZekeYeager", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge =  "Warrior Unit" },
        new CharacterProfile { Slug = "Bertolt-Hoover", NameKey = "Nav_BertoltHoover", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Annie-Leonhart", NameKey = "Nav_AnnieLeonhart", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Marcel-Galliard", NameKey = "Nav_MarcelGalliard", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Porco-Galliard", NameKey = "Nav_PorcoGalliard", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Pieck-Finger", NameKey = "Nav_PieckFinger", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Gabi-Braun", NameKey = "Nav_GabiBraun", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Falco-Grice", NameKey = "Nav_FalcoGrice", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Colt-Grice", NameKey = "Nav_ColtGrice", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
 

        // --- Tybur Family ---
        new CharacterProfile { Slug = "willy-tybur", NameKey = "Nav_WillyTybur", TitleKey = "CharTybur_Title", BioKey = "CharTybur_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.TyburFamily], Badge = "B RANK" },
        new CharacterProfile { Slug = "lara-tybur", NameKey = "Nav_LaraTybur", TitleKey = "CharTybur_Title", BioKey = "CharTybur_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.TyburFamily], Badge = "B RANK" },


        // --- Yeagerists ---
        new CharacterProfile { Slug = "floch-forster", NameKey = "Nav_FlochForster", TitleKey = "CharYeagerist_Title", BioKey = "CharYeagerist_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Yeagerists], Badge = "B RANK" },
        new CharacterProfile { Slug = "eren-yeager", NameKey = "Nav_Eren", TitleKey = "CharYeagerist_Title", BioKey = "CharYeagerist_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Yeagerists], Badge = "B RANK" },
    
    
        // --- Nav_Other ---
        new CharacterProfile { Slug = "TheAntiMarleyanVolunteers", NameKey = "Nav_TheAntiMarleyanVolunteers", TitleKey = "CharYeagerist_Title", BioKey = "CharYeagerist_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Other], Badge = "B RANK" },
        new CharacterProfile { Slug = "Underground", NameKey = "Nav_Underground", TitleKey = "CharYeagerist_Title", BioKey = "CharYeagerist_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Other], Badge = "B RANK" },


    };

    public static CharacterProfile? FindBySlug(string slug) => Profiles.FirstOrDefault(c => c.Slug == slug);

    public static readonly List<CharacterFaction> FactionOrder = new()
    {
        CharacterFaction.Main, 
        CharacterFaction.Titans, 
        CharacterFaction.SurveyCorps, 
        CharacterFaction.SurveyCorpsSpecialOps,
        CharacterFaction.Garrison, 
        CharacterFaction.MilitaryPolice, 
        CharacterFaction.FirstInteriorSquad,
        CharacterFaction.RulingFamily, 
        CharacterFaction.RoyalGovernment, 
        CharacterFaction.Civilians,
        CharacterFaction.Marley, 
        CharacterFaction.TyburFamily, 
        CharacterFaction.Yeagerists, 
        CharacterFaction.Other
    };

    // enum -> resx key — reuses the Nav_* keys your dropdown already has (Nav_Main, Nav_Titans,
    // Nav_SurveyCorps...), zero new localization needed for section headers.
    public static readonly Dictionary<CharacterFaction, string> FactionNameKeys = new()
    {
        { CharacterFaction.Main, "Nav_Main" },
        { CharacterFaction.Titans, "Nav_Titans" },
        { CharacterFaction.SurveyCorps, "Nav_SurveyCorps" },
        { CharacterFaction.SurveyCorpsSpecialOps, "Nav_TheSurveyCorpsSpecialOperation" },
        { CharacterFaction.Garrison, "Nav_GarrisonRegiment" }, 
        { CharacterFaction.MilitaryPolice, "Nav_MilitaryPoliceBrigade" },
        { CharacterFaction.FirstInteriorSquad, "Nav_TheFirstInteriorSquad" },
        { CharacterFaction.RulingFamily, "Nav_RulingFamily" },
        { CharacterFaction.RoyalGovernment, "Nav_TheRoyalGovernment" }, 
        { CharacterFaction.Civilians, "Nav_Civilians" },
        { CharacterFaction.Marley, "Nav_Marley" }, 
        { CharacterFaction.TyburFamily, "Nav_TyburFamily" },
        { CharacterFaction.Yeagerists, "Nav_Yeagerists" }, 
        { CharacterFaction.Other, "Nav_Other" }
    };

    // a character appears on every faction shelf any of its Factions lists — same rule as
    // MangaVolumeModel.Arcs (a boundary volume shows on every arc shelf it touches).
    public static IEnumerable<CharacterProfile> InFaction(CharacterFaction faction) =>
        Profiles.Where(c => c.Factions.Contains(faction));
}