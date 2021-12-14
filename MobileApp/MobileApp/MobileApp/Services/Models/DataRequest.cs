using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Models
{
    public class DataRequest
    {
        public bool IsSuccessful { get; internal set; }
    }

    public class DataRequest<TEntity> : DataRequest where TEntity : class
    {
        public TEntity Result { get; internal set; }
    }
}
