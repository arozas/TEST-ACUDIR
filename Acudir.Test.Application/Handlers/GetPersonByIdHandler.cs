using System.Data;
using Acudir.Test.Application.Mappers;
using Acudir.Test.Application.Queries;
using Acudir.Test.Application.Responses;
using Acudir.Test.Core.Interfaces;
using AutoMapper;
using MediatR;

namespace Acudir.Test.Application.Handlers;

public class GetPersonByIdHandler: IRequestHandler<GetPersonByIdQuery, PersonResponse>
{

    private readonly IPersonRepository _personRepository;

    public GetPersonByIdHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    public async Task<PersonResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.Get(request.Id);
        
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with ID {request.Id} not found");
        }

        var personResponse = LazyMapper.Mapper.Map<PersonResponse>(person);
        
        return personResponse;
    }
}
