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

        // Note: ModelState.IsValid kontrolü iptal edildi. .net core'da model validation otomatik çalışıyor.
        // Hata varsa otomatik olarak badrequest dönüyor, validation mesajlarıyla.
        // Kaynak; https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.0#automatic-http-400-responses



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
        public IActionResult Post([FromBody] ToDoRequestModel.Create model)
        {
            // TODO: fwt ile user atama işlemini yap
            var result = service.Create(model);
            if (result.isSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ToDoRequestModel.Update model)
        {
            var result = service.Update(model);
            if (result.isSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var result = service.Delete(id);
            if (result.isSuccess) return Ok(result);
            return BadRequest(result);
        }

    }
}