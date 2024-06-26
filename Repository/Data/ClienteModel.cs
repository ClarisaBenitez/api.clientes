﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository.Data
{
    public class ClienteModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Banco ID")]
        public int id_banco { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string documento { get; set; }

        public string direccion { get; set; }

        public string mail { get; set; }

        public string celular { get; set; }

        public string estado { get; set; }

    }
}
