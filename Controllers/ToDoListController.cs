using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void Post(ToDo td)
        {
            ToDoDAL tddal = new ToDoDAL();
            tddal.InsertToDo(td);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ToDoDAL tddal = new ToDoDAL();
            tddal.DeleteToDo(id);
        }

        [HttpPut]
        public void Update([FromBody]ToDo td)
        {
            ToDoDAL tddal = new ToDoDAL();
            tddal.updateToDo(td);
        }
    }
}