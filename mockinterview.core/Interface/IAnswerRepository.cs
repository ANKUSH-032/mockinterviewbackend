using BluckImport.Core.ClsResponce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static mockinterview.core.Model.Answer;

namespace mockinterview.core.Interface
{
    public interface IAnswerRepository
    {
        Task<Response> AddAnswer(AnswerInsert answerInsert);
    }
}
