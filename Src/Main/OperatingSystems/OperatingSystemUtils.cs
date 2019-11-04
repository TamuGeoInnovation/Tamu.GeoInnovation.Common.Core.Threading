using System;

namespace USC.GISResearchLab.Common.Core.Utils.OperatingSystems
{
    public class OperatingSystemUtils
    {

        // following method from: http://support.microsoft.com/default.aspx?scid=kb%3Ben-us%3B304283
        public static string GetOSVersion()
        {
            string ret = null;

            OperatingSystem osInfo = Environment.OSVersion;

            // Determine the platform.
            switch (osInfo.Platform)
            {
                // Platform is Windows 95, Windows 98, Windows 98 Second Edition, or Windows Me.
                case PlatformID.Win32Windows:

                    switch (osInfo.Version.Minor)
                    {
                        case 0:
                            ret = "Windows 95";
                            break;
                        case 10:
                            if (osInfo.Version.Revision.ToString() == "2222A")
                                ret = "Windows 98 Second Edition";
                            else
                                ret = "Windows 98";
                            break;
                        case 90:
                            ret = "Windows Me";
                            break;
                    }
                    break;

                // Platform is Windows NT 3.51, Windows NT 4.0, Windows 2000, or Windows XP.
                case PlatformID.Win32NT:

                    switch (osInfo.Version.Major)
                    {
                        case 3:
                            ret = "Windows NT 3.51";
                            break;
                        case 4:
                            ret = "Windows NT 4.0";
                            break;
                        case 5:
                            if (osInfo.Version.Minor == 0)
                                ret = "Windows 2000";
                            else
                                ret = "Windows XP";
                            break;
                        case 6:
                            ret = "Windows Vista";
                            break;
                    }
                    break;
            }

            return ret;

        }

    }
}

