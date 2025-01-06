using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MonosortMiniApp.Domain.Constant;

namespace MonosortMiniApp.Domain.Entities;

[Table(EntityInformation.Dictionary.Sirup, Schema = EntityInformation.Dictionary.Scheme)]
public class Sirup : BaseDictionaryEntity
{
}
