using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyThingsDotIo.Models;
using MyThingsDotIo.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyThingsDotIo.Controllers.Api
{
    [Route("api/person")]
    public class UserController : Controller
    {
        private ILogger<UserController> _logger;
        private IMyThingsDotIoRepository _repository;

        public UserController(IMyThingsDotIoRepository repository, ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var people = _repository.GetAll();
                return Ok(Mapper.Map<IEnumerable<PersonViewModel>>(people));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all users: {ex}");

                return BadRequest("Error occurred");
            }
        }

        [HttpGet("{alias}")]
        public IActionResult GetByAlias(string alias)
        {
            try
            {
                var person = _repository.GetByAlias(alias);
                if (person != null)
                    return Ok(Mapper.Map<PersonViewModel>(person));

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");

                return BadRequest("An error occurred");
            }
        }

        [HttpGet("{uuid}")]
        public IActionResult GetGetByUuid(Guid uuid)
        {
            try
            {
                var person = _repository.GetByUniqueId(uuid);
                if (person != null)
                    return Ok(Mapper.Map<PersonViewModel>(person));

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");

                return BadRequest("An error occurred");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] PersonViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var person = Mapper.Map<Person>(vm);
                _logger.LogInformation("Attempting to add new user");
                _repository.Add(person);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/person/{vm.Alias}", Mapper.Map<PersonViewModel>(person));
                }
            }

            return BadRequest("Failed to save person");
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] PersonViewModel vm)
        {
            if (vm == null)
                return BadRequest();

            var item = _repository.GetByUniqueId(vm.UniqueId);
            if (item == null)
                return NotFound();

            _repository.Update(Mapper.Map<Person>(vm));

            if (await _repository.SaveChangesAsync())
            {
                return new NoContentResult();
            }

            return BadRequest("Failed to update person");
        }

        [HttpDelete("{alias}")]
        public void Delete(string alias)
        {
            _repository.Remove(alias);
        }

        [HttpDelete("{uuid}")]
        public void Delete(Guid uniqueId)
        {
            _repository.Remove(uniqueId);
        }

    }
}
