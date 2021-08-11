using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Infrastructure
{
    public class SelectListItemComparer : IEqualityComparer<SelectListItem>
    {
        public bool Equals([AllowNull] SelectListItem x, [AllowNull] SelectListItem y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode([DisallowNull] SelectListItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
