﻿using Entity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Collections.Generic;

namespace Api.Controllers
{

    // TODO: Bu dokümana göre api akışını kontrol et, dönüş modeli, otomatik hata döndürme vs 
    // https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.0
    // https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.0#automatic-http-400-responses



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
        public IActionResult Post([FromBody] ToDoRequestModel.Create model)
        {
            if (ModelState.IsValid)
            {
                // TODO: fwt ile user atama işlemini yap
                var result = service.Create(model);
                if (result.isSuccess) return Ok(result);
                return BadRequest(result);
            }

            return Ok(model);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ToDoRequestModel.Update model)
        {
            if (ModelState.IsValid)
            {
                var result = service.Update(model);
                if (result.isSuccess) return Ok(result);
                return BadRequest(result);
            }
            return Ok(model);
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