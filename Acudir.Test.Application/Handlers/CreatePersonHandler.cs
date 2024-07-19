using Acudir.Test.Application.Commands;
using Acudir.Test.Application.Responses;
using Acudir.Test.Core.Entities;
using Acudir.Test.Core.Interfaces;
using AutoMapper;
using MediatR;

namespace Acudir.Test.Application.Handlers;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, PersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreatePersonHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    
    public Task<PersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);
        if (person is null)
        {
            throw new ApplicationException("Hay un problema al mapear una nueva persona.");
        }

        var newPerson = _personRepository.Create(person);
        var personResponse = _mapper.Map<PersonResponse>(newPerson);
        
        if (personResponse is null)
        {
            throw new ApplicationException("Hay un problema al crear una nueva persona.");
        }
        return Task.FromResult(personResponse);
    }
}