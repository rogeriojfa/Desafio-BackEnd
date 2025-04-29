using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoRent.Application.DTOs;

namespace MotoRent.Application.Interfaces.Service
{

        public interface IDeliverymanService
        {
            Task<DeliverymanResponseDto> CreateAsync(CreateDeliverymanDto dto);
            Task<IEnumerable<DeliverymanResponseDto>> GetAllAsync();
            Task<DeliverymanResponseDto> GetByIdAsync(Guid id);
            Task UpdateAsync(Guid id, UpdateDeliverymanDto dto);
        }
    }

