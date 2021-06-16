using HowartsAPI.DTOs;
using HowartsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowartsAPI.Controllers
{
    [Route("api/Candidates")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        #region Fields
        private readonly HogwartsContext context;
        #endregion

        #region Builder
        public CandidatesController(HogwartsContext context)
        {
            this.context = context;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtener candidato
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Candidate>>> GetCanditates()
        {
            try
            {
                var entity = await context.Candidates.ToListAsync();
                return entity;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Obtener candidato por ID
        /// </summary>
        /// <param name="id">Candidato ID</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetCanditateById")]
        public async Task<ActionResult<CandidateCompleteDtos>> GetCanditateById(int id)
        {
            try
            {
                var entity = await context.Candidates
                    .Where(c => c.CandidateId == id)
                    .Include(c => c.House)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return NotFound();
                }

                var candidateDto = new CandidateCreationDtos
                {
                    Name = entity.Name,
                    LastName = entity.LastName,
                    Id = entity.Id,
                    Age = entity.Age,
                    HouseId = entity.HouseId
                };

                var candiate = new CandidateCompleteDtos
                {
                    Canidate = candidateDto,
                    House = new HouseCreationDtos { NameHouse = entity.House.NameHouse, HouseId = entity.House.HouseId }
                };

                return candiate;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Crear Candidato
        /// </summary>
        /// <param name="dto">Data transfer objet</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateCandidate([FromBody] CandidateCreationDtos dto)
        {
            try
            {
                var entity = new Candidate
                {
                    Name = dto.Name,
                    LastName = dto.LastName,
                    Id = dto.Id,
                    Age = dto.Age,
                    HouseId = dto.HouseId,
                };
                context.Add(entity);


                if (entity.HouseId != 1 & entity.HouseId != 2 & entity.HouseId != 3 & entity.HouseId !=4)
                {
                    return StatusCode(400);
                }

                await context.SaveChangesAsync();
                return new CreatedAtRouteResult("GetCanditateById", new { id = entity.CandidateId }, entity);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Actualizar Candidato
        /// </summary>
        /// <param name="id">Candidato Id</param>
        /// <param name="dto">Data transfer objet</param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Candidate>> UpdateCandidate(int id, [FromBody] CandidateCreationDtos dto)
        {
            try
            {
                var entity = await context.Candidates.FindAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = dto.Name;
                entity.LastName = dto.LastName;
                entity.Id = dto.Id;
                entity.Age = dto.Age;
                entity.HouseId = dto.HouseId;
                
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
        /// Eliminar Candidato
        /// </summary>
        /// <param name="id">Candidato Id</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Candidate>> DeleteCandidate(int id)
        {
            try
            {
                var entity = await context.Candidates.FindAsync(id);
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
