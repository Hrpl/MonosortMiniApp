﻿using MonosortMiniApp.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Entities;

[Table(EntityInformation.Dictionary.Volume, Schema = EntityInformation.Dictionary.Scheme)]
public class Volume : BaseEntity
{
    public string Name { get; set; }
    public string Size { get; set; }
}
