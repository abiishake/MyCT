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
    [Route("api/cities")]
    [ApiController]
    public class CityController : BaseController
    {
        public CityController(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddCity([FromBody] CityDTO CityDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<CityDTO>(CityDTO);
                var CityBO = _serviceLocator.Resolve<ICityBO>();
                if (CityBO.Add(CityDTO, out _object) > 0)
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
        public IActionResult EditCity([FromBody] CityDTO CityDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<CityDTO>(CityDTO);
                var CityBO = _serviceLocator.Resolve<ICityBO>();
                if (CityBO.Add(CityDTO, out _object) > 0)
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


                var CityBO = _serviceLocator.Resolve<ICityBO>();
                var list = CityBO.List();
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

        [HttpGet("{cityid}")]
        public IActionResult GetById(int cityid)
        {
            try
            {
                var CityBO = _serviceLocator.Resolve<ICityBO>();
                var city = CityBO.GetById(cityid);
                if (city != null)
                {
                    return Ok(city);
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

        [HttpDelete("{cityid}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var CityBO = _serviceLocator.Resolve<ICityBO>();
                if (CityBO.Remove(id))
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
