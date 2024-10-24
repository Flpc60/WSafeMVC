﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WSafe.Domain.Models
{
    public class RoleUserVM
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
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        [Display(Name = "Perfiles usuario")]
        public int RoleID { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatotio")]
        public int OrganizationID { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Consultoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una consultoría !!")]
        public int ClientID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }
    }
}