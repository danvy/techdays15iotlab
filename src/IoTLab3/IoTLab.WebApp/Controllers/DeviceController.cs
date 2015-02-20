using IoTLab.WebApp.Models;
using System.Web.Http;

namespace IoTLab.WebApp.Controllers
{
    public class DeviceController : ApiController
    {
        // GET: api/Device/
        public bool Get()
        {
            return DeviceModel.Instance.LightOn;
        }
        // PUT: api/Device/
        public IHttpActionResult Put([FromBody]bool value)
        {
            DeviceModel.Instance.LightOn = value;
            return Ok();
        }
    }
}
