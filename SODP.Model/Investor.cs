using SODP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Model
{
    public class Investor : BaseEntity, IActiveStatus
    {
        public string Name { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
