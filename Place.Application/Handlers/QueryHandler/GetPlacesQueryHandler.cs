using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Configuration.Queries;
using Place.Application.Configuration.Queries;
using Place.Application.Helper;
using Place.Application.Queries;
using Place.Domain.Interface;
using Place.Domain.ViewModels;

namespace Place.Application.Handlers.QueryHandler;

public class GetPlacesQueryHandler : IQueryHandler<GetPlacesQuery<GetPlaceFilterViewModel>, QueryResult>
{
    private readonly IPlaceDal _QueryRepository;

    public GetPlacesQueryHandler(IPlaceDal customerQueryRepository)
    {
        _QueryRepository = customerQueryRepository;
    }

    public async Task<QueryResult> Handle(GetPlacesQuery<GetPlaceFilterViewModel> request, CancellationToken cancellationToken)
    {
        var result = await _QueryRepository.GetAllSP(request.Data);
        if (result == null)
            return new QueryResult("There is not any data.");
        return new QueryResult(result);
    }
}