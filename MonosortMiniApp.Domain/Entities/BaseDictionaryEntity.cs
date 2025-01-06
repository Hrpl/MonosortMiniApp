using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

public class BaseDictionaryEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public bool IsExistence { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }
}
