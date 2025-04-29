using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoRent.Domain.Enums;

namespace MotoRent.Application.DTOs
{
    public class MotoResponseDto
    {
        public Guid Id { get; set; }
        public string Plate { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public MotoStatus Status { get; set; }
    }
}
