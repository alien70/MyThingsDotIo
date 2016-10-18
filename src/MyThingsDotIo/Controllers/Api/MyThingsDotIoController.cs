using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyThingsDotIo.Models;
using MyThingsDotIo.ViewModels;
using System;
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

        #region User

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var people = _repository.GetAll();
                return Ok(Mapper.Map<IEnumerable<UserViewModel>>(people));
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
                    return Ok(Mapper.Map<UserViewModel>(person));

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
                    return Ok(Mapper.Map<UserViewModel>(person));

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user: {ex}");

                return BadRequest("An error occurred");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var person = Mapper.Map<User>(vm);
                _logger.LogInformation("Attempting to add new user");
                _repository.Add(person);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/person/{vm.Alias}", Mapper.Map<UserViewModel>(person));
                }
            }

            return BadRequest("Failed to save person");
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] UserViewModel vm)
        {
            if (vm == null)
                return BadRequest();

            var item = _repository.GetByUniqueId(vm.UniqueId);
            if (item == null)
                return NotFound();

            _repository.Update(Mapper.Map<User>(vm));

            if (await _repository.SaveChangesAsync())
            {
                return new NoContentResult();
            }

            return BadRequest("Failed to update person");
        }

        [HttpDelete("{alias}")]
        public async Task<IActionResult> Delete(string alias)
        {
            if (alias == null || alias.Length == 0)
                return BadRequest();

            var person = await _repository.Remove(alias);

            if (person == null)
                return NotFound();

            return Ok(Mapper.Map<UserViewModel>(person));
        }

        [HttpDelete("{uuid}")]
        public async Task<IActionResult> Delete(Guid? uniqueId)
        {
            if (!uniqueId.HasValue)
                return BadRequest();

            var person = await _repository.Remove(uniqueId);

            if (person == null)
                return NotFound();

            return Ok(Mapper.Map<UserViewModel>(person));
        }

        #endregion

        #region Contacts

        [HttpGet("{alias}/contacts")]
        public IActionResult GetContactsByAlias(string alias)
        {
            try
            {
                var contacts = _repository.GetContactsByAlias(alias);
                if (contacts != null)
                    return Ok(Mapper.Map<IEnumerable<ContactViewModel>>(contacts));

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get contacts: {ex}");

                return BadRequest("An error occurred");
            }
        }

        [HttpPost("{alias}/contacts")]
        public async Task<IActionResult> Post(string alias, [FromBody] ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newContact = Mapper.Map<Contact>(vm);
                _repository.Add(alias, newContact);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/person/{alias}/{vm.Value}", Mapper.Map<ContactViewModel>(newContact));
                }
            }

            return BadRequest("Failed to save contact");
        }
        #endregion
    }
}
