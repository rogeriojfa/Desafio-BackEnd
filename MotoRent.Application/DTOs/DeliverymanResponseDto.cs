using MotoRent.Domain.Enums;

namespace MotoRent.Application.DTOs
{
    public class DeliverymanResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cnh { get; set; }
        public CnhType CnhType { get; set; }
        public string CnhImagePath { get; set; }
    }
}
