using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Templates;

public static class EmailTemplates
{
    public const string RegistrationEmailTemplate = @"
<body >
    
    <div >
        <h3 >Благодарим вас за регистрацию в нашем приложении.</h3>
        <br>
        <p >Для завершения процесса регистрации, пожалуйста, перейдите по <span>
            <a href=""https://monosortcoffee.ru/api/user/confirm?email=@email"">ССЫЛКЕ</a>
        </span> для подтверждения email.</p>
        
    </div>
</body>";
}
