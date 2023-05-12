using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper){
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet("cities")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var cities = await uow.CityRepository.GetCitiesAsync();
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            // var citiesDto = from c in cities
            //     select new CityDto(){
            //         ID = c.ID,
            //         Name = c.Name
            //     };
            return Ok(citiesDto);
        }
        
        // // Post api/city/add?citname=Miami
        // // Post api/city/add/Los angeles
        // [HttpPost("add")]
        // [HttpPost("add/{cityname}")]
        // public async Task<IActionResult> AddCity(string cityName)
        // {
        //     City city = new City();
        //     city.Name = cityName;
        //     await dc.Cities.AddAsync(city);
        //     await dc.SaveChangesAsync();
        //     return Ok(city);
        // }

        // Post api/city/post - post data in JSON format
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {   
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;
            // var city = new City {
            //     Name = cityDto.Name,
            //     LastUpdatedBy = 1,
            //     LastUpdatedOn = DateTime.Now
            // };
            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            try{
                if(id != cityDto.ID)
                return BadRequest("Update not allowed");

                var cityFromDB = await uow.CityRepository.FindCity(id);

                if(cityFromDB == null)
                    return BadRequest("Update not allowed");

                cityFromDB.LastUpdatedBy = 1;
                cityFromDB.LastUpdatedOn = DateTime.Now;
                mapper.Map(cityDto, cityFromDB);
                await uow.SaveAsync();

                return StatusCode(201);
            } catch {
                return StatusCode(500, "Some unknown error occurred");
            }

        }

        [HttpPut("updateCityName/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDto cityDto)
        { 
            var cityFromDB = await uow.CityRepository.FindCity(id);
            cityFromDB.LastUpdatedBy = 1;
            cityFromDB.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDB);
            await uow.SaveAsync();

            return StatusCode(201);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        {
            var cityFromDB = await uow.CityRepository.FindCity(id);
            cityFromDB.LastUpdatedBy = 1;
            cityFromDB.LastUpdatedOn = DateTime.Now;

            cityToPatch.ApplyTo(cityFromDB, ModelState);
            await uow.SaveAsync();

            return StatusCode(201);
        }
        
        // Delete api/city/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
