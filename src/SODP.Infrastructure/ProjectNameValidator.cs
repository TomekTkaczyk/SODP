using SODP.Model.Extensions;
using System.Text.RegularExpressions;

namespace SODP.Infrastructure
{
    public class ProjectNameValidator
    {

        public bool Validate(string projectName)
        {
            bool result = true;
            var localization = Path.GetFileName(projectName);
            var sign = localization.GetUntilOrEmpty("_");
            var regex1 = new Regex(@"^([1-9]{1})([0-9]{3})$");
            var regex2 = new Regex(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$");
            result = result && 
                !String.IsNullOrEmpty(sign) && 
                regex1.Match(sign.Substring(0, 4)).Success &&
                regex2.Match(localization[(sign.Length+1)..]).Success;

            return result;
        }
    }
}
