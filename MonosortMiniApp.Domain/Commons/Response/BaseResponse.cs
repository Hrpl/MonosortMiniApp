using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonosortMiniApp.Domain.Commons.Response;

public class BaseResponse<T>
{
    public BaseResponse() { }

    public BaseResponse(T data, string description = "")
    {
        Data = data;
        Description = description;
    }


    /// <summary>
    /// Данные
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Описание ответа
    /// </summary>
    public string? Description { get; set; }
}
