using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTLab.WebApp.Models
{
    public class DeviceModel
    {
        public static readonly DeviceModel Instance = new DeviceModel();
        private DeviceModel()
        {
        }
        public bool LightOn { get; set; }
    }
}