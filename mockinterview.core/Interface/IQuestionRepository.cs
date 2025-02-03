using BluckImport.Core.ClsResponce;
using mockinterview.core.common.import;

namespace mockinterview.core.Interface
{
    public interface IQuestionRepository
    {
        Task<Response<QuestionClass>> QuestionList(QuestionTypeRequest questionType);
        Task<Response<QuestionClass>> QuestionGet();
    }
}
