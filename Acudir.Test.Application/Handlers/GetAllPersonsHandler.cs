using System.Collections;
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
    private readonly IMapper _mapper;
    
    public GetAllPersonsHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    
    public Task<IList<PersonResponse>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var PersonList = _personRepository.GetAll();
        var PersonResponseList = _mapper.Map<IList<PersonResponse>>(PersonList);
        return Task.FromResult(PersonResponseList);
    }
}