using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Models;
using dbSettings.DataAccess;
namespace ToDoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {

        private readonly ILogger<ToDoListController> _logger;

        public ToDoListController(ILogger<ToDoListController> logger)
        {
            _logger = logger;
        }
        [HttpGet("{id}")]
        public ToDo Get(int id)
        {
            ToDoDAL tododal = new ToDoDAL();
            ToDo toDo=tododal.ReadToDo(id);
            Console.WriteLine(toDo.Id);
            return  toDo;
        }
        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            ToDoDAL tododal = new ToDoDAL();
            List<ToDo> todos = tododal.GetToDosAsGenericList();
            return todos;
        }

        [HttpPost]
        public ActionResult Post(ToDo td)
        {
            try
            {
                ToDoDAL tddal = new ToDoDAL();
                tddal.InsertToDo(td);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                ToDoDAL tddal = new ToDoDAL();
                tddal.DeleteToDo(id);
            }
            catch
            {
                return BadRequest("somthing went wrong");
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody]ToDo td)
        {
            try
            {
                ToDoDAL tddal = new ToDoDAL();
                tddal.updateToDo(td);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }
    }
}