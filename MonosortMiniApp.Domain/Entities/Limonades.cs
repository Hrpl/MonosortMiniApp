using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table(EntityInformation.Dictionary.Limonades, Schema = EntityInformation.Dictionary.Scheme)]
public class Limonades : BaseDictionaryEntity
{
}
