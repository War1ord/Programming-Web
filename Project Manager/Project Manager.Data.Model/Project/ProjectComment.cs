﻿using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Project
{
	public class ProjectComment : Base.ProjectBase
	{
		[Required]
		public string Text { get; set; }
	}
}