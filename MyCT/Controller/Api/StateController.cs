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
    [Route("api/states")]
    [ApiController]
    public class StateController : BaseController
    {
        public StateController(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddState([FromBody] StateDTO StateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<StateDTO>(StateDTO);
                var StateBO = _serviceLocator.Resolve<IStateBO>();
                if (StateBO.Add(StateDTO, out _object) > 0)
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
        public IActionResult EditState([FromBody] StateDTO StateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<StateDTO>(StateDTO);
                var StateBO = _serviceLocator.Resolve<IStateBO>();
                if (StateBO.Add(StateDTO, out _object) > 0)
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


                var StateBO = _serviceLocator.Resolve<IStateBO>();
                var list = StateBO.List();
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

        [HttpGet("{stateid}")]
        public IActionResult GetById(int stateid)
        {
            try
            {
                var StateBO = _serviceLocator.Resolve<IStateBO>();
                var state = StateBO.GetById(stateid);
                if (state != null)
                {
                    return Ok(state);
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

        [HttpDelete("{stateid}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var StateBO = _serviceLocator.Resolve<IStateBO>();
                if (StateBO.Remove(id))
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
