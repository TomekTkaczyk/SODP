﻿using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.UI.Pages.Shared.ViewModels;

public class PageCollection<TDto> where TDto : BaseDTO
{
	public ICollection<TDto> Collection { get; set; }
	public PageInfo PageInfo { get; set; }
}