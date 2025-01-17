using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form.Domain.Templates;

public static class EmailTemplates
{
    public static string Send = @"
    <body>
        <p>Имя: @VisitorName </p>
        <p>Пара: @PartnerName </p>
        <p>Дети: @ChildrenName </p>
        <p>Номер телефона: @PhoneNumber </p>
        <p>@drink</p>
        <p>@reg</p>
     </body>
    ";
}
