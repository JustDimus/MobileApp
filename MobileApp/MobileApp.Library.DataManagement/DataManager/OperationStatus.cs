using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.DataManager
{
    public class OperationStatus
    {
        public bool IsSuccessful { get; internal set; }
    }

    public class OperationStatus<TEntity> where TEntity : class
    {
        public TEntity Result { get; internal set; }
    }
}
