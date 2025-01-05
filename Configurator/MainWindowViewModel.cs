using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using Configurator.Models;
using Microsoft.Win32;
using WGetNET;

namespace Configurator;

public class MainWindowViewModel : ObservableObject
{
    private InstalledApplication _selectedApplication;

    public InstalledApplication SelectedApplication
    {
        get => _selectedApplication;
        set
        {
            _selectedApplication = value;
            SetProperty(ref _selectedApplication, value);   
            OnPropertyChanged();
        }
    }
    private List<InstalledApplication> _packages;

    public List<InstalledApplication> Packages
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
            Packages = GetInstalledApplications();
        }
    }

    public static List<InstalledApplication> GetInstalledApplications()
    {
        var installedApplications = new List<InstalledApplication>();

        string[] registryKeys =
        {
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
            @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
        };

        foreach (var keyPath in registryKeys)
        {
            using RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath);
            if (key != null)
            {
                foreach (var subKeys in key.GetSubKeyNames())
                {
                    using var subKey = key.OpenSubKey(subKeys);
                    if (subKey == null) continue;
                            
                    var displayName = subKey.GetValue("DisplayName") as string;
                    var displayIcoPath = subKey.GetValue("DisplayIcon") as string;
                    var publisher = subKey.GetValue("Publisher") as string;
                    var installLocation = subKey.GetValue("InstallLocation") as string;
                            
                    var guidRegex = new Regex("/^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$/gm");
                    if (!string.IsNullOrEmpty(displayName))
                    {
                        installedApplications.Add(new InstalledApplication()
                        {
                            DisplayName = displayName ?? "Unknown",
                            InstalLocation = installLocation ?? "Unknown",
                            Publisher = publisher ?? "Unknown",
                            //DisplayIcon = LoadFromResource(new Uri(displayIcoPath) ?? null)
                        });
                    }
                }
            }
        }
        return installedApplications;
    }
    
    public static Bitmap LoadFromResource(Uri resourceUri)
    {
        return new Bitmap(AssetLoader.Open(resourceUri));
    }
}