using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Place.Domain.Interface;
using Place.Domain.Models;
using Place.Infrastructure.Tools;

namespace Place.Infrastructure.DAL;


public class ReservationDal : IReservationDal
{

    #region DataMembe
    private const string TbName = "[dbo].[Reservation]";
    #endregion

    public async Task<List<Reservation>?> GetAll()
    {
        await using var connection = new DbEntityObject().GetConnectionString();
        var result = await connection.QueryAsync<Reservation>($@"Select * From {TbName}");
        return result.ToList();
    }

    #region insert
    public async Task<long> Add(Reservation entity)
    {
        await using var connection = new DbEntityObject().GetConnectionString();

        var prams = new DynamicParameters();
        prams.Add("@UserId", entity.UserId);
        prams.Add("@PlaceId", entity.PlaceId);
        prams.Add("@ReserveTime", entity.ReserveTime);
        prams.Add("@CreateOn", entity.CreateOn);
        prams.Add("@Cost", entity.Cost);

        var entityId = (await connection.QueryAsync<long>(
            $@"INSERT INTO {TbName} 
                               (
                                       UserId
                                      ,PlaceId
                                      ,ReserveTime
                                      ,CreateOn
                                      ,Cost
                               )
                               VALUES
                               (
                                       @UserId
                                      ,@PlaceId
                                      ,@ReserveTime
                                      ,@CreateOn
                                      ,@Cost   
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

        return entityId;
    }
    #endregion
}

