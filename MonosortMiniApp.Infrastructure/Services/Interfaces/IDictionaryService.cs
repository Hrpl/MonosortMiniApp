using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Infrastructure.Services.Interfaces;

public interface IDictionaryService<T>
{
    public Task<List<T>> GetAllAsync(string tableName);
    public Task<T> Get(int id, string tableName);
}
