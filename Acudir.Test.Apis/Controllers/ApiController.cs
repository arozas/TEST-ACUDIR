using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Acudir.Test.Apis.Controllers;

[ApiVersion("1")]
[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    
}