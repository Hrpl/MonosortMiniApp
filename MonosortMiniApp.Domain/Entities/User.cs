using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table("User", Schema = EntityInformation.Dictionary.Scheme)]
public class User : BaseEntity
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public bool IsConfirmed { get; set; }

}
