using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCT.Core.Model.DTO;
using MyCT.Interface.BOObjects;
using MyCT.Interface.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCT.Controller.Api
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : BaseController
    {
        public StatusController(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddStatus([FromBody] StatusDTO StatusDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<StatusDTO>(StatusDTO);
                var StatusBO = _serviceLocator.Resolve<IStatusBO>();
                if (StatusBO.Add(StatusDTO, out _object) > 0)
                {
                    return Created(new Uri("", UriKind.Relative), _object);
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("edit")]
        [HttpPut]
        public IActionResult EditStatus([FromBody] StatusDTO StatusDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<StatusDTO>(StatusDTO);
                var StatusBO = _serviceLocator.Resolve<IStatusBO>();
                if (StatusBO.Add(StatusDTO, out _object) > 0)
                {
                    return Ok(_object);
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpGet()]
        public IActionResult GetAll()
        {
            try
            {


                var StatusBO = _serviceLocator.Resolve<IStatusBO>();
                var list = StatusBO.List();
                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{statusid}")]
        public IActionResult GetById(int statusid)
        {
            try
            {
                var StatusBO = _serviceLocator.Resolve<IStatusBO>();
                var status = StatusBO.GetById(statusid);
                if (status != null)
                {
                    return Ok(status);
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("{statusid}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var StatusBO = _serviceLocator.Resolve<IStatusBO>();
                if (StatusBO.Remove(id))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequestMessage("Error Occured");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
