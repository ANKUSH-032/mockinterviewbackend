using BluckImport.Core.ClsResponce;
using Microsoft.Extensions.Configuration;
using mockinterview.core.common;
using mockinterview.core.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mockinterview.core.Model;
using Dapper;

namespace mockinterview.infrastructure
{
    public class CandidateRepository :ICandidateRepository
    {
        private static string _con = string.Empty;
        private static IConfigurationRoot? _iconfiguration;
        public CandidateRepository()    
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();
            _con = _iconfiguration["ConnectionStrings:DataConnect"]!;
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_con);
            }
        }
        public async Task<Responce> CandidateInsert(CandidateInsert CandidateInsert)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspCandidateInsert]", new
            {
                CandidateInsert.FirstName,
                CandidateInsert.LastName,
                CandidateInsert.Email,
                CandidateInsert.ContactNo,
                CandidateInsert.Address,
                CandidateInsert.RoleID,
                CandidateInsert.ZipCode,
                CandidateInsert.Gender,
                CandidateInsert.PasswordHash,
                CandidateInsert.PasswordSalt,
                CandidateInsert.CreatedBy
            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            Responce? response = result.FirstOrDefault();

            return response!;
        }

        public async Task<ClsResponse<CandidateList>> CandidateList(JqueryDataTable jqueryDataTable)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspCandidateGetList]", new
            {
                jqueryDataTable.Start,
                jqueryDataTable.SortCol,
                jqueryDataTable.PageSize,
                jqueryDataTable.SearchKey
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<CandidateList>? responce = result.Read<ClsResponse<CandidateList>>().FirstOrDefault();
            if (responce!.Status)
            {
                responce.Data = result.Read<CandidateList>().ToList();
                responce.TotalRecords = result.Read<int>().FirstOrDefault();
            }
            return responce;
        }

        public async Task<Responce> CandidateUpdate(CandidateUpdate CandidateUpdate)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspCandidateUpdate]", new
            {
                CandidateUpdate.FirstName,
                CandidateUpdate.LastName,
                CandidateUpdate.Email,
                CandidateUpdate.Address,
                CandidateUpdate.CandidateId,
                CandidateUpdate.Gender,
                CandidateUpdate.ContactNo,
                CandidateUpdate.ZipCode,

            }, commandType: CommandType.StoredProcedure);
            Responce? responce = result.FirstOrDefault();

            return responce!;
        }

        public async Task<ClsResponse<Candidate>> CandidateGet(string CandidateId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspCandidateGetDetails]", new
            {
                CandidateId
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<Candidate>? responce = result.Read<ClsResponse<Candidate>>().FirstOrDefault();

            if (responce!.Status)
            {
                responce.Data = result.Read<Candidate>().ToList();

            }
            return responce;

        }
        public async Task<Responce> CandidateDelete(string CandidateId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspCandidateDelete]", new
            {
                CandidateId
            }, commandType: CommandType.StoredProcedure);

            Responce? responce = result.FirstOrDefault();


            return responce!;

        }
    }
}
