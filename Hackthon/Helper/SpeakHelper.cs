using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Hackthon.Helper
{
    public class SpeakHelper 
    {
        public static void Speak(string text, string voiceId, Stream outputStream)
        {
            string guid = Guid.NewGuid().ToString();
            string textPath = string.Format(@"Temp\text\{0}_{1}.txt", voiceId, guid);
            EnsureFolderExist(textPath);
            using (StreamWriter writer = new StreamWriter(textPath))
            {
                writer.WriteLine(text);
            }

            string wavePath = string.Format(@"Temp\wave\{0}_{1}", voiceId, guid);
            EnsureFolderExist(wavePath);
            string args = string.Format("-i {0} -o {1} -k {2}", textPath, wavePath, voiceId);
            if (CommandLineHelper.RunCommand(HttpContext.Current.Server.MapPath(@"~\bin\") +
                @"Resource\Font\Platform\Text2WaveSAPI.exe", args))
            {
                using (Stream stream = new FileStream(wavePath, FileMode.Open, FileAccess.Read))
                {
                    outputStream = stream;
                }
            }

            if (Directory.Exists(wavePath))
            {
                Directory.Delete(wavePath, true);
            }

            if (File.Exists(textPath))
            {
                File.Delete(textPath);
            }
        }

        public static void SaveVoice(string voiceId, string fontPath)
        {
            
        }

        private static void EnsureFolderExist(string path)
        {
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}