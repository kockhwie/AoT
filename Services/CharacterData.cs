using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AOT.Services;



// same stable-id pattern as MangaArc — display text lives in resx via CharacterFactionKeys.
public enum CharacterFaction
{
    Main, Titans, SurveyCorps, SurveyCorpsSpecialOps, Garrison, MilitaryPolice,
    FirstInteriorSquad, RulingFamily, RoyalGovernment, Civilians, Marley,
    TyburFamily, Yeagerists, Other
}

// a secondary portrait for characters with a distinct alternate form (titan shift, etc).
// LabelKey is a resx key shown as a small tag under the name when this version is active.
public sealed class CharacterVersion
{
    public string Portrait { get; init; } = "";
    public string LabelKey { get; init; } = "";
}

public sealed class CharacterProfile
{
    public string Slug { get; init; } = "";// url segment, e.g. "mikasa-ackerman"
    public string NameKey { get; init; } = "";
    public string TitleKey { get; init; } = "";
    public string BioKey { get; init; } = "";
    public string Portrait { get; init; } = "";// wwwroot/img/characters/{Portrait}
    public List<CharacterFaction> Factions { get; init; } = []; // was single Faction — a character appears on every faction shelf listed here
    public string Badge { get; init; } = "";
    public List<CharacterVersion>? ExtraVersions { get; init; }

    public string? FocalPosition { get; init; }
    // Pre-formatted style string so Razor markup never embeds a raw ternary/colon inside
    // style="...", which trips VS's CSS024 analyzer (CSS024: missing property name before ':').
    public string FocalStyle => string.IsNullOrEmpty(FocalPosition) ? "" : $"object-position: {FocalPosition};";
    
    // base portrait + any extra versions, each resolved to a full /img src — detail page
    // filmstrip just iterates this, no path logic in the .razor file.
    public List<(string Src, string LabelKey)> AllVersions
    {
        get
        {
            var list = new List<(string, string)>
            {
                ($"/img/characters/{Portrait}", ExtraVersions is { Count: > 0 } ? "Version_Human" : "")
            };
            if (ExtraVersions != null)
                list.AddRange(ExtraVersions.Select(v => ($"/img/titans/cards/{v.Portrait}", v.LabelKey)));
            return list;
        }
    }
}

