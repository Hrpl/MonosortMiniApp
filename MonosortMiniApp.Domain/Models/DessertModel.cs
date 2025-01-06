using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Models;

public class DessertModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Photo { get; set; }
    public bool IsExistence { get; set; }
}
