using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAD.Services;
using SAD.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace SAD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet]
        public List<RoomVM> GetAll()
        {
            return _roomService
                .GetAll()
                .ProjectTo<RoomVM>(_mapper.ConfigurationProvider)
                .ToList();
        }

        [HttpPost("check")]
        public IActionResult CheckAccess(RequestVM request)
        {
            return _roomService.CheckAccess(request) ? Ok() : (IActionResult)Unauthorized();
        }
    }
}
