using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rabbit.Models.Entities;
using Rabbit.Services.Interfaces;

namespace Rabbit.Publisher.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMessageControllers : ControllerBase
    {
        private readonly IRabbitMessageService _service;
        public RabbitMessageControllers(IRabbitMessageService service) {
            _service = service;
        }

        [HttpPost]
        public void AddMensage(RabbitMessage message)
        {
            _service.SendMessage(message);
        }

    }
}
