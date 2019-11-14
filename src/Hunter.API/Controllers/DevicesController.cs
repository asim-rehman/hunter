using Hunter.API.Framework;
using Hunter.DataBase;
using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Models;
using Hunter.DataBase.Repository;
using Hunter.DataBase.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hunter.API.Controllers
{
    /// <summary>
    /// API Controller for Devices
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : BaseAPIController
    {
        private readonly IDevicesRepository devicesRepository;
        private readonly IHunterDBContext hunter;
        public DevicesController(IHunterDBContext hunter)
        {
            devicesRepository = new DevicesRepository(hunter);
            this.hunter = hunter;
        }

        // GET: api/Devices
        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                IList<Devices> devices = devicesRepository.RetrieveAll().Where(i => i.User.Id == CurrentUser(hunter).Id).OrderBy(p => p.Name).ToList();
                return JsonResponse(devices);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            try
            {
                var devices = devicesRepository.RetrieveByPK(id);
                if (devices == null || devices.User != CurrentUser(hunter))
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }
                return JsonResponse(devices);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        // PUT: api/Devices/5
        [HttpPut]
        public JsonResult Put(Devices devices)
        {

            if (!ModelState.IsValid)
            {
                return JsonResponse(ResponseType.error, ValidationErrors());
            }

            try
            {
                devicesRepository.Update(devices);
                devicesRepository.SaveChanges();
                return JsonResponse(ResponseType.success, ResponseMessage.ChangesSaved);
            }
            catch (DuplicateItemException die)
            {
                return JsonResponse(ResponseType.error, die.Message);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }
        }

        // POST: api/Devices
        [HttpPost]
        public JsonResult Post([FromBody]Devices devices)
        {
            if (!ModelState.IsValid)
            {
                return JsonResponse(ResponseType.error, ResponseMessage.ValidationErrors);
            }
            try
            {
                devices.User = CurrentUser(hunter);
                devicesRepository.Add(devices);
                devicesRepository.SaveChanges();
                return JsonResponse(ResponseType.success, ResponseMessage.ChangesSaved);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }

        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var devices = devicesRepository.RetrieveByPK(id);
                if (devices == null || devices.User != CurrentUser(hunter))
                {
                    return JsonResponse(ResponseType.error, ResponseMessage.ItemNotFound);
                }

                devicesRepository.Delete(devices);
                devicesRepository.SaveChanges();

                return JsonResponse(ResponseType.success, ResponseMessage.DeletedSuccessfully);
            }
            catch (Exception ex)
            {
                return InternalException(ex);
            }

        }

    }
}
