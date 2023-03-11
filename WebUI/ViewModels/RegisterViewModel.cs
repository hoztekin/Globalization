using Elfie.Serialization;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using WebUI.Models;

namespace WebUI.ViewModels
{
	public class RegisterViewModel
	{
		public int Id { get; set; }

		[Display(Name = "Email"), Required(ErrorMessage = "Required")]
		public string Email { get; set; }


	}
}
