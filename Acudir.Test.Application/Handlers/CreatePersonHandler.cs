using Acudir.Test.Application.Commands;
using Acudir.Test.Application.Mappers;
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

    public CreatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    public async Task<PersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = LazyMapper.Mapper.Map<Person>(request);
        if (person is null)
        {
            throw new ApplicationException("Hay un problema al mapear una nueva persona.");
        }

        var newPerson = await _personRepository.Create(person);
        var personResponse = LazyMapper.Mapper.Map<PersonResponse>(newPerson);
        
        if (personResponse is null)
        {
            throw new ApplicationException("Hay un problema al crear una nueva persona.");
        }
        return personResponse;
    }
}