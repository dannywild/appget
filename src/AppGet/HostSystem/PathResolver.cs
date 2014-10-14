using System;
using System.IO;
using System.Security.AccessControl;
using AppGet.FlightPlans;

namespace AppGet.HostSystem
{
    public interface IPathResolver
    {
        string InstalledPackageList { get; }
        string GetInstallerTempPath(string fileName);
        string GetInstallerLogFile(FlightPlan flightPlan);
        string GetInstallationPath(FlightPlan flightPlan);
    }

    public class PathResolver : IPathResolver
    {
        private string TempFolder
        {
            get
            {
                return Path.GetTempPath();
            }
        }

        private string ProgramData
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            }
        }

        private string AppGetWrokingDirectory
        {
            get { return Path.Combine(ProgramData, "AppGet"); }
        }

        public string InstalledPackageList
        {
            get { return Path.Combine(AppGetWrokingDirectory, "packages.yaml"); }
        }

        public string GetInstallerTempPath(string fileName)
        {
            return Path.Combine(TempFolder, fileName);
        }

        public string GetInstallerLogFile(FlightPlan flightPlan)
        {
            var installerLogDir = Path.Combine(AppGetWrokingDirectory, "Logs");

            Directory.CreateDirectory(installerLogDir);

            var fileName = string.Format("{0}_{1:yyyyMMdd_HHssmm}.log", flightPlan.Id, DateTime.Now);

            return Path.Combine(installerLogDir, fileName);
        }


        public string GetInstallationPath(FlightPlan flightPlan)
        {
            switch (flightPlan.InstallMethod)
            {
                case InstallMethodType.Zip:
                    {
                        var folderName = string.Format("{0}-{1}", flightPlan.Id, flightPlan.Version);
                        return Path.Combine(AppGetWrokingDirectory, folderName);
                    }
                default:
                    {
                        throw new NotImplementedException(flightPlan.InstallMethod + " is not supported yet.");
                    }
            }
        }
    }
}