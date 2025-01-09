using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table(EntityInformation.Dictionary.Desserts, Schema = EntityInformation.Dictionary.Scheme)]
public class Dessert : BaseEntity
{
    public int MenuId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Photo { get; set; }
    public bool IsExistence { get; set; }
}
