using System.Text.RegularExpressions;
using NotesWithTags.Services.App.Int;

namespace NotesWithTags.Services.App;

public class TagService : ITagService
{
    public IEnumerable<string> AddTags(string noteText)
    {
        var phoneTag = CheckForPhoneTag(noteText);
        var emailTag = CheckForEmailTag(noteText);

        var result = new List<string>();
        if (phoneTag) result.Add("PHONE");
        if (emailTag) result.Add("EMAIL");

        return result;
    }

    private bool CheckForPhoneTag(string noteText)
    {
        var internationalPattern =
            "\\+(9[976]\\d|8[987530]\\d|6[987]\\d|5[90]\\d|42\\d|3[875]\\d|\n2[98654321]\\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|\n4[987654310]|3[9643210]|2[70]|7|1)\\d{1,14}$";
        var internationNumber = Regex.Match(noteText, internationalPattern, RegexOptions.IgnoreCase).Success;

        var nationalPattern = "[0-9]{9}";
        var nationalNumber = Regex.Match(noteText, nationalPattern, RegexOptions.IgnoreCase).Success;

        return internationNumber || nationalNumber;
    }

    private bool CheckForEmailTag(string noteText)
    {
        var pattern =
            "[-A-Za-z0-9!#$%&'*+/=?^_`{|}~]+(?:\\.[-A-Za-z0-9!#$%&'*+/=?^_`{|}~]+)*@(?:[A-Za-z0-9](?:[-A-Za-z0-9]*[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[-A-Za-z0-9]*[A-Za-z0-9])?";
        return Regex.Match(noteText, pattern, RegexOptions.IgnoreCase).Success;
    }
}