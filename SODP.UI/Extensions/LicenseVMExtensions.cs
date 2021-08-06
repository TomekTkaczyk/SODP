using SODP.Shared.DTO;
using SODP.UI.Pages.Designers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Extensions
{
    public static class LicenseVMExtensions
    {
        public static StringContent ToHttpContent(this LicenseVM license)
        {
            var licenseDTO = new LicenseDTO
            {
                Id = license.Id,
                Contents = license.Contents
            };
            licenseDTO.Designer = new DesignerDTO
            {
                Id = license.DesignerId
            };

            return new StringContent(
                                  JsonSerializer.Serialize(licenseDTO),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }

        public static LicenseVM ToViewModel(this LicenseDTO license)
        {
            return new LicenseVM
            {
                Id = license.Id,
                Contents = license.Contents,
                DesignerId = license.Designer.Id
            };
        }
    }
}
