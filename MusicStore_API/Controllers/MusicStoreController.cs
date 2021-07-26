using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore_API.Middleware.Auth;
using MusicStore_API.Models;
using MusicStore_API.Util;
using MusicStore_Common.Entities;
using MusicStore_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore_API.Controllers
{

    /// <summary>
    /// CRUD operation for the music store.
    /// </summary>
    [Route("store")]
    [ApiController]
    public class MusicStoreController : ControllerBase
    {

        private IMusicStoreService _musicStoreService;

        public MusicStoreController(IMusicStoreService musicStoreService)
        {
            _musicStoreService = musicStoreService;
        }

        /// <summary>
        /// Get the item by the given id.
        /// </summary>
        /// <param name="id">GUID id of item.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /0edd7334-167d-4149-8b75-1e50a80d161d
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Policies.All)]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ItemModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {

            if (id == Guid.Empty)
                return BadRequest("Id must not be empty");

            var result = await _musicStoreService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result.ToModel());

        }

        /// <summary>
        /// Get all existing items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Policies.All)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<ItemModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync()
        {
        
            var results =await _musicStoreService.GetAsync();
            if (!results.Any())
                return NoContent();

            return Ok(results.Select(x => x.ToModel()));
        
        }

        /// <summary>
        /// Get all existing items. The results will be paged
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Policies.All)]
        [Route("paged")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<ItemModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
         
            var results = await _musicStoreService.GetAsync(pageNumber, pageSize);
            if (!results.Any())
                return NoContent();

            return Ok(results.Select(x => x.ToModel()));
       
        }

        /// <summary>
        /// Add an item in the system.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ItemModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddAsync([FromBody] ItemModel model)
        {
          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemDto = model.ToDto(null);

            //the user id is extracted from HttpContext and inserted in the itemDto to be passed to service
            var addedBy = (User)HttpContext.Items["User"];
            itemDto.AddedById = addedBy.Id;

            var result = await _musicStoreService.AddAsync(itemDto);

            return Ok(result.ToModel());
       
        }

        /// <summary>
        /// Update an existing item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ItemModel model)
        {
         
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty");

            if (id != model.Id)
                return BadRequest("Ids do not match");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _musicStoreService.UpdateAsync(model.ToDto(id));
            if (result)
                return Ok();

            return NotFound();
        
        }

        /// <summary>
        /// Delete an existing item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Policy = Policies.Admin)]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
        
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty");

            var result = await _musicStoreService.RemoveAsync(id);
            if (result)
                return Ok();

            return NotFound();
      
        }

    }

}