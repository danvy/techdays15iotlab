using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using System.Threading;
using Microsoft.SPOT.Net.NetworkInformation;
using GT = Gadgeteer;
using System;
using System.Net;
using Gadgeteer.Networking;
using System.IO;
using System.Text;

namespace IoTLab.Device
{
    public partial class Program
    {
        private bool lightOn = false;
        private bool connected = false;
        private GT.Timer timer;
        private string webAPIUrl = "http://td15iotlab.azurewebsites.net/";
        void ProgramStarted()
        {
            led7C.SetColor(LED7C.Color.Off);
            button.ButtonPressed += button_ButtonPressed;
            ethernetENC28.UseThisNetworkInterface();
            NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
            //Prevent race condition where network status is not correctly updated
            Thread.Sleep(1);
            ethernetENC28.NetworkInterface.EnableDhcp();
            ethernetENC28.NetworkInterface.EnableDynamicDns();
            timer = new GT.Timer(1000);
            timer.Tick += timer_Tick;
            timer.Start();
            Debug.Print("Program Started");
        }
        private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            connected = ethernetENC28.NetworkInterface.IPAddress != "0.0.0.0";
        }
        void timer_Tick(GT.Timer timer)
        {
            if (!connected)
                return;
            using (var req = WebRequest.Create(webAPIUrl + "api/device/0"))
            {
                using (var res = req.GetResponse() as HttpWebResponse)
                {
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = res.GetResponseStream())
                        {
                            var reader = new StreamReader(stream);
                            var s = reader.ReadToEnd();
                            LightOn = s == "true";
                        }
                    }
                }
            }
        }
        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            if (!connected)
                return;
            //LightOn = !LightOn;
            using (var req = WebRequest.Create(webAPIUrl + "api/device/0"))
            {
                req.Method = "PUT";
                var content = (!LightOn).ToString().ToLower();
                var contentData = Encoding.UTF8.GetBytes(content);
                req.ContentLength = contentData.Length;
                req.ContentType = "application/json;charset=utf-8";
                using (var stream = req.GetRequestStream())
                {
                    stream.Write(contentData, 0, contentData.Length);
                }
                using (var res = req.GetResponse() as HttpWebResponse)
                {
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        Debug.Print("Switch");
                    }
                }
            }
        }
        bool LightOn
        {
            get
            {
                return lightOn;
            }
            set
            {
                if (value == lightOn)
                    return;
                lightOn = value;
                led7C.SetColor(lightOn ? LED7C.Color.Green : LED7C.Color.Off);
                Debug.Print("Light " + (lightOn ? "On" : "Off"));
            }
        }
    }
}
