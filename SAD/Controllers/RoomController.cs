using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SAD.Hubs;
using SAD.Model;
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
        private readonly IAuditLogService _auditLogService;
        private readonly IHubContext<AuditLogHub> _hubContext;

        public RoomController(IRoomService roomService, IMapper mapper, IAuditLogService auditLogService, IHubContext<AuditLogHub> context)
        {
            _roomService = roomService;
            _mapper = mapper;
            _auditLogService = auditLogService;
            _hubContext = context;
        }

        [HttpGet]
        [Authorize]
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
            var result = _roomService.CheckAccess(request) ;
            var auditLog = _auditLogService.Add(
                request.CardSerialNumber, 
                request.RoomNumber, 
                result);
            var vm = _mapper.Map<AuditLogVM>(auditLog);
            _hubContext.Clients.All.SendAsync("live", vm);
            return result ? Ok() : (IActionResult)Unauthorized();
        }
    }
}
