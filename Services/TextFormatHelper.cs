using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace AOT.Services;

/// <summary>
/// Small display-formatting helpers for already-localized text — formatting, not translation,
/// so it doesn't belong in AppLocalizationService.
/// </summary>
public static class TextFormatHelper
{
    // matches " aka " in any case, e.g. "Historia Reiss aka Christa Lenz" or "...雷斯 AKA 克里斯塔..."
    private static readonly Regex AkaPattern = new(@"\s+aka\s+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Shrinks and force-lowercases an "aka &lt;alias&gt;" suffix so long alt-name strings
    /// don't blow out card/title layouts at full font size.
    /// </summary>
    public static MarkupString FormatNameWithAka(string name)
    {
        var match = AkaPattern.Match(name);
        if (!match.Success)
            return new MarkupString(System.Net.WebUtility.HtmlEncode(name));

        var main = System.Net.WebUtility.HtmlEncode(name[..match.Index]);
        var alias = System.Net.WebUtility.HtmlEncode(name[(match.Index + match.Length)..]);
        return new MarkupString($"{main} <span class=\"name-aka\">aka {alias}</span>");
    }
}