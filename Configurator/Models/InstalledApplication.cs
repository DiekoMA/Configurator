using Avalonia.Media.Imaging;

namespace Configurator.Models;

public class InstalledApplication
{
    public string DisplayName { get; set; }
    public Bitmap DisplayIcon { get; set; }
    public string InstalLocation { get; set; }
    public string Publisher { get; set; }
}