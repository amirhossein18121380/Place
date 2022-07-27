using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Place.Domain.ViewModels;

public class GetPlacesViewModel
{

    public GetPlacesViewModel(int pageSize, int currentPageNumber, string? title, string? placeType)
    {
        PageSize = pageSize;
        CurrentPageNumber = currentPageNumber;
        Title = title;
        PlaceType = placeType;
    }

    public int PageSize { get; set; }
    public int CurrentPageNumber { get; set; }
    public string? Title { get; set; }
    public string? PlaceType { get; set; }
}