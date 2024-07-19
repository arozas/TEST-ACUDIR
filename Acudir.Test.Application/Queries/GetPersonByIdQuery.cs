using Acudir.Test.Application.Responses;
using MediatR;

namespace Acudir.Test.Application.Queries;

public class GetPersonByIdQuery:IRequest<PersonResponse>
{
    public int Id { get; set; }

    public GetPersonByIdQuery(int id)
    {
        Id = id;
    }
}