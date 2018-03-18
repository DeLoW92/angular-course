using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using DatingApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IValueRepository _valueRepository;

    public ValuesController(IValueRepository valueRepository)
    {
        _valueRepository = valueRepository;
    }

    [AllowAnonymous]
    [NoCache]
    [HttpGet]
    public async Task<IEnumerable<Value>> Get() //Se utilizan asyn methods porque basicamente son iguales en rendimiento y no bloquean la aplicacion.
    {
        return await _valueRepository.GetAllValues();
    }



    // GET api/values/5 - retrieves a specific value using either Id or InternalId (BSonId)
    [HttpGet("{id}")]
    public async Task<Value> Get(string id)
    {
        return await _valueRepository.GetValue(id) ?? new Value();
    }

    // POST api/values - creates a new value
    [HttpPost]
    public void Post([FromBody] ValueParam newvalue)
    {
        _valueRepository.AddValue(new Value
                                    {                                    
                                        Id = newvalue.Id,
                                        Body = newvalue.Body,
                                      //  InternalId = newvalue.InternalId,
                                      //  Name = newvalue.Name,
                                      //  UpdatedOn = newvalue.UpdatedOn,
                                      //  CreatedOn = newvalue.CreatedOn
       
                                    });       
        
       
      
                        
    }

    // PUT api/values/5 - updates a specific value
    [HttpPut("{id}")]
    public void Put(string id, [FromBody]string value)
    {
        _valueRepository.UpdateValueDocument(id, value);
    }

    // DELETE api/values/5 - deletes a specific value
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        _valueRepository.RemoveValue(id);
    }
    }
}