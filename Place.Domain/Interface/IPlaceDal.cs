using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Domain.Models;
using Place.Domain.ViewModels;

namespace Place.Domain.Interface;
public interface IPlaceDal
{
    Task<(List<Domain.Models.Place> data, int totalCount)> GetAllAsync(GetPlaceFilterViewModel viewModel);
    Task<List<Domain.Models.Place>> GetAllSP(GetPlaceFilterViewModel viewModel);
    Task<long> Insert(Domain.Models.Place entity);
    Task<int> Update(Domain.Models.Place entity);
    Task<bool> Delete(long id);
    Task<User> GetById(long id);
}