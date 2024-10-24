using backend.Enums;

namespace backend.Settings;

public class BunConfiguration
{
    public Dictionary<BunType, BunSettings> BunSettings { get; set; }
}