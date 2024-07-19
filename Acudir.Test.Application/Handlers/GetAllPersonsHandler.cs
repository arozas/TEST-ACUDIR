using System.Collections;
using Acudir.Test.Application.Mappers;
using Acudir.Test.Application.Queries;
using Acudir.Test.Application.Responses;
using Acudir.Test.Core.Entities;
using Acudir.Test.Core.Interfaces;
using AutoMapper;
using MediatR;

namespace Acudir.Test.Application.Handlers;

public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsQuery, IList<PersonResponse>>
{
    private readonly IPersonRepository _personRepository;
    
    public GetAllPersonsHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    public Task<IList<PersonResponse>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var personList = _personRepository.GetAll();
        var personResponseList = LazyMapper.Mapper.Map<IList<PersonResponse>>(personList);
        return Task.FromResult(personResponseList);
    }
}