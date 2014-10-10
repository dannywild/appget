﻿using System;

namespace AppGet.FlightPlans
{
    public class FlightPlan
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public string ApplicationUrl { get; set; }

        public string[] Exe { get; set; }
        public InstallerType Installer { get; set; }

        public PackageSource[] Packages { get; set; }
    }

    public class PackageSource
    {
        public string Source { get; set; }
        public string Sha256 { get; set; }

        public ArchitectureType Architecture { get; set; }

        //http://en.wikipedia.org/wiki/List_of_Microsoft_Windows_versions
        public Version MinWindowsVersion { get; set; }
        public Version MaxWindowsVersion { get; set; }
    }

    public enum ArchitectureType
    {
        Any,
        x86,
        x64,
        Itanium,
        ARM
    }

    public enum InstallerType
    {
        Zip,
        MSI,
        Inno,
        InstallShield,
        NSIS
    }
}