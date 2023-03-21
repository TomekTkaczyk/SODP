using System;
using System.Collections.Generic;
using System.Text;

namespace SODP.Shared.DTO
{
    public abstract class OrderedBaseDTO : BaseDTO
    {
        public int Order { get; set; }
    }
}
