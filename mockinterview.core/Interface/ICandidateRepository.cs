using BluckImport.Core.ClsResponce;
using mockinterview.core.common;
using mockinterview.core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mockinterview.core.Interface
{
    public interface ICandidateRepository
    {
        Task<Responce> CandidateInsert(CandidateInsert CandidateInsert);
        Task<ClsResponse<CandidateList>> CandidateList(JqueryDataTable jqueryDataTable);
        Task<Responce> CandidateUpdate(CandidateUpdate CandidateUpdate);
        Task<ClsResponse<Candidate>> CandidateGet(string CandidateId);
        Task<Responce> CandidateDelete(string CandidateId);
    }
}
