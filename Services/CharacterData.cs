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
    // --- Main / Survey Corps ---
    new CharacterProfile { Slug = "levi-ackerman", NameKey = "C1Name", TitleKey = "C1Title", BioKey = "C1Bio", Portrait = "levi-portrait.webp", Factions = [CharacterFaction.SurveyCorpsSpecialOps, CharacterFaction.SurveyCorps], Badge = "S RANK" },
    new CharacterProfile { Slug = "mikasa-ackerman", NameKey = "C2Name", TitleKey = "C2Title", BioKey = "C2Bio", Portrait = "mikasa-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorps], Badge = "A+ RANK" },
    new CharacterProfile { Slug = "hange-zoe", NameKey = "C4Name", TitleKey = "C4Title", BioKey = "C4Bio", Portrait = "hange-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "A RANK" },
    new CharacterProfile { Slug = "erwin-smith", NameKey = "C5Name", TitleKey = "C5Title", BioKey = "C5Bio", Portrait = "erwin-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "S RANK" },
    new CharacterProfile { Slug = "jean-kirstein", NameKey = "C6Name", TitleKey = "C6Title", BioKey = "C6Bio", Portrait = "jean-portrait.webp", Factions = [CharacterFaction.SurveyCorps], Badge = "A RANK" },

    // --- Main + Titans ---
    new CharacterProfile { Slug = "armin-arlert", NameKey = "C3Name", TitleKey = "C3Title", BioKey = "C3Bio", Portrait = "armin-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.SurveyCorps, CharacterFaction.Titans], Badge = "60m" },
    new CharacterProfile { Slug = "eren-yeager", NameKey = "Nav_Eren", TitleKey = "CharEren_Title", BioKey = "CharEren_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.Yeagerists, CharacterFaction.Titans], Badge = "15m" },
    new CharacterProfile { Slug = "historia-reiss", NameKey = "Nav_Historia", TitleKey = "CharHistoria_Title", BioKey = "CharHistoria_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Main, CharacterFaction.RulingFamily], Badge = "" },

    // --- Garrison / Military Police / First Interior ---
    new CharacterProfile { Slug = "hannes", NameKey = "Nav_Hannes", TitleKey = "CharGarrison_Title", BioKey = "CharGarrison_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Garrison], Badge = "B RANK" },
    new CharacterProfile { Slug = "nile-dok", NameKey = "Nav_NileDok", TitleKey = "CharMP_Title", BioKey = "CharMP_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.MilitaryPolice], Badge = "B RANK" },
    new CharacterProfile { Slug = "kenny-ackerman", NameKey = "Nav_KennyAckerman", TitleKey = "CharInterior_Title", BioKey = "CharInterior_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.FirstInteriorSquad], Badge = "S RANK" },

    // --- Ruling Family / Royal Government — no badge ---
    new CharacterProfile { Slug = "rod-reiss", NameKey = "Nav_RodReiss", TitleKey = "CharRoyal_Title", BioKey = "CharRoyal_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RulingFamily], Badge = "" },
    new CharacterProfile { Slug = "aurille", NameKey = "Nav_Aurille", TitleKey = "CharGov_Title", BioKey = "CharGov_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.RoyalGovernment], Badge = "" },

    // --- Civilians + Titans ---
    new CharacterProfile { Slug = "grisha-yeager", NameKey = "Nav_GrishaYeager", TitleKey = "CharCivilian_Title", BioKey = "CharCivilian_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Civilians, CharacterFaction.Titans], Badge = "15m" },

    // --- Marley + Titans ---
    new CharacterProfile { Slug = "reiner-braun", NameKey = "Nav_ReinerBraun", TitleKey = "CharMarley_Title", BioKey = "CharMarley_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Marley, CharacterFaction.Titans], Badge = "15m" },

    // --- Tybur Family / Yeagerists ---
    new CharacterProfile { Slug = "willy-tybur", NameKey = "Nav_WillyTybur", TitleKey = "CharTybur_Title", BioKey = "CharTybur_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.TyburFamily], Badge = "B RANK" },
    new CharacterProfile { Slug = "floch-forster", NameKey = "Nav_FlochForster", TitleKey = "CharYeagerist_Title", BioKey = "CharYeagerist_Bio", Portrait = "placeholder-portrait.webp", Factions = [CharacterFaction.Yeagerists], Badge = "B RANK" },
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