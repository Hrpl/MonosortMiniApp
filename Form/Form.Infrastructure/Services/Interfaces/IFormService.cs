using Form.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form.Infrastructure.Services.Interfaces;

public interface IFormService
{
    public Task CreateEntity(FormEntity entity);
    public Task<List<FormEntity>> GetAll();
}
