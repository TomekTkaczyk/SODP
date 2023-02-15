
using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class PartDTO  : NewPartDTO
    {
        public int Order { get; set; }

        public bool ActiveStatus { get; set; }
    }
}
