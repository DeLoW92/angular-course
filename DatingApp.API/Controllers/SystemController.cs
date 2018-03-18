using System;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models;
using DatingApp.API.Interfaces;


namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    public class SystemController
    {
        private readonly IValueRepository _ValueRepository;

        public SystemController(IValueRepository ValueRepository)
        {
            _ValueRepository = ValueRepository;
        }

        // Call an initialization - api/system/init
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "init")
            {
                _ValueRepository.RemoveAllValues();
                _ValueRepository.AddValue(new Value()
                {
                    Id = "1",
                    Body = "Test Value 1",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    
                });
                _ValueRepository.AddValue(new Value()
                {
                    Id = "2",
                    Body = "Test Value 2",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    
                });
                _ValueRepository.AddValue(new Value()
                {
                    Id = "3",
                    Body = "Test Value 3",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                  
                });
                _ValueRepository.AddValue(new Value()
                {
                    Id = "4",
                    Body = "Test Value 4",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                  
                });

                return "Done";
            }

            return "Unknown";
        }
    }
}