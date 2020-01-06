using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAD.Model;
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
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardController(ICardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public List<CardVM> GetAll()
        {
            return _mapper.Map<List<CardVM>>(_cardService
                .GetAll()
                .ToList());
        }
        [HttpPost]
        [Authorize]
        public IActionResult Insert(CardVM card)
        {
            var c = _mapper.Map<Card>(card);
            _cardService.Add(c);
            return Ok(c.Id);
        }

        [HttpPost("block/{id}")]
        public IActionResult Block(string id)
        {
            _cardService.Block(id);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(CardVM card)
        {
            _cardService.Update(_mapper.Map<Card>(card));
            return Ok();
        }
    }
}
