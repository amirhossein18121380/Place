using Dapper;
using Place.Domain.Interface;
using Place.Domain.Models;
using Place.Infrastructure.Tools;

namespace Place.Infrastructure.DAL;


public class UserDal : IUserDal
{
    #region DataMembe
    private const string TbName = "[dbo].[User]";
    #endregion


    //this is not async
    public User? GetById(long id)
    {
        using var connection = new DbEntityObject().GetConnectionString();
        var result =  connection.Query<User>($@"Select * From {TbName} WHERE Id=@id ", new { id });
        return result.SingleOrDefault();
    }

    public async Task<User?> GetByUserName(string UserName)
    {
        await using var connection = new DbEntityObject().GetConnectionString();
        var result = await connection.QueryAsync<User>($@"Select * From {TbName} WHERE UserName=@UserName ", new { UserName = UserName });
        return result.SingleOrDefault();
    }

    #region insert
    public async Task<long> Insert(Domain.Models.User entity)
    {
        await using var connection = new DbEntityObject().GetConnectionString();

        var prams = new DynamicParameters();
        prams.Add("@Name", entity.Name);
        prams.Add("@UserName", entity.UserName);
        prams.Add("@Password", entity.Password);
        prams.Add("@IsActive", entity.IsActive);
        prams.Add("@RegisterDate", entity.RegisterDate);

        var entityId = (await connection.QueryAsync<long>(
            $@"INSERT INTO {TbName} 
                               (
                                       Name
                                      ,UserName
                                      ,Password
                                      ,IsActive
                                      ,RegisterDate
                               )
                               VALUES
                               (
                                       @Name
                                      ,@UserName
                                      ,@Password
                                      ,@IsActive
                                      ,@RegisterDate           
                               );
                               SELECT CAST(SCOPE_IDENTITY() as BIGINT);", prams)).SingleOrDefault();

        return entityId;
    }
    #endregion
}



