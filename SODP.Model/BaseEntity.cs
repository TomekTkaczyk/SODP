﻿using System;

namespace SODP.Model;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreateTimeStamp { get; set; }
    
    public DateTime ModifyTimeStamp { get; set; }

    public BaseEntity()
    {
        CreateTimeStamp = DateTime.UtcNow;
        ModifyTimeStamp = CreateTimeStamp;
    }
}
