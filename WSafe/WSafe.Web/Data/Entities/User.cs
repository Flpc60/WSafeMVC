﻿using System.ComponentModel.DataAnnotations;

namespace WSafe.Domain.Data.Entities
{
    public class User
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Usuario")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Correo")]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Contraseña")]
        [MaxLength(100)]
        public string Password { get; set; }
        public int RoleID { get; set; }
    }
}