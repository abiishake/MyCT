using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCT.Interface.BOObjects;
using MyCT.Interface.ServiceLocator;

namespace MyCT.Controller.Api
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : BaseController
    {
        public ShopController(IServiceLocator serviceLocator) : base(serviceLocator)
        {

        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddShop([FromBody] ShopDTO ShopDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<ShopDTO>(ShopDTO);
                var ShopBO = _serviceLocator.Resolve<IShopBO>();
                if (ShopBO.Add(ShopDTO, out _object) > 0)
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
        public IActionResult EditShop([FromBody] ShopDTO ShopDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestMessage();
                }

                AddCreatedBy<ShopDTO>(ShopDTO);
                var ShopBO = _serviceLocator.Resolve<IShopBO>();
                if (ShopBO.Add(ShopDTO, out _object) > 0)
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


                var ShopBO = _serviceLocator.Resolve<IShopBO>();
                var list = ShopBO.List();
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

        [HttpGet("{shopid}")]
        public IActionResult GetById(int shopid)
        {
            try
            {
                var ShopBO = _serviceLocator.Resolve<IShopBO>();
                var shop = ShopBO.GetById(shopid);
                if (shop != null)
                {
                    return Ok(shop);
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

        [HttpDelete("{shopid}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ShopBO = _serviceLocator.Resolve<IShopBO>();
                if (ShopBO.Remove(id))
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
