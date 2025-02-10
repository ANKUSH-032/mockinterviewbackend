using BluckImport.Core.ClsResponce;
using BluckImport.Core.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using mockinterview.core.common;
using mockinterview.core.Interface;
using Mockinterview.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static mockinterview.core.Model.Answer;

namespace mockinterview.infrastructure
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IConfiguration _configuration;
        private static string con = string.Empty;

        public AnswerRepository(IConfiguration configuration)
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
        public async Task<Response> AddAnswer(AnswerInsert answerInsert)
        {
            IDbConnection db = connection;

            DataTable dtoAnswer;

            if (answerInsert.InsertAnswerType != null)
            {
                dtoAnswer = Authenticates.ConvertToDataTable(answerInsert.InsertAnswerType);
            }
            else
            {
                dtoAnswer = Authenticates.ConvertToDataTable(new List<InsertAnswerType>());
            }
            dtoAnswerAdd dtoQuestionAdd = new dtoAnswerAdd()
            {
                CandidateID = answerInsert.CandidateID,
                InsertAnswerType = dtoAnswer
            };

            var result = await db.QueryAsync<Response>("[dbo].[uspAnswerInsert]", dtoQuestionAdd).ConfigureAwait(false);

            Response? response = result.FirstOrDefault();

            return response!;

        }
    }
}
