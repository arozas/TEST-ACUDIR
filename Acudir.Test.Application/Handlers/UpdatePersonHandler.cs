using Acudir.Test.Application.Commands;
using Acudir.Test.Core.Entities;
using Acudir.Test.Core.Interfaces;
using AutoMapper;
using MediatR;

namespace Acudir.Test.Application.Handlers;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public UpdatePersonHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
    }
    public Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _personRepository.Update(new Person
        {
            NombreCompleto = request.NombreCompleto,
            Edad = request.Edad,
            Domicilio = request.Domicilio,
            Telefono = request.Telefono,
            Profesion = request.Profesion,
        });

        return person;
    }
}