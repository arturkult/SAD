using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAD.FilterParams;
using SAD.Services;
using SAD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/audit-logs")]
    public class AuditLogController : Controller
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public AuditLogController(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Get([FromBody] AuditLogFilterParams filterParams)
        {
            return Ok(_auditLogService.GetAll(filterParams).ProjectTo<AuditLogVM>(_mapper.ConfigurationProvider));
        }
    }
}
