using Acudir.Test.Application.Queries;
using Acudir.Test.Application.Responses;
using Acudir.Test.Core.Interfaces;
using AutoMapper;
using MediatR;

namespace Acudir.Test.Application.Handlers;

public class GetPersonByIdHandler: IRequestHandler<GetPersonByIdQuery, PersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonByIdHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    
    public Task<PersonResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = _personRepository.Get(request.Id);
        var personResponse = _mapper.Map<PersonResponse>(person);
        return Task.FromResult(personResponse);
    }
}