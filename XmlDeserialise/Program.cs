using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlDeserialise
{
    [Serializable]
    public class Request
    {
        [Serializable]
        public class FtpParams
        {
            public string Server { get; set; }
            public string Directory { get; set; }
            public int Port { get; set; } = 21;

        }
        public FtpParams FtpParams1 { get; set; }
        public string EnvName { get; set; }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (!args.Any())
                {
                    throw new Exception($"You must supply an argument.");
                }

                var filename = args[0];
                var ser = new XmlSerializer(typeof(Request));
                Request request;
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    request = (Request)ser.Deserialize(reader);
                }

                Console.WriteLine($"{request.FtpParams1.Directory}");
                Console.WriteLine($"{request.FtpParams1.Port}");
                Console.WriteLine($"{request.EnvName}");
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}
