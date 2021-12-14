using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Library.DataManagement.RequestManagement
{
    public interface IRequestManager
    {
        Task<ResponseContext> SendRequestAsync(BaseApplicationRequest request);

        Task<ResponseContext<TEntity>> SendRequestAsync<TEntity>(BaseApplicationRequest request) where TEntity : class;
    }
}
