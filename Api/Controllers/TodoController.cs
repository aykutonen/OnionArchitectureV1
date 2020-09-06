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
        private readonly IService<ToDo> service;

        public TodoController(IService<ToDo> toDoService)
        {
            this.service = toDoService;
        }

        [HttpGet]
        public IEnumerable<ToDo> Get(long userId)
        {
            return service.Get(userId).Data;
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