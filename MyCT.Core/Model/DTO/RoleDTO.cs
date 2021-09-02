﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Core.Model.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}