﻿using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class License : BaseEntity
{
    public int DesignerId { get; set; }
    public virtual Designer Designer { get; set; }
    public string Content { get; set; }
    public virtual ICollection<BranchLicense> Branches { get; set; }
}