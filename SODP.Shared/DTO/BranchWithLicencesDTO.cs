using System.Collections.Generic;

namespace SODP.Shared.DTO
{
    public class BranchWithLicencesDTO : BranchDTO
    {
        public IList<LicenceDTO> Licences { get; set; }
    }
}
