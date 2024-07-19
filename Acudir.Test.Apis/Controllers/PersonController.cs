using System.Net;
using Acudir.Test.Application.Queries;
using Acudir.Test.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Acudir.Test.Apis.Controllers;

public class PersonController: ApiController
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("[action]/{id}")]
    [ProducesResponseType(typeof(PersonResponse),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<PersonResponse>> GetPersonById(int id)
    {
        var query = new GetPersonByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}