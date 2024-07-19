using MediatR;

namespace Acudir.Test.Application.Commands;

public class DeletePersonByIdCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeletePersonByIdCommand(int id)
    {
        Id = id;
    }
}