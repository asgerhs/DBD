using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment_1.DataModels;
using assignment_1.Persistent;
using assignment_1.Persistent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace assignment_1.Controllers
{
    [ApiController]
    [Route("/address")]
    public class AddressesController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly AddressRepository addressRepository;

        private readonly CityRepository cityRepository;
        
        public AddressesController (
            UnitOfWork unitOfWork,
            AddressRepository addressRepository,
            CityRepository cityRepository
        ){
            this.unitOfWork = unitOfWork;
            this.addressRepository = addressRepository;
            this.cityRepository = cityRepository;
        }
        [HttpGet("get/id")]
        public async Task<ActionResult<Address>> GetAddress(long id){        
           var address = await this.addressRepository.GetAsync(id, true);
           return Ok(address);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Address>> AddAddress([FromBody]Address address){    
            var city = await this.cityRepository.GetByZipAsync(address.city.zip);
            if(city == null)
                city = new City {
                    zip = address.city.zip,
                    city = address.city.city
                };
            
            Address newAddress = new Address {
                street = address.street,
                housenumber = address.housenumber,
                city = city
            };

            this.addressRepository.Add(newAddress);
            await this.unitOfWork.CompleteAsync();

            return Ok(newAddress);
        }
    }
}
