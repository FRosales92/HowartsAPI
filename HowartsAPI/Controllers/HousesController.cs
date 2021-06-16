using HowartsAPI.DTOs;
using HowartsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowartsAPI.Controllers
{
    [Route("api/Houses")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        #region Fields
        private readonly HogwartsContext context;

        #endregion

        #region Builder
        public HousesController(HogwartsContext context)
        {
            this.context = context;
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Obtener Casas de estudio
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<House>>> GetHouses()
        {
            try
            {
                var entity = await context.Houses.ToListAsync();
                return entity;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Obtener Casas de estudio por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetHouseById")]
        public async Task<ActionResult<House>> GetHouseById(int id)
        {
            try
            {
                var entity = await context.Houses.FindAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }
                return entity;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        #endregion
    }
}
