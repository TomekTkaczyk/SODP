using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public class DesignerDTO : BaseDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool ActiveStatus { get; set; } = true;
        public override string ToString()
        {
            return $"{Title.Trim()} {Firstname.Trim()} {Lastname.Trim()}";
        }
    }
}
