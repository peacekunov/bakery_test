using backend.Enums;

namespace backend.Settings;

public class BunSettings
{
    public BunType Type { get; set; }
    public float Price { get; set; }
    public int FreshHours { get; set; }
    public int ShelfLifeHours { get; set; }
    public string PriceDropStrategy { get; set; } = "Default";
}