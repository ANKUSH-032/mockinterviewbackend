using BluckImport.Core.ClsResponce;
using mockinterview.core.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mockinterview.core.Interface
{
    public interface IUserInterface
    {
        Task<Responce> UserInsert(UserInsert userInsert);
        Task<ClsResponse<UserList>> UserList(JqueryDataTable jqueryDataTable);
        Task<Responce> UserUpdate(UserUpdate userUpdate);
        Task<ClsResponse<User>> UserGet(string UserId);
        Task<Responce> UserDelete(string UserId);
    }
}
