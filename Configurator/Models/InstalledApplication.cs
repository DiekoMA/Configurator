using Avalonia.Media.Imaging;

namespace Configurator.Models;

public record InstalledApplication
{
    public required string DisplayName { get; set; }
    public Bitmap? DisplayIcon { get; set; }
    public required string InstallLocation { get; set; }
    public required string Publisher { get; set; }
}
