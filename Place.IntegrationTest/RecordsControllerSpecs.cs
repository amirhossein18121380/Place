using MediatR;
using Microsoft.Extensions.Configuration;
using Place.API.Controllers;
using Place.Domain.ViewModels;
using Xunit;

namespace Place.IntegrationTest;

public class RecordsControllerSpecs
{
    private readonly IMediator _md;

    [Fact]
    public void GetAll_Places()
    {
        var configuration = GetConfig();
        var sut = new PlaceController(_md, configuration);
        int pageSize = 10;
        int currentPageNumber = 1;
        string? title = "ww";
        short? placeType = 1;

        var model = new GetPlaceFilterViewModel(pageSize, currentPageNumber, title, placeType);
        var result =  sut.GetPlaces(model);
        Assert.NotNull(result);
    }

    [Fact]
    public void Add_Place()
    {
        var configuration = GetConfig();
        var sut = new PlaceController(_md, configuration);
        var ss = new AddPlaceViewModel();
        ss.PlaceType = 1;
        ss.Address = "22john street";
        ss.Location = "98.65.77.25";
        ss.Title = "Medical Center";

        var result = sut.AddPlace(ss);
 
        Assert.NotNull(result);
    }

    [Fact]
    public void Delete_Place()
    {
        var configuration = GetConfig();
        var sut = new PlaceController(_md, configuration);
        long id = 1;

        var result = sut.Delete(id);

        Assert.NotNull(result);
    }

    [Fact]
    public void Reserve()
    {
        var configuration = GetConfig();
        var sut = new ReserveController(_md, configuration);
        var ss = new ReserveViewModel();
        ss.ReserveTime = DateTime.Now;
        ss.Cost = 100;
        ss.PlaceId = 3;

        var result = sut.Reserve(ss);

        Assert.NotNull(result);
    }

    private IConfiguration GetConfig()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}