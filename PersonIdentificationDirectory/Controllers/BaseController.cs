using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonIdentificationDirectory.API.Filters;

namespace PersonIdentificationDirectory.API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    [ValidateModel]
    public abstract class BaseController : ControllerBase
    {
        protected ISender Sender;

        public BaseController(ISender sender)
        {
            Sender = sender;
        }
    }
}
