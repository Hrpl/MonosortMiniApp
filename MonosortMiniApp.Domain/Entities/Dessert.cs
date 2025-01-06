using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table(EntityInformation.Dictionary.Desserts, Schema = EntityInformation.Dictionary.Scheme)]
public class Dessert : BaseDictionaryEntity
{
    public string Description { get; set; }
}
