using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security;
using System.Collections;
using System.Text;

namespace NetLicensingDemo
{
    /// <summary>
    /// Generates a 16 byte Unique Identification code of a computer
    /// Example: 4876-8DB5-EE85-69D3-FE52-8CF7-395D-2EA9
    /// </summary>
    public class HardwareFingerprint  
    {
        private static string fingerPrint = string.Empty;
        public static string Value()
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                fingerPrint = GetHash(
                        "CPU >> " + cpuId()
                    + "\nBIOS >> " + biosId()
                    + "\nBASE >> " + baseId()
                    //+ "\nDISK >> " + diskId()
                    //+ "\nVIDEO >> " + videoId()
                    //+ "\nMAC >> " + macId()
                );
            }
            return fingerPrint;
        }

        private static string _rawHWID = string.Empty;
        public static string rawHWID()
        {
            if (string.IsNullOrEmpty(_rawHWID))
            {
                _rawHWID =
                  "CPU >> " + cpuId ()
                + "\nBIOS >> " + biosId ()
                + "\nBASE >> " + baseId ()
                + "\nDISK >> " + diskId ()
                + "\nVIDEO >> " + videoId ()
                + "\nMAC >> " + macId ();
            }
            return _rawHWID;
        }

        public static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }

        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = 
                new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = 
                new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        private static string cpuId()
        {
            string retVal = "";
            if (IsRunningMono ()) {
                retVal = "not supported";
            } else {
                //Uses first CPU identifier available in order of preference
                //Don't get all identifiers, as it is very time consuming
                retVal = identifier ("Win32_Processor", "UniqueId");
                if (retVal == "") { //If no UniqueID, use ProcessorID
                    retVal = identifier ("Win32_Processor", "ProcessorId");
                    if (retVal == "") { //If no ProcessorId, use Name
                        retVal = identifier ("Win32_Processor", "Name");
                        if (retVal == "") { //If no Name, use Manufacturer
                            retVal = identifier ("Win32_Processor", "Manufacturer");
                        }
                        //Add clock speed for extra security
                        retVal += identifier ("Win32_Processor", "MaxClockSpeed");
                    }
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            string retVal = "";
            if (IsRunningMono ()) {
                retVal = "not supported";
            } else {
                retVal = identifier ("Win32_BIOS", "Manufacturer")
                + identifier ("Win32_BIOS", "SMBIOSBIOSVersion")
                + identifier ("Win32_BIOS", "IdentificationCode")
                + identifier ("Win32_BIOS", "SerialNumber")
                + identifier ("Win32_BIOS", "ReleaseDate")
                + identifier ("Win32_BIOS", "Version");
            }
            return retVal;
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            string retVal = "";
            if (IsRunningMono ()) {
                retVal = "not supported";
            } else {
                retVal = identifier ("Win32_DiskDrive", "Model")
                + identifier ("Win32_DiskDrive", "Manufacturer")
                + identifier ("Win32_DiskDrive", "Signature")
                + identifier ("Win32_DiskDrive", "TotalHeads");
            }
            return retVal;
        }
        //Motherboard ID
        private static string baseId()
        {
            string retVal = "";
            if (IsRunningMono ()) {
                retVal = "not supported";
            } else {
                retVal = identifier ("Win32_BaseBoard", "Model")
                + identifier ("Win32_BaseBoard", "Manufacturer")
                + identifier ("Win32_BaseBoard", "Name")
                + identifier ("Win32_BaseBoard", "SerialNumber");
            }
            return retVal;
        }
        //Primary video controller ID
        private static string videoId()
        {
            string retVal = "";
            if (IsRunningMono ()) {
                retVal = "not supported";
            } else {
                retVal = identifier ("Win32_VideoController", "DriverVersion")
                + identifier ("Win32_VideoController", "Name");
            }
            return retVal;
        }
        //First enabled network card ID
        private static string macId()
        {
            string retVal = "";
            if (IsRunningMono()) {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
                    if ((nic.OperationalStatus == OperationalStatus.Up) ||
                        ((nic.OperationalStatus == OperationalStatus.Unknown) &&
                            (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                            (nic.Supports(NetworkInterfaceComponent.IPv4) ||
                             nic.Supports(NetworkInterfaceComponent.IPv6)))) {
                        retVal += nic.GetPhysicalAddress().ToString ();
                        break;
                    }
                }
            } else {
                retVal = identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
            }
            return retVal;
        }
        #endregion

        private static bool IsRunningMono()
        {
            // With our application, it will be used on an embedded system, and we know that
            // Windows will never be running Mono, so you may have to adjust accordingly.
            return Type.GetType("Mono.Runtime") != null;
        }
            
    }
}