using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoRent.Domain.Enums;

namespace MotoRent.Application.DTOs
{
    public class UpdateMotoDto
    {
        public string? Model { get; set; }
        public int? Year { get; set; }
        public MotoStatus? Status { get; set; }
    }
}
