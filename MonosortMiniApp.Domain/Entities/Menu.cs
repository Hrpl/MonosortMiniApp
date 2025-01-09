using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table("Menu", Schema = EntityInformation.Dictionary.Scheme)]
public class Menu : BaseEntity
{
    public string Name { get; set; }
}
