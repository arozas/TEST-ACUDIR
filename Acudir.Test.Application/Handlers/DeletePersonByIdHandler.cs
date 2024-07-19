using Acudir.Test.Application.Commands;
using Acudir.Test.Core.Interfaces;
using MediatR;

namespace Acudir.Test.Application.Handlers;

public class DeletePersonByIdHandler : IRequestHandler<DeletePersonByIdCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonByIdHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    public Task<bool> Handle(DeletePersonByIdCommand request, CancellationToken cancellationToken)
    {
        var deleteResponse = _personRepository.Delete(request.Id);
        return deleteResponse;
    }
}