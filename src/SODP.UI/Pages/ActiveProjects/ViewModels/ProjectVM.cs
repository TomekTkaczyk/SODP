﻿namespace SODP.UI.Pages.ActiveProjects.ViewModels;

public class ProjectVM
{
	public int Id { get; set; }

	public string Number { get; set; }

	public StageVM Stage { get; set; }

	public string Name { get; set; }
}