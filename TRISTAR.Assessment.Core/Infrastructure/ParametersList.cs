using System.Collections.Generic;

namespace TRISTAR.Assessment.Infrastructure
{
    /// <summary>
    /// A subclass of list to which you can conveniently assign a single value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ParametersList<T> : List<T>
    {
        public static implicit operator ParametersList<T>(T value)
        {
            return new ParametersList<T>
            {
                value
            };
        }
    }
}
