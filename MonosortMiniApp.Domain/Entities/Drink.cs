using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

public class Drink : BaseEntity
{
    public int TypeDrinkId { get; set; }
    public string Name { get; set; }
    public bool IsExistence { get; set; }
    public string Photo {  get; set; }
}
