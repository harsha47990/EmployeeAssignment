using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using EmployeeWebAPI.Models;
using Newtonsoft.Json;

namespace EmployeeWebAPI.Controllers
{
    public class ValuesController : ApiController
    {        // GET api/values

        public List<Employee> Get()
        {
            AdventureWorks2016Entities db = new AdventureWorks2016Entities();

            return db.Employees.Where(x=> x.Status==1).ToList();
        }

        // GET api/values/5
        public Employee Get(int id)
        {
            Employee emp = new Employee();

            var db = new AdventureWorks2016Entities();
            emp = db.Employees.Where(x => x.id == id).FirstOrDefault();
            return emp;
        }

        // POST api/values
        public string Post([FromBody] dynamic value)
        {
            //Employee emp = WebAPIUtils.Convert(value);

            Employee emp = JsonConvert.DeserializeObject<Employee>(value.ToString());
            AdventureWorks2016Entities db = new AdventureWorks2016Entities();
            int max = db.Database.SqlQuery<int>("Select max(id) from dbo.Employee").FirstOrDefault(); ;
            emp.id = max + 1;
            emp.Status = 1;
            db.Employees.Add(emp);
            db.SaveChanges();
            return emp.id + "  Succesfully created";
        }

        // PUT api/values/5
        public string Put(int id, [FromBody] dynamic value)
        {
            AdventureWorks2016Entities db = new AdventureWorks2016Entities();
            db.Database.ExecuteSqlCommand($"delete from dbo.Employee WHERE id = {id}");
            Employee emp = JsonConvert.DeserializeObject<Employee>(value.ToString());
            emp.Status = 1;
            db.Employees.Add(emp);
            db.SaveChanges();
            return emp.id + "  Succesfully Updated";
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            AdventureWorks2016Entities db = new AdventureWorks2016Entities();
            db.Database.ExecuteSqlCommand($"UPDATE dbo.Employee SET Status = 0 WHERE id = {id}");
            return id + "  Succesfully Deleted";
        }
    }
}