public static class CharacterData
{
    public static readonly List<CharacterProfile> Profiles = [

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
        new CharacterProfile { Slug = "levi-ackerman", NameKey = "Name_LeviAckerman", TitleKey = "Title_LeviAckerman", BioKey = "Bio_LeviAckerman", Portrait = "levi-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorpsSpecialOps, CharacterFaction.SurveyCorps], Badge = "S RANK" }, // Nav_Captains
        new CharacterProfile { Slug = "mikasa-ackerman", NameKey = "Name_MikasaAckerman", TitleKey = "Title_MikasaAckerman", BioKey = "Bio_MikasaAckerman", Portrait = "mikasa-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorps], Badge = "A+ RANK" },
        new CharacterProfile { Slug = "hange-zoe", NameKey = "Name_HangeZoe", TitleKey = "Title_HangeZoe", BioKey = "Bio_HangeZoe", Portrait = "hange2-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "A RANK" }, // Nav_Commanders
        new CharacterProfile { Slug = "erwin-smith", NameKey = "Name_ErwinSmith", TitleKey = "Title_ErwinSmith", BioKey = "Bio_ErwinSmith", Portrait = "erwin-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "S RANK" }, // Nav_Commanders, Nav_SectionCommander
        new CharacterProfile { Slug = "jean-kirstein", NameKey = "Name_JeanKirstein", TitleKey = "Title_JeanKirstein", BioKey = "Bio_JeanKirstein", Portrait = "jean-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "A RANK" },
        new CharacterProfile { Slug = "armin-arlert", NameKey = "Name_ArminArlert", TitleKey = "Title_ArminArlert", BioKey = "Bio_ArminArlert", Portrait = "armin-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorps], Badge = "A RANK" }, // Nav_Commanders

        new CharacterProfile { Slug = "eren-yeager", NameKey = "Name_ErenYeager", TitleKey = "Title_ErenYeager", BioKey = "Bio_ErenYeager", Portrait = "eren1-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.Yeagerists], Badge = "A RANK",
            ExtraVersions = [
                new() { Portrait = "attack-titan-card.webp", LabelKey = "Version_AttackTitan" },
                new() { Portrait = "founding-titan-card.webp", LabelKey = "Version_FoundingTitan" }
            ] },
        new CharacterProfile { Slug = "Ymir", NameKey = "Name_Ymir", TitleKey = "Title_Ymir", BioKey = "Bio_Ymir", Portrait = "ymir-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorps], Badge = "A RANK",
            ExtraVersions = [
                new() { Portrait = "jaw-titan-card.webp", LabelKey = "Version_JawTitan" } 
            ] },

        new CharacterProfile { Slug = "historia-reiss", NameKey = "Name_HistoriaReiss", TitleKey = "Title_HistoriaReiss", BioKey = "Bio_HistoriaReiss", Portrait = "historia-portrait.webp", FocalPosition = "center 40%", Factions = [CharacterFaction.Main, CharacterFaction.RulingFamily], Badge = "B RANK" },
        
        // --- Titans ---
        new CharacterProfile { Slug = "Founding-Titan", NameKey = "Name_FoundingTitan", TitleKey = "Title_FoundingTitan", BioKey = "Bio_FoundingTitan", Portrait = "founding-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "204M" },
        new CharacterProfile { Slug = "Final-Founding-Titan", NameKey = "Name_FinalFoundingTitan", TitleKey = "Title_FinalFoundingTitan", BioKey = "Bio_FinalFoundingTitan", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "100M+" },
        new CharacterProfile { Slug = "Attack-Titan", NameKey = "Name_AttackTitan", TitleKey = "Title_AttackTitan", BioKey = "Bio_AttackTitan", Portrait = "attack-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "15M" },
        new CharacterProfile { Slug = "Armored-Titan", NameKey = "Name_ArmoredTitan", TitleKey = "Title_ArmoredTitan", BioKey = "Bio_ArmoredTitan", Portrait = "armored-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "15M" },
        new CharacterProfile { Slug = "Beast-Titan", NameKey = "Name_BeastTitan", TitleKey = "Title_BeastTitan", BioKey = "Bio_BeastTitan", Portrait = "beast-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "17M" },
        new CharacterProfile { Slug = "Cart-Titan", NameKey = "Name_CartTitan", TitleKey = "Title_CartTitan", BioKey = "Bio_CartTitan", Portrait = "cart-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "4M" },
        new CharacterProfile { Slug = "Colossal-Titan", NameKey = "Name_ColossalTitan", TitleKey = "Title_ColossalTitan", BioKey = "Bio_ColossalTitan", Portrait = "colossal-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "60M" },
        new CharacterProfile { Slug = "Female-Titan", NameKey = "Name_FemaleTitan", TitleKey = "Title_FemaleTitan", BioKey = "Bio_FemaleTitan", Portrait = "female-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "15M" },
        new CharacterProfile { Slug = "Jaw-Titan", NameKey = "Name_JawTitan", TitleKey = "Title_JawTitan", BioKey = "Bio_JawTitan", Portrait = "jaw-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "5M" },
        new CharacterProfile { Slug = "WarHammer-Titan", NameKey = "Name_WarhammerTitan", TitleKey = "Title_WarhammerTitan", BioKey = "Bio_WarhammerTitan", Portrait = "warhammer-titan-card.webp", Factions = [CharacterFaction.Titans], Badge = "15M" },
        new CharacterProfile { Slug = "Sonny-and-Bean", NameKey = "Name_SonnyAndBean", TitleKey = "Title_SonnyAndBean", BioKey = "Bio_SonnyAndBean", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "4M~7M" },
        new CharacterProfile { Slug = "Wall-Titans", NameKey = "Name_WallTitans", TitleKey = "Title_WallTitans", BioKey = "Bio_WallTitans", Portrait = "wall-titans-portrait.webp", Factions = [CharacterFaction.Titans], Badge = "50M" },

        new CharacterProfile { Slug = "Eld-Jinn", NameKey = "Name_EldJinn", TitleKey = "Title_EldJinn", BioKey = "Bio_EldJinn", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps], Badge = "A+ RANK" },
        new CharacterProfile { Slug = "Oluo-Bozado", NameKey = "Name_OluoBozado", TitleKey = "Title_OluoBozado", BioKey = "Bio_OluoBozado", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps], Badge = "A+ RANK" },
        new CharacterProfile { Slug = "Petra-Ral", NameKey = "Name_PetraRal", TitleKey = "Title_PetraRal", BioKey = "Bio_PetraRal", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps], Badge = "A+ RANK" },
        new CharacterProfile { Slug = "Gnther-Schultz", NameKey = "Name_GntherSchultz", TitleKey = "Title_GntherSchultz", BioKey = "Bio_GntherSchultz", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps], Badge = "A+ RANK" },


        // --- Garrison ---
        new CharacterProfile { Slug = "hannes", NameKey = "Name_Hannes", TitleKey = "Title_Hannes", BioKey = "Bio_Hannes", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Garrison], Badge = "B RANK" },
        new CharacterProfile { Slug = "Dot-Pixis", NameKey = "Name_DotPixis", TitleKey = "Title_DotPixis", BioKey = "Bio_DotPixis", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Garrison], Badge = "B RANK" },
        new CharacterProfile { Slug = "Rico-Brzenska", NameKey = "Name_RicoBrzenska", TitleKey = "Title_RicoBrzenska", BioKey = "Bio_RicoBrzenska", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Garrison], Badge = "B RANK" },
 

        // --- Military Police ---
        new CharacterProfile { Slug = "nile-dok", NameKey = "Name_NileDok", TitleKey = "Title_NileDok", BioKey = "Bio_NileDok", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },
        new CharacterProfile { Slug = "Marlowe-Freudenberg", NameKey = "Name_MarloweFreudenberg", TitleKey = "Title_MarloweFreudenberg", BioKey = "Bio_MarloweFreudenberg", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },
        new CharacterProfile { Slug = "Hitch-Dreyse", NameKey = "Name_HitchDreyse", TitleKey = "Title_HitchDreyse", BioKey = "Bio_HitchDreyse", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },
        new CharacterProfile { Slug = "Boris-Feulner", NameKey = "Name_BorisFeulner", TitleKey = "Title_BorisFeulner", BioKey = "Bio_BorisFeulner", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },

 
        // --- First Interior ---
        new CharacterProfile { Slug = "kenny-ackerman", NameKey = "Name_KennyAckerman", TitleKey = "Title_KennyAckerman", BioKey = "Bio_KennyAckerman", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.FirstInteriorSquad], Badge = "S RANK" },
        new CharacterProfile { Slug = "Traute-Carven", NameKey = "Name_TrauteCarven", TitleKey = "Title_TrauteCarven", BioKey = "Bio_TrauteCarven", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.FirstInteriorSquad], Badge = "S RANK" },
        new CharacterProfile { Slug = "Djel-Sanes", NameKey = "Name_DjelSanes", TitleKey = "Title_DjelSanes", BioKey = "Bio_DjelSanes", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.FirstInteriorSquad], Badge = "S RANK" },
 

        // --- Ruling Family - Nav_RulingFamily ---
        new CharacterProfile { Slug = "Fritz", NameKey = "Name_Fritz", TitleKey = "Title_Fritz", BioKey = "Bio_Fritz", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RulingFamily], Badge = "" },
        new CharacterProfile { Slug = "Rod-Reiss", NameKey = "Name_RodReiss", TitleKey = "Title_RodReiss", BioKey = "Bio_RodReiss", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RulingFamily], Badge = "" },
        new CharacterProfile { Slug = "Frieda-Reiss", NameKey = "Name_FriedaReiss", TitleKey = "Title_FriedaReiss", BioKey = "Bio_FriedaReiss", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RulingFamily], Badge = "" },
 

        // --- Royal Government — Nav_TheRoyalGovernment ---
        new CharacterProfile { Slug = "aurille", NameKey = "Name_Aurille", TitleKey = "Title_Aurille", BioKey = "Bio_Aurille", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },
        new CharacterProfile { Slug = "Deltoff", NameKey = "Name_Deltoff", TitleKey = "Title_Deltoff", BioKey = "Bio_Deltoff", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },
        new CharacterProfile { Slug = "Gerald", NameKey = "Name_Gerald", TitleKey = "Title_Gerald", BioKey = "Bio_Gerald", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },
        new CharacterProfile { Slug = "Roderich", NameKey = "Name_Roderich", TitleKey = "Title_Roderich", BioKey = "Bio_Roderich", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },


        // --- Civilians - Nav_Civilians ---
        new CharacterProfile { Slug = "grisha-yeager", NameKey = "Name_GrishaYeager", TitleKey = "Title_GrishaYeager", BioKey = "Bio_GrishaYeager", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },
        new CharacterProfile { Slug = "Carla-Yeager", NameKey = "Name_CarlaYeager", TitleKey = "Title_CarlaYeager", BioKey = "Bio_CarlaYeager", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },
        new CharacterProfile { Slug = "Minister-Nick", NameKey = "Name_MinisterNick", TitleKey = "Title_MinisterNick", BioKey = "Bio_MinisterNick", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },
        new CharacterProfile { Slug = "Dimo-Reeves", NameKey = "Name_DimoReeves", TitleKey = "Title_DimoReeves", BioKey = "Bio_DimoReeves", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },
        new CharacterProfile { Slug = "Kaya", NameKey = "Name_Kaya", TitleKey = "Title_Kaya", BioKey = "Bio_Kaya", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians], Badge = "" },

        // --- Marley - Nav_Marley ---
        new CharacterProfile { Slug = "Calvi-The-Marshal", NameKey = "Name_CalviTheMarshal", TitleKey = "Title_CalviTheMarshal", BioKey = "Bio_CalviTheMarshal", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "The Marshal" },
        new CharacterProfile { Slug = "Theo-Magath-Acommander", NameKey = "Name_TheoMagathAcommander", TitleKey = "Title_TheoMagathAcommander", BioKey = "Bio_TheoMagathAcommander", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Commander" },


        new CharacterProfile { Slug = "Reiner-Braun", NameKey = "Name_ReinerBraun", TitleKey = "Title_ReinerBraun", BioKey = "Bio_ReinerBraun", Portrait = "reiner-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit",
            ExtraVersions = [ new() { Portrait = "armored-titan-card.webp", LabelKey = "Version_ArmoredTitan" } ] 
        },

        new CharacterProfile { Slug = "Zeke-Yeager", NameKey = "Name_ZekeYeager", TitleKey = "Title_ZekeYeager", BioKey = "Bio_ZekeYeager", Portrait = "zeke-portrait.webp", Factions = [CharacterFaction.Marley], Badge =  "Warrior Unit",
            ExtraVersions = [ new() { Portrait = "beast-titan-card.webp", LabelKey = "Version_BeastTitan" } ] },
        new CharacterProfile { Slug = "Bertolt-Hoover", NameKey = "Name_BertoltHoover", TitleKey = "Title_BertoltHoover", BioKey = "Bio_BertoltHoover", Portrait = "bertolt-hoover-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit",
            ExtraVersions = [ new() { Portrait = "colossal-titan-card.webp", LabelKey = "Version_ColossalTitan" } ] },
        new CharacterProfile { Slug = "Annie-Leonhart", NameKey = "Name_AnnieLeonhart", TitleKey = "Title_AnnieLeonhart", BioKey = "Bio_AnnieLeonhart", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit",
            ExtraVersions = [ new() { Portrait = "female-titan-card.webp", LabelKey = "Version_FemaleTitan" } ] },    
        new CharacterProfile { Slug = "Marcel-Galliard", NameKey = "Name_MarcelGalliard", TitleKey = "Title_MarcelGalliard", BioKey = "Bio_MarcelGalliard", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Porco-Galliard", NameKey = "Name_PorcoGalliard", TitleKey = "Title_PorcoGalliard", BioKey = "Bio_PorcoGalliard", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Pieck-Finger", NameKey = "Name_PieckFinger", TitleKey = "Title_PieckFinger", BioKey = "Bio_PieckFinger", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Gabi-Braun", NameKey = "Name_GabiBraun", TitleKey = "Title_GabiBraun", BioKey = "Bio_GabiBraun", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Falco-Grice", NameKey = "Name_FalcoGrice", TitleKey = "Title_FalcoGrice", BioKey = "Bio_FalcoGrice", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
        new CharacterProfile { Slug = "Colt-Grice", NameKey = "Name_ColtGrice", TitleKey = "Title_ColtGrice", BioKey = "Bio_ColtGrice", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley], Badge = "Warrior Unit" },
 

        // --- Tybur Family ---
        new CharacterProfile { Slug = "willy-tybur", NameKey = "Name_WillyTybur", TitleKey = "Title_WillyTybur", BioKey = "Bio_WillyTybur", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.TyburFamily], Badge = "B RANK" },
        new CharacterProfile { Slug = "lara-tybur", NameKey = "Name_LaraTybur", TitleKey = "Title_LaraTybur", BioKey = "Bio_LaraTybur", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.TyburFamily], Badge = "B RANK" },


