using Entity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IToDoService service;

        public TodoController(IToDoService toDoService)
        {
            this.service = toDoService;
        }

        [HttpGet]
        public IActionResult Get(long userId)
        {
            if (userId > 0) return Ok(service.Get(userId).Data);
            return Ok(".Net Core, Entity Framework Core, Code First, Onion Architecture, Repository Pattern, Unit Of Work Pattern'lerinin birlikte kullanıldığı To Do app için api projesi.");
        }

        [HttpPost]
        public ToDo Post([FromBody] ToDo model)
        {
            service.Create(model);
            service.Save();
            return model;
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            service.Delete(id);
            service.Save();
            return Ok();
        }

    }
}