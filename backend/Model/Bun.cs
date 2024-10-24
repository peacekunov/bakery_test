using backend.Enums;
using backend.Repository;

namespace backend.Model;

public class Bun : IEntity
{
    public Guid Key { get; set; }

    public BunType Type { get; set; }

    public float InitialPrice { get; set; }

    public DateTime BakedTime { get; set; }

    public DateTime StaleTime { get; set; }

    public DateTime DiscardTime { get; set; }

    public override string ToString()
    {
        return $"Key: {Key}, BunType: {Type}, InitialPrice: {InitialPrice}, BakedTime: {BakedTime}, StaleTime: {StaleTime}, DiscardTime: {DiscardTime}";
    }
}