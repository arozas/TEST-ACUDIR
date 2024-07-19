using Acudir.Test.Application.Responses;
using MediatR;

namespace Acudir.Test.Application.Queries;

public class GetAllPersonsQuery : IRequest<IList<PersonResponse>>
{
    
}