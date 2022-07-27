using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Configuration.Queries;
using Place.Application.Configuration.Queries;

namespace Place.Application.Queries;

public class GetPlacesQuery<T> : QueryBase<QueryResult> where T : class
{
    public T Data { get; set; }
    public GetPlacesQuery(T data)
    {
        Data = data;
    }
}
