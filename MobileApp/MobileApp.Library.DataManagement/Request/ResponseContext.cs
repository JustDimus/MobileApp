using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.Request
{
    public class ResponseContext
    {
        public bool IsSuccessful { get; set; }

        public static implicit operator bool(ResponseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.IsSuccessful;
        }
        
        public static implicit operator ResponseContext(bool result)
        {
            return new ResponseContext()
            {
                IsSuccessful = result
            };
        }
    }

    public class ResponseContext<TEntity> : ResponseContext where TEntity : class
    {
        public TEntity Result { get; set; }

        public string RawBodyResponse { get; set; }

        public static ResponseContext<TEntity> FromResult(TEntity entity)
        {
            return new ResponseContext<TEntity>()
            {
                IsSuccessful = true,
                Result = entity
            };
        }

        public static ResponseContext<TEntity> FromStatus(bool status)
        {
            return new ResponseContext<TEntity>()
            {
                IsSuccessful = status
            };
        }

        public ResponseContext<TEntity> AddRawBodyData(string body)
        {
            this.RawBodyResponse = body;
            return this;
        }
    }
}
