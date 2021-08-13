using System.Collections.Generic;
using System.Text.RegularExpressions;
using SODP.Model.Extensions;
using SODP.Shared.Enums;

namespace SODP.Model
{
    public class Project : BaseEntity
    {
        public Project() { }

        public Project(string number, string stageSign, string title)
        {
            RequiredPropertiesInit(number, stageSign, title);
            EmptyPropertiesInit();
        }

        public Project(string foldername)
        {
            var sign = foldername.GetUntilOrEmpty("_");
            RequiredPropertiesInit(
                sign.Substring(0, 4),
                sign[4..],
                foldername[(sign.Length + 1)..]);
            EmptyPropertiesInit();

            if (string.IsNullOrEmpty(Stage.Sign))
            {
                sign = foldername.GetLastUntilOrEmpty("_");
                Stage.Sign = sign;
                var titleLength = Title.Length - Stage.Sign.Length - 1;
                Title = Title[0..titleLength];
            }
        }

        public string Number { get; set; }
        public int StageId { get; set; }
        public virtual Stage Stage { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public string TitleStudy { get; set; }
        public string Investment { get; set; }
        public string Address { get; set; }
        public string Investor { get; set; }
        public string Location { get; set; }
        public ProjectStatus Status { get; set; }
        public ICollection<ProjectBranch> Branches { get; set; }

        public virtual string Symbol 
        {
            get 
            {
                return Number.Trim() + Stage.Sign.Trim();
            }
        }

        public override string ToString()
        {
            return Number.Trim() + Stage.Sign.Trim() + "_" + Title.Trim();
        }

        public void Normalize()
        {
            Regex regex = new Regex("[ ]", RegexOptions.None);
            Title = regex.Replace(Title, "_");
            regex = new Regex("[_]{2,}", RegexOptions.None);
            Title = regex.Replace(Title, "_");
            regex = new Regex("(_+)$", RegexOptions.None);
            Title = regex.Replace(Title, "");
            Title = Title.CapitalizeFirstLetter();
        }

        private void EmptyPropertiesInit()
        {
            Address = "";
            Investment = "";
            Investor = "";
            TitleStudy = "";
            Location = "";
        }

        private void RequiredPropertiesInit(string number, string stageSign, string title)
        {
            Number = number;
            Stage = new Stage() { Sign = stageSign };
            Title = title;
        }
    }
}
