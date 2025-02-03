using BluckImport.Core.ClsResponce;
using Dapper;
using Microsoft.Extensions.Configuration;
using mockinterview.core.common.import;
using mockinterview.core.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mockinterview.infrastructure
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IConfiguration _configuration;
        private static string con = string.Empty;
        public QuestionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            con = _configuration["ConnectionStrings:DataConnect"]!;
        }
        public static IDbConnection connection
        {
            get
            {
                return new SqlConnection(con);
            }
        }

        public async Task<Response<QuestionClass>> QuestionList(QuestionTypeRequest questionType)
        {
            Response<QuestionClass> response = new();
            using (IDbConnection db = connection)
            {
                var result = await db.QueryMultipleAsync("uspQuestionList", new { questionType.QuestionType }).ConfigureAwait(false);

                response = result.Read<Response<QuestionClass>>().FirstOrDefault()!;
                if (response.Status)
                {
                    var data = result.Read<QuestionClass>().ToList();
                    response.Data = data;
                    //response.TotalRecords = response.recordsFiltered = result.Read<int>().FirstOrDefault();
                }
            }
            return response;
        }

        public async Task<Response<QuestionClass>> QuestionGet()
        {
            Response<QuestionClass> response = new();
            using (IDbConnection db = connection)
            {
                var result = await db.QueryMultipleAsync("uspQuestionTypeGet", new { }).ConfigureAwait(false);

                response = result.Read<Response<QuestionClass>>().FirstOrDefault()!;
                if (response.Status)
                {
                    var data = result.Read<QuestionClass>().ToList();
                    response.Data = data;
                    //response.TotalRecords = response.recordsFiltered = result.Read<int>().FirstOrDefault();
                }
            }
            return response;
        }
    }
}
