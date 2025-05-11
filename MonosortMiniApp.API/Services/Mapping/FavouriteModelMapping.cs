using Mapster;
using MonosortMiniApp.Domain.Commons.Request;
using MonosortMiniApp.Domain.Models;

namespace MonosortMiniApp.API.Services.Mapping;

public class FavouriteModelMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateFavouriteItemRequest, FavouriteItemModel>()
            .Map(dest => dest.SugarCount, src => src.Additives.SugarCount)
            .Map(dest => dest.MilkId, src => src.Additives.MilkId)
            .Map(dest => dest.SiropId, src => src.Additives.SiropId)
            .Map(dest => dest.ExtraShot, src => src.Additives.ExtraShot)
            .Map(dest => dest.Sprinkling, src => src.Additives.Sprinkling);
    }
}
