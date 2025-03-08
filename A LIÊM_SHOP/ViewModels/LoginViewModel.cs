﻿using System.ComponentModel.DataAnnotations;

namespace A_LIÊM_SHOP.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Please enter your password")]
		[DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
        public string Password { get; set; }
	}
}
