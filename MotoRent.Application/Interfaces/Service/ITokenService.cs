using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoRent.Domain.Entities;

namespace MotoRent.Application.Interfaces.Service
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}
