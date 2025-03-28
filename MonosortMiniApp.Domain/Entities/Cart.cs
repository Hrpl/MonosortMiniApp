using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table("Cart", Schema = EntityInformation.Dictionary.Scheme)]
public class Cart : BaseEntity
{
    public int UserId { get; set; }
}
