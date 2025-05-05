using MonosortMiniApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Response;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public bool IsExistence { get; set; }
    public List<VolumePriceModel> VolumePriceModels {  get; set; }
}
