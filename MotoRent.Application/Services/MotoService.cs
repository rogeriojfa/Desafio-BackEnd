using MotoRent.Application.DTOs;
using MotoRent.Application.Interfaces;
using MotoRent.Domain.Entities;

namespace MotoRent.Application.Services
{
    public class MotoService(IMotoRepository motoRepository) : IMotoService
    {
        private readonly IMotoRepository _motoRepository = motoRepository;

        public IMotoRepository MotoRepository => _motoRepository;

        public async Task<Moto?> GetByLicensePlateAsync(string licensePlate)
        {
            return await MotoRepository.GetByLicensePlateAsync(licensePlate);
        }

        public async Task<IEnumerable<Moto>> GetAllAsync()
        {
            return await MotoRepository.GetAllAsync();
        }

        public async Task AddAsync(Moto moto)
        {
            await MotoRepository.AddAsync(moto);
        }

        public async Task UpdatePlateAsync(Guid id, string newPlate)
        {
            await MotoRepository.UpdatePlateAsync(id, newPlate);
        }

        public async Task DeleteAsync(Guid id)
        {
            var hasRental = await MotoRepository.HasAnyRentalAsync(id);
            if (hasRental)
            {
                throw new InvalidOperationException("Moto não pode ser removida porque está alugada.");
            }

            await MotoRepository.DeleteAsync(id);
        }
    }
}
