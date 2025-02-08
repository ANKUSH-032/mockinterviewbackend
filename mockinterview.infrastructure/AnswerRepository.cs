using BluckImport.Core.ClsResponce;
using BluckImport.Core.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using mockinterview.core.Interface;
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
        public Task<Response> AddAnswer(AnswerInsert answerInsert)
        {
            using (IDbConnection db = connection)
            {
                // Create a DataTable to match the TVP structure
                var tvp = new DataTable();
                tvp.Columns.Add("Question", typeof(string));
                tvp.Columns.Add("QuestionType", typeof(string));

                // Populate the DataTable with data from personList
                foreach (var question in answerInsert.InsertAnswerType!)
                {

                    tvp.Rows.Add(question.Question, question.QuestionType);
                }

                // Define a dynamic parameter to pass the TVP
                var parameters = new DynamicParameters();
                parameters.Add("BulkQuestionData", tvp.AsTableValuedParameter("bulkQuestionInsert"));
                parameters.Add("CreatedBy", "Ankush");
                // Call the stored procedure
                var affectedRows = await db.QueryMultipleAsync("[dbo].[uspQuestionInsert]", parameters, commandType: CommandType.StoredProcedure);

                // Update response based on the result

                Response<InsertQuestion> response = affectedRows.Read<Response<InsertQuestion>>().FirstOrDefault()!;

                if (response != null && !response.Status)
                {
                    var data = affectedRows.Read<InsertQuestion>().ToList();
                    response.Data = data;
                    return response;
                }
                else
                {
                    response!.Status = true;
                    response.Message = "Insert Successfully";
                    return response;
                }

            }
        }
    }
}
