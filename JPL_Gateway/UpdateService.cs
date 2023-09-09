using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JPL_Gateway
{
    class UpdateService
    {

        public static string SERVERADDR = "https://jplk001.koreacentral.cloudapp.azure.com";
        //private static string SERVERADDR = "http://localhost:6001/";

        public static JObject checkPgmUpdateVersion(string versionname)
        {

            Console.WriteLine("Version {0}", versionname);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            JObject jobj = null;


            var macAddr = (
                            from nic in NetworkInterface.GetAllNetworkInterfaces()
                            where nic.OperationalStatus == OperationalStatus.Up
                            select nic.GetPhysicalAddress().ToString()
                        ).FirstOrDefault();

            try
            {

                var client = new RestClient(SERVERADDR);
                var request = new RestRequest("/api/version/jpl/gateway");
                request.AddHeader("Accept", "application/json");
                request.AddParameter("modeluuid", "ba967c1d-6297-4bc4-a0b3-a52c1745e153");
                request.AddParameter("versionno", versionname);
                request.AddParameter("mode", "latest");
                request.AddParameter("tag", "korea");
                request.AddParameter("pcuuid", macAddr);


                var response = client.Post(request);

                Console.WriteLine(response.Content.ToString());

                jobj = JObject.Parse(response.Content.ToString());


                return jobj;
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        // version --> versionname 
        public static JObject checkUpdateVersionBinary(string vid, string pid, string version)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            JObject jobj = null;

            var macAddr = (
                            from nic in NetworkInterface.GetAllNetworkInterfaces()
                            where nic.OperationalStatus == OperationalStatus.Up
                            select nic.GetPhysicalAddress().ToString()
                        ).FirstOrDefault();

            try
            {
                //Console.WriteLine("checkUpdateVersion {0} {1} {2}", vid, pid, version);
                var client = new RestClient(SERVERADDR);
                var request = new RestRequest("/api/version/jpl/bbin");
                request.AddHeader("Accept", "application/json");
                request.AddParameter("pcuuid", macAddr);
                request.AddParameter("vid", String.Format("0x{0}",vid));
                request.AddParameter("pid", String.Format("0x{0}", pid));
                request.AddParameter("version", version);
                var response = client.Post(request);
                jobj = JObject.Parse(response.Content.ToString());
                if (jobj != null)
                {
                    if (jobj["update"].ToString().Equals("True"))
                    {
                        new Thread(() =>
                        {
                            Thread.CurrentThread.IsBackground = true;

                            using (var client2 = new WebClient())
                            {
                                // neet setup filedownload url 
                                string downpath = jobj["filepath"].ToString();
                                string filename2 = jobj["filename"].ToString();

                                string result = Path.GetTempPath();
                                string downloadurl = String.Format("{0}{1} ", UpdateService.SERVERADDR, downpath);
                                if (!File.Exists(result + filename2))
                                {
                                    client2.DownloadFile(downloadurl, result + filename2);
                                }
                            }
                        }).Start();
                    }
                }
                return jobj;
            }
            catch (Exception ex)
            {
                return null;
            }



        }
    }
}