        // --- Yeagerists ---
        new CharacterProfile { Slug = "floch-forster", NameKey = "Name_FlochForster", TitleKey = "Title_FlochForster", BioKey = "Bio_FlochForster", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Yeagerists], Badge = "B RANK" },
        new CharacterProfile { Slug = "Holger", NameKey = "Name_Holger", TitleKey = "Title_Holger", BioKey = "Bio_Holger", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Yeagerists], Badge = "B RANK" },
        new CharacterProfile { Slug = "Wim", NameKey = "Name_Wim", TitleKey = "Title_Wim", BioKey = "Bio_Wim", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Yeagerists], Badge = "B RANK" },
    
        // --- Nav_Other ---
        new CharacterProfile { Slug = "TheAntiMarleyanVolunteers", NameKey = "Name_Theantimarleyanvolunteers", TitleKey = "Title_Theantimarleyanvolunteers", BioKey = "Bio_Theantimarleyanvolunteers", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Other], Badge = "B RANK" },
        new CharacterProfile { Slug = "Underground", NameKey = "Name_Underground", TitleKey = "Title_Underground", BioKey = "Bio_Underground", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Other], Badge = "B RANK" },


    ];

    public static CharacterProfile? FindBySlug(string slug) => Profiles.FirstOrDefault(c => c.Slug == slug);

    public static readonly List<CharacterFaction> FactionOrder = [
    
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
    ];

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