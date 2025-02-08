using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mockinterview.core.Model
{
    public class Answer
    {
        public class InsertAnswerType
        {
            public string? Question { get; set; }
            public string? QuestionType { get; set; }
        }
        public class AnswerInsert
        {
            public string? CandidateID { get; set; }
            public long NumberOfInterviewID { get; set; }
            public InsertAnswerType? InsertAnswerType { get; set; }
        }
    }
}
