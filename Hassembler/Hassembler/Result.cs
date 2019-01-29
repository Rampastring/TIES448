using System;

namespace Hassembler
{
    public class Result
    {
        public Result(object result) => this.result = result;

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

        public object GetResult() => result;
    
        public static Result operator +(Result left, Result right)
        {
            return new Result(left.GetResult<int>() + right.GetResult<int>());
        }

        public static Result operator -(Result left, Result right)
        {
            return new Result(left.GetResult<int>() - right.GetResult<int>());
        }

        public static Result operator *(Result left, Result right)
        {
            return new Result(left.GetResult<int>() * right.GetResult<int>());
        }

        public static Result operator /(Result left, Result right)
        {
            return new Result(left.GetResult<int>() / right.GetResult<int>());
        }

        public static Result operator <(Result left, Result right)
        {
            return new Result(left.GetResult<int>() < right.GetResult<int>());
        }

        public static Result operator >(Result left, Result right)
        {
            return new Result(left.GetResult<int>() > right.GetResult<int>());
        }

        public static Result operator <=(Result left, Result right)
        {
            return new Result(left.GetResult<int>() <= right.GetResult<int>());
        }

        public static Result operator >=(Result left, Result right)
        {
            return new Result(left.GetResult<int>() >= right.GetResult<int>());
        }

        public static Result operator ==(Result left, Result right)
        {
            CheckType(left, right);
            return new Result(left.GetResult().Equals(right.GetResult()));
        }

        public static Result operator !=(Result left, Result right)
        {
            CheckType(left, right);
            return new Result(!left.GetResult().Equals(right.GetResult()));
        }

        private static void CheckType(Result left, Result right)
        {
            if (left.result.GetType().Equals(right.result))
            {
                throw new InvalidOperationException($"Type mismatch: " +
                    $"{left.result.GetType().Name} cannot be compared with {right.result.GetType().Name}");
            }
        }


        public override string ToString() => result.ToString();

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return base.Equals (obj);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}