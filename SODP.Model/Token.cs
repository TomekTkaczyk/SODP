﻿namespace SODP.Model;

public class Token : BaseEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Access { get; set; }
    public string RefreshTokenKey { get; set; }
    public string Refresh { get; set; }
}
