using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Auto.Data.Entities;

namespace Auto.Website.Models
{
    public class AutoOwnerDto
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        
        [Required]
        [DisplayName("Phone number")]
        public string Number { get; set; }
        
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        private string autoId;
        private static string NormalizeAutoId (string reg)
        {
            return reg == null ? reg : Regex.Replace(reg.ToUpperInvariant(), "[^A-Z0-9]", "");
        }
        
        [Required]
        [DisplayName("Registration code")]
        public string AutoId {
            get => NormalizeAutoId(autoId);
            set => autoId = value;
        }
    }
}