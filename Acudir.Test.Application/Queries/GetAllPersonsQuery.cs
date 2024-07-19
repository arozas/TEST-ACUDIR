using Acudir.Test.Application.Responses;
using Acudir.Test.Core.Entities;
using MediatR;

namespace Acudir.Test.Application.Queries;

public class GetAllPersonsQuery : IRequest<IList<PersonResponse>>, IRequest
{
    
}