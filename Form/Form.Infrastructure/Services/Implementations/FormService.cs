using Form.Domain.Domain;
using Form.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form.Infrastructure.Services.Implementations;

public class FormService : IFormService
{
    public Task CreateEntity(FormEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<FormEntity>> GetAll()
    {
        throw new NotImplementedException();
    }
}
