using System;

namespace Hassembler
{
    class Result
    {

        public Result(object result)
        {
            this.result = result;
        }

        private Type[] typeWhitelist = new Type[]
         {
             typeof(int),
             typeof(bool),
             typeof(float),
             typeof(string),
             typeof(char),
         };

        private object result;
        public T GetResult<T>() 
        {
            if (result.GetType() != typeof(T))
            throw new Exception();

            return (T)result;
        }

    }
}