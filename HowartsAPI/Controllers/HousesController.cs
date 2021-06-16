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

        /// <summary>
        /// Crear Casa de estudio
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateHouse([FromBody] HouseCreationDtos dto)
        {
            try
            {
                var entity = new House
                {
                    NameHouse = dto.NameHouse
                };
                context.Add(entity);
                await context.SaveChangesAsync();
                return new CreatedAtRouteResult("GetHouseById", new { id = entity.HouseId }, entity);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Actualizar Casa de estudio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Candidate>> UpdateHouse(int id, [FromBody] HouseCreationDtos dto)
        {
            try
            {
                var entity = await context.Houses.FindAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.HouseId = dto.HouseId;
                entity.NameHouse = dto.NameHouse;
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Elimnar Casa de estudio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Candidate>> DeleteHouse(int id)
        {
            try
            {
                var entity = await context.Houses.FindAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }
                context.Remove(entity);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        #endregion
    }
}
