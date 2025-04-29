using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoRent.Domain.Enums;

namespace MotoRent.Application.DTOs
{
    public class UpdateDeliverymanDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cnh { get; set; }
        public CnhType CnhType { get; set; }
        public string CnhImagePath { get; set; }
    }
}
