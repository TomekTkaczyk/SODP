﻿using SODP.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.ViewModels
{
    public class DesignerLicencesVM
    {
        public int DesignerId { get; set; }

        public IList<LicenceDTO> Licence { get; set; }
    }
}
