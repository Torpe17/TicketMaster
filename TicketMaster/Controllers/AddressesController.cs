using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AddressesController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressGetDTO>>> GetAddresses()
        {
            var addresses = await _unitOfWork.AddressRepository.GetAsync(includedProperties: ["User"]);
            return _mapper.Map<List<AddressGetDTO>>(addresses); 
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressGetDTO>> GetAddress(int id)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(id, includedReferences: ["User"]);

            if (address == null)
            {
                return NotFound();
            }

            return _mapper.Map<AddressGetDTO>(address);
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressPutDTO dto)
        {
            Address? a = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (a == null)
            {
                return NotFound();
            }
            //TODO: floor ha nem stringként van megadva akkor nem dob hibát

            _mapper.Map(dto, a);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(AddressPostDTO address)
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(address.UserId, includedReferences: ["Address"]);
            if (user == null) {
                return BadRequest($"User (id: {address.UserId}) not found");
            }
            
            if (user.Address != null)
            {
                return BadRequest($"Only one address is allowed for a user");
            }
            Address newAddress = _mapper.Map<Address>(address);

            await _unitOfWork.AddressRepository.InsertAsync(newAddress);
            await _unitOfWork.SaveAsync();
            
            return Created();
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if ((await _unitOfWork.AddressRepository.GetByIdAsync(id)) == null)
            {
                return BadRequest($"Address (id: {id}) not found");
            }
            await _unitOfWork.AddressRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
            
            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return _unitOfWork.AddressRepository.GetByIdAsync(id) != null;
        }
    }
}
