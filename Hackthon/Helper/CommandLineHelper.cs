using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace Hackthon.Helper
{
    public class CommandLineHelper
    {
        /// <summary>
        /// Run command
        /// </summary>
        /// <param name="cmdExe">Exe path</param>
        /// <param name="cmdStr">Argument string</param>
        static public bool RunCommand(string cmdExe, string cmdStr)
        {
            if (!File.Exists(cmdExe))
            {
                return false;
            }

            bool result = false;
            string str = string.Empty;
            try
            {
                using (Process myPro = new Process())
                {
                    myPro.StartInfo.FileName = "cmd.exe";
                    myPro.StartInfo.UseShellExecute = false;
                    myPro.StartInfo.RedirectStandardInput = true;
                    myPro.StartInfo.RedirectStandardError = true;
                    myPro.StartInfo.RedirectStandardOutput = true;
                    myPro.StartInfo.CreateNoWindow = true;
                    myPro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    str = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");

                    myPro.Start();
                    myPro.BeginErrorReadLine();
                    myPro.BeginOutputReadLine();
                    myPro.StandardInput.WriteLine(str);
                    myPro.StandardInput.AutoFlush = true;
                    myPro.WaitForExit(10 * 60 * 1000);

                    result = (myPro.ExitCode == 0);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}