using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Models;

namespace MonosortMiniApp.Domain.Commons.Mapping;

public class AdditiveModelMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CartItemRequest, CartItemModel>()
            .Map(dest => dest.SugarCount, src => src.Additives.SugarCount)
            .Map(dest => dest.MilkId, src => src.Additives.MilkId)
            .Map(dest => dest.SiropId, src => src.Additives.SiropId)
            .Map(dest => dest.ExtraShot, src => src.Additives.ExtraShot)
            .Map(dest => dest.Sprinkling, src => src.Additives.Sprinkling);
    }
}
