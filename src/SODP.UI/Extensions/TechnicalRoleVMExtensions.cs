using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions
{
    public static class TechnicalRoleVMExtensions
    {
        public static StringContent ToHttpContent(this TechnicalRoleVM role)
        {
            return new StringContent(
                                  JsonSerializer.Serialize(new TechnicalRoleDTO
                                  {
                                      ProjectId = role.ProjectId,
                                      BranchId = role.BranchId,
                                      Role = (TechnicalRole)Enum.Parse(typeof(TechnicalRole), role.RoleId),
                                      LicenseId = role.LicenseId
                                  }),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }
    }
}
