using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IO;
using System.Net;

namespace JPL_Gateway
{
    class UpdateSoftphone
    {
        private static string SERVERADDR = "http://softlink.headsetkorea.com/index.php/json/headset/version/";


        // version --> versionname
        public static JObject checkUpdateVersion(string vid, string pid, string version)
        {
            JObject rtnjson;
            //Console.WriteLine("checkUpdateVersion {0} {1} {2}",vid,pid,version);
            try
            {
                Console.WriteLine("checkUpdateVersion {0} {1} {2}", vid, pid, version);
                var client = new RestClient(SERVERADDR);
                string param = String.Format("/vid/0x{0}/pid/0x{1}/rev/0x{2}", vid, pid, version);
                string filename = String.Format("vid{0}pid{1}.json", vid, pid);
                string fullpath = Path.GetTempPath() + filename;

                var request = new RestRequest(param, Method.GET);

                IRestResponse response = client.Execute(request);
                var content = response.Content;

                if (content.Length == 0)
                {
                    content = @"{ update: false }";
                }
                else
                {
                }
                System.IO.File.WriteAllText(Path.GetTempPath() + filename, content);

                //Console.WriteLine("Path.GetTempPath() " + Path.GetTempPath());
                //Console.WriteLine(content);
                try
                {
                    rtnjson = JObject.Parse(content);

                    if (rtnjson["update"].ToString().Equals("True"))
                    {
                        using (var client2 = new WebClient())
                        {
                            string downpath = rtnjson["path"].ToString();
                            string filename2 = rtnjson["filename"].ToString();

                            string result = Path.GetTempPath();
                            Console.WriteLine(result);

                            if (!File.Exists(result + filename2))
                            {
                                Console.WriteLine(" =================  download ================ ");
                                client2.DownloadFile(downpath, result + filename2);
                            }
                        }
                    }
                    if (!version.Equals(rtnjson["version"]))
                    {
                    }
                    return rtnjson;
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}