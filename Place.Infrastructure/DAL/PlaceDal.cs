
using System.Data;
using Dapper;
using Place.Domain.Interface;
using Place.Domain.Models;
using Place.Domain.ViewModels;
using Place.Infrastructure.Tools;

namespace Place.Infrastructure.DAL;


public class PlaceDal : IPlaceDal
{
    #region DataMembe
    private const string TbName = "[dbo].[Place]";
    #endregion

    #region Fetch
    public async Task<(List<Domain.Models.Place> data, int totalCount)> GetAllAsync(GetPlaceFilterViewModel viewModel)
    {
        try
        {
            await using var connection = new DbEntityObject().GetConnectionString();

            int maxPagSize = 50;
            int pageSize = (viewModel.PageSize > 0 && viewModel.PageSize <= maxPagSize) ? viewModel.PageSize : maxPagSize;
            int skip = (viewModel.CurrentPageNumber - 1) * pageSize;
            int take = pageSize;


            #region Set Where Param
            var prams = new DynamicParameters();
            var whereQuery = string.Empty;


            if (!string.IsNullOrEmpty(viewModel?.Title?.Trim()))
            {
                whereQuery += @"AND LOWER(u.Title) LIKE @Title ";
                prams.Add("Title", $"%{viewModel.Title.Trim().ToLower()}%");
            }

            if (viewModel?.PlaceType != null)
            {
                whereQuery += @"AND u.PlaceType LIKE @PlaceType ";
                prams.Add("ProductCode", $"%{viewModel.PlaceType}%");
            }

            #endregion
            whereQuery = whereQuery.StartsWith("AND") ? $"WHERE{whereQuery.Substring(3, whereQuery.Length - 3)}" : whereQuery;
            var query = ($@"SELECT u.Id,u.Title,u.Address,u.PlaceType,u.Location,u.Date,u.UserId FROM {TbName} As u 
                                {whereQuery}
                                ORDER BY u.Id DESC OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY 
                                SELECT COUNT(1) FROM {TbName} u
                                {whereQuery}");


            using var reader = await connection.QueryMultipleAsync(query, prams);
            var data = (await reader.ReadAsync<Domain.Models.Place>()).ToList();
            var totalCount = (await reader.ReadAsync<int>()).FirstOrDefault();

            return (data, totalCount);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message, exp);
        }
    }

    public async Task<List<Domain.Models.Place>> GetAllSP(GetPlaceFilterViewModel viewModel)
    {
        try
        {
            await using var connection = new DbEntityObject().GetConnectionString();
            var totalCount = 0;
            var prams = new DynamicParameters();
            prams.Add("placeType", viewModel.PlaceType);
            prams.Add("Title", viewModel.Title);
            prams.Add("PageIndex", viewModel.CurrentPageNumber);
            prams.Add("PageSize", viewModel.PageSize);
            prams.Add("RecordCount", totalCount);

            var user = connection.Query<Domain.Models.Place>("GetPlaces", prams,
                commandType: CommandType.StoredProcedure).ToList();

            return user;
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message, exp);
        }
    }

    public async Task<User> GetById(long id)
    {
        await using var connection = new DbEntityObject().GetConnectionString();
        var result = await connection.QueryAsync<User>($@"Select * From {TbName} WHERE Id=@id ", new { id });
        return result.SingleOrDefault();
    }

    #endregion

    #region insert
    public async Task<long> Insert(Domain.Models.Place entity)
    {
        await using var connection = new DbEntityObject().GetConnectionString();

        var prams = new DynamicParameters();
        prams.Add("@Title", entity.Title);
        prams.Add("@Address", entity.Address);
        prams.Add("@PlaceType", entity.PlaceType);
        prams.Add("@Location", entity.Location);
        prams.Add("@Date", entity.Date);
        prams.Add("@UserId", entity.UserId);

        var entityId = (await connection.QueryAsync<long>(
            $@"INSERT INTO {TbName} 
                               (
                                       Title
                                      ,Address
                                      ,PlaceType
                                      ,Location
                                      ,Date
                                      ,UserId
                               )
                               VALUES
                               (
                                       @Title
                                      ,@Address
                                      ,@PlaceType
                                      ,@Location
                                      ,@Date
                                      ,@UserId      
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

        return entityId;
    }
    #endregion

    #region Update
    public async Task<int> Update(Domain.Models.Place entity)
    {
        await using var connection = new DbEntityObject().GetConnectionString();

        var sqlQuery = $@"UPDATE {TbName} 
                                   SET 
                                        Title = @Title
                                       ,Address = @Address                                     
                                       ,PlaceType = @PlaceType                                   
                                       ,Location = @Location                                   
                                       ,Date = @Date                                   
                                       ,UserId = @UserId                                                                   
                                   WHERE Id = @Id";

        var rowsAffected = await connection.ExecuteAsync(sqlQuery, new
        {
            entity.Title,
            entity.Address,
            entity.PlaceType,
            entity.Location,
            entity.Date,
            entity.UserId,
            entity.Id
        });

        return rowsAffected;
    }
    #endregion

    #region Delete
    public async Task<bool> Delete(long id)
    {
        await using var connection = new DbEntityObject().GetConnectionString();

        var sqlQuery = $@"DELETE FROM {TbName} WHERE Id = @Id";
        var rowsCount = await connection.ExecuteAsync(sqlQuery, new { id });
        return rowsCount > 0;
    }
    #endregion

}