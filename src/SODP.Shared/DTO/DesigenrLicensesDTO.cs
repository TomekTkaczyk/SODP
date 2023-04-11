using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Shared.DTO;

public record DesigenrLicensesDTO(DesignerDTO designer, ICollection<LicenseDTO> licenses)
{
}
