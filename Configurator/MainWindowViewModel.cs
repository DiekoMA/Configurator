using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WGetNET;

namespace Configurator;

public class MainWindowViewModel : ObservableObject
{
    private List<WinGetPackage> _packages;

    public List<WinGetPackage> Packages
    {
        get => _packages;
        set
        {
            _packages = value;
            OnPropertyChanged();
        }
    }
    public MainWindowViewModel()
    {
        WinGet wget = new WinGet();
        WinGetPackageManager pm = new WinGetPackageManager();
        if (wget.IsInstalled)
        {
            Packages = pm.GetInstalledPackages();
            foreach (var package in Packages)
            {
                
            }
        }
    }

    public void GetInstalledApplications()
    {
        
    }
}