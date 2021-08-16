using Microsoft.AspNetCore.Mvc.Rendering;
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
                Content = license.Content
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

        public static LicenseVM ToViewModel(this LicenseWithBranchesDTO licenseDTO)
        {
            var license = new LicenseVM
            {
                DesignerId = licenseDTO.Designer.Id,
                Id = licenseDTO.Id,
                Content = licenseDTO.Content,
                ApplyBranches = licenseDTO.Branches.Select(x => new SelectListItem {Value=x.Id.ToString(), Text=x.ToString()}).ToList()
            };

            return license;
        }

        public static StringContent ToHttpContent(this NewLicenseVM license)
        {
            var licenseDTO = new LicenseDTO
            {
                Content = license.Content
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
    }
}
