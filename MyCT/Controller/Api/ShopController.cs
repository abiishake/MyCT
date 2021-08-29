using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCT.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MyCT.Interface.BOObjects;

namespace MyCT.Controller.Api
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : BaseController
    {
        public ShopController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        [Route("add")]
        [HttpPost]
        public IActionResult PostAddShop([FromBody] ShopDTO shopDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }

                var shopBO = _serviceProvider.GetService<IShopBO>();
                if (shopBO.Add(shopDTO) > 0)
                {
                    return Ok("Shop Added Successfully");
                }
                else
                {
                    return BadRequest("Error Occured");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
