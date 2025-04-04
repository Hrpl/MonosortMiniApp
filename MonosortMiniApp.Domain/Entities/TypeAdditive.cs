﻿using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table("TypeAdditive", Schema = EntityInformation.Dictionary.Scheme)]
public class TypeAdditive : BaseEntity
{
    public string Name { get; set; }
    public string Photo { get; set; }

}
