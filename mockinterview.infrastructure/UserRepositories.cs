using BluckImport.Core.ClsResponce;
using Microsoft.Extensions.Configuration;
using mockinterview.core.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mockinterview.core.common;
using Dapper;

namespace mockinterview.infrastructure
{
    public class UserRepositories : IUserInterface
    {
        private static string _con = string.Empty;
        private static IConfigurationRoot? _iconfiguration;
        public UserRepositories()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();
            _con = _iconfiguration["ConnectionStrings:DataAccessConnection"]!;
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_con);
            }
        }
        public async Task<Responce> UserInsert(UserInsert userInsert)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspUserInsert]", new
            {
                userInsert.FirstName,
                userInsert.LastName,
                userInsert.Email,
                userInsert.ContactNo,
                userInsert.Address,
                userInsert.HospitalName,
                userInsert.RoleID,
                userInsert.ZipCode,
                userInsert.Gender,
                userInsert.PasswordHash,
                userInsert.PasswordSalt,
                userInsert.CreatedBy
            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            Responce? response = result.FirstOrDefault();

            return response!;
        }

        public async Task<ClsResponse<UserList>> UserList(JqueryDataTable jqueryDataTable)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspUserGetList]", new
            {
                jqueryDataTable.Start,
                jqueryDataTable.SortCol,
                jqueryDataTable.PageSize,
                jqueryDataTable.SearchKey
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<UserList>? responce = result.Read<ClsResponse<UserList>>().FirstOrDefault();
            if (responce!.Status)
            {
                responce.Data = result.Read<UserList>().ToList();
                responce.TotalRecords = result.Read<int>().FirstOrDefault();
            }
            return responce;
        }

        public async Task<Responce> UserUpdate(UserUpdate userUpdate)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspUserUpdate]", new
            {
                userUpdate.FirstName,
                userUpdate.LastName,
                userUpdate.Email,
                userUpdate.Address,
                userUpdate.HospitalName,
                userUpdate.UserId,
                userUpdate.Gender,
                userUpdate.ContactNo,
                userUpdate.ZipCode,

            }, commandType: CommandType.StoredProcedure);
            Responce? responce = result.FirstOrDefault();

            return responce!;
        }

        public async Task<ClsResponse<User>> UserGet(string UserId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspUserGetDetails]", new
            {
                UserId
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<User>? responce = result.Read<ClsResponse<User>>().FirstOrDefault();

            if (responce!.Status)
            {
                responce.Data = result.Read<User>().ToList();

            }
            return responce;

        }
        public async Task<Responce> UserDelete(string UserId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspUserDelete]", new
            {
                UserId
            }, commandType: CommandType.StoredProcedure);

            Responce? responce = result.FirstOrDefault();


            return responce!;

        }
    }
}
