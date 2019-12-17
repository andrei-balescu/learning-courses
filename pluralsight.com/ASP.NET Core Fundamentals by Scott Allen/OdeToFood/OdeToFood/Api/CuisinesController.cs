using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Core;

namespace OdeToFood.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuisinesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetCuisines()
        {
            string[] cuisines = Enum.GetNames(typeof(CuisineType));
            return cuisines;
        }
    }
}