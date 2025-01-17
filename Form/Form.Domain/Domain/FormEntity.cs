using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form.Domain.Domain;

public class FormEntity
{
    public int Id { get; set; }
    public string VisitorName { get; set; }
    public string PhoneNumber { get; set; }
    public string PartnerName { get; set; }
    public string ChildrenName { get; set; }
    public bool BeVodka { get; set; }
    public bool BeWine { get; set; }
    public bool BeCongac { get; set; }
    public bool BeChampagne { get; set; }
    public bool DontDrink { get; set; }
    public bool WillTheRegistration { get; set; }
}
