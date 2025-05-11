using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Response;

public class ProductItemResponse
{
    public int Id { get; set; }
    public string? Photo { get; set; }
    public string Drink { get; set; }
    public string Volume { get; set; }
    public string Price { get; set; }
    public int SugarCount { get; set; }
    public bool ExtraShot { get; set; }
    public string SiropName { get; set; }
    public string MilkName { get; set; }
    public string Sprinkling { get; set; }
}
