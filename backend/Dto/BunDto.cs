using System.Text.Json.Serialization;
using backend.Enums;

namespace backend.Dto;

public class BunDto
{
    public Guid Key { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BunType Type { get; set; }

    public float InitialPrice { get; set; }

    public float CurrentPrice { get; set; }

    public float NextPrice { get; set; }

    public bool WillBeThrownOut { get; set; }

    public TimeSpan TimeUntilPriceChange { get; set; }

    public override string ToString()
    {
        return $"Key: {Key}, BunType: {Type}, InitialPrice: {InitialPrice}, CurrentPrice: {CurrentPrice}, NextPrice: {NextPrice}, TimeUntilPriceChange: {TimeUntilPriceChange}";
    }
}