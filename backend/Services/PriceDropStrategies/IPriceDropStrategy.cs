using backend.Dto;
using backend.Model;

namespace backend.Services.PriceDropStrategies;

public interface IPriceDropStrategy
{
    BunDto GetUpdatedBun(Bun bun);
}