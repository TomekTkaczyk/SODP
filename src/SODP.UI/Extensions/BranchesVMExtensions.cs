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
    public static class BranchesVMExtensions
    {
        public static StringContent ToHttpContent(this BranchesVM branches)
        {
            var branch = new BranchDTO
            {
                Id = branches.BranchId,
            };
            return new StringContent(
                                  JsonSerializer.Serialize(branch),
                                  Encoding.UTF8,
                                  "application/json"
                              );
        }
    }
}
