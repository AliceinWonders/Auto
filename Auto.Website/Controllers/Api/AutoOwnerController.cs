using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Messages;
using Auto.Website.Models;
using Castle.Core.Internal;
using DefaultNamespace;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;

namespace Auto.Website.Controllers.Api
{
    [Route("api/[controller]")]
        [ApiController]
        public class AutoOwnerController : ControllerBase
        {
            private readonly IAutoDatabase db;
            private readonly IBus bus;

            public AutoOwnerController(IAutoDatabase db, IBus bus)
            {
                this.db = db;
                this.bus = bus;
            }
            
            private dynamic Paginate(string url, int index, int count, int total) {
                dynamic links = new ExpandoObject();
                links.self = new { href = url };
                links.final = new { href = $"{url}?index={total - (total % count)}&count={count}" };
                links.first = new { href = $"{url}?index=0&count={count}" };
                if (index > 0) links.previous = new { href = $"{url}?index={index - count}&count={count}" };
                if (index + count < total) links.next = new { href = $"{url}?index={index + count}&count={count}" };
                return links;
            }
            
            // GET: api/autoowners
            [HttpGet]
            [Produces("application/hal+json")]
            public IActionResult Get(int index = 0, int count = 10) {
                var items = db.ListAutoOwners().Skip(index).Take(count);
                var total = db.CountAutoOwner();
                var _links = Paginate("/api/autoowners", index, count, total);
                var _actions = new {
                    create = new {
                        method = "POST",
                        type = "application/json",
                        name = "Create a new auto owner",
                        href = "/api/autoowners"
                    },
                    delete = new {
                        method = "DELETE",
                        name = "Delete an autoowner",
                        href = "/api/autoowners/{id}"
                    }
                };
                var result = new {
                    _links, _actions, index, count, total, items
                };
                return Ok(result);
            }
            
            // GET api/autoowners/ABC123
            [HttpGet("{id}")]
            public IActionResult Get(string id) {
                var autoOwner = db.FindAutoOwner(id);
                if (autoOwner == default) return NotFound();
                var json = autoOwner.ToDynamic();
                json._links = new {
                    self = new { href = $"/api/autoowners/{id}" },
                    AutoOwnerVehicle = new { href = $"/api/vehicles/{autoOwner.AutoId}" }
                };
                json._actions = new {
                    update = new {
                        method = "PUT",
                        href = $"/api/autoowners/{id}",
                        accept = "application/json"
                    },
                    delete = new {
                        method = "DELETE",
                        href = $"/api/autoowners/{id}"
                    }
                };
                return Ok(json);
            }
            
            // POST api/autoowners
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] AutoOwnerDto dto) {
                var autoOwnerVehicle = db.FindVehicle(dto.AutoId);
                var autoOwner = new AutoOwner {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    Number = dto.Number,
                    Email = dto.Email,
                    AutoOwnerVehicle = autoOwnerVehicle
                };
                db.CreateAutoOwner(autoOwner);
                await PublishNewAutoOwnerMessage(autoOwner);
                return Ok(dto);
            }

            public static NewAutoOwnerMessage ToMessage(AutoOwner autoOwner)
            {
                var message = new NewAutoOwnerMessage()
                {
                    Name = autoOwner.Name,
                    Surname = autoOwner.Surname,
                    Number = autoOwner.Number,
                    Email = autoOwner.Email,
                    AutoId = autoOwner.AutoOwnerVehicle?.Registration,
                    ModelName = autoOwner.AutoOwnerVehicle?.VehicleModel?.Name,
                    CreatedAt = DateTimeOffset.UtcNow,
                };
                return message;
            }

            private async Task PublishNewAutoOwnerMessage(AutoOwner autoOwner)
            {
                var message = ToMessage(autoOwner);
                await bus.PubSub.PublishAsync(message);
            }
            
            // PUT api/autoowners/ABC123
            [HttpPut("{id}")]
            public IActionResult Put(string id, [FromBody] dynamic dto) {
                var AutoOwnerVehicleHref = dto._links.AutoOwnerVehicle.href;
                var AutoOwnerVehicleId = VehiclesController.ParseRegistration(AutoOwnerVehicleHref);
                var AutoOwnerVehicle = db.FindVehicle(AutoOwnerVehicleId);
                var autoOwner = new AutoOwner {
                    AutoId = id,
                    Name = dto.name,
                    Surname = dto.surname,
                    Number = dto.Number,
                    Email = dto.Email,
                    AutoOwnerVehicle = AutoOwnerVehicle.AutoId
                };
                db.UpdateAutoOwner(autoOwner);
                return Get(id);
            }
            
            // DELETE api/autoowners/ABC123
            [HttpDelete("{id}")]
            public IActionResult Delete(string id) {
                var autoOwner = db.FindAutoOwner(id);
                if (autoOwner == default) return NotFound();
                db.DeleteAutoOwner(autoOwner);
                return NoContent();
            }
        }
    }