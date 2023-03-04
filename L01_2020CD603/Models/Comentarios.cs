﻿using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace L01_2020CD603.Models
{
    public class Comentarios
    {
        [Key]
        public int cometarioId { get; set; }
        public int publicacionId { get; set; }
	    public string? comentario { get; set; }
	    public int usuarioId { get; set; }
    }
}
