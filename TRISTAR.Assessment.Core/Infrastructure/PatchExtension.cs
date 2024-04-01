using System;

namespace TRISTAR.Assessment.Infrastructure
{
    public static class PatchExtension
    {
        /// <summary>
        /// Writes the changes from the <paramref name="source"/> to <paramref name="target"/>.
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void Patch<TFrom, TTo>(this TFrom source, TTo target)
            where TFrom : class, ITrackChanges
            where TTo : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            // We need to get the runtime types of the instances that were provided.
            // We can't use typeof(TFrom) or typeof(TTo) because they might be base types
            // of the actual source or target instances that were provided
            var sourceType = source.GetType();
            var targetType = target.GetType();

            foreach (var change in source.GetChangedProperties())
            {
                var targetProperty = targetType.GetProperty(change);
                if (targetProperty?.CanWrite != true)
                    continue;
                var sourceProperty = sourceType.GetProperty(change);
                if (sourceProperty == null || !targetProperty.CanRead)
                    continue;

                var sourceValue = sourceProperty.GetValue(source, null);
                if (!sourceValue.IsAssignableTo(targetProperty.PropertyType))
                    continue;

                targetProperty.SetValue(target, sourceValue);
            }
        }

        /// <summary>
        /// Determines whether or not an object can be assigned to <paramref name="type"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsAssignableTo(this object obj, Type type)
        {
            if (obj == null && type.IsValueType && Nullable.GetUnderlyingType(type) == null)
                return false;
            if (obj != null && !type.IsAssignableFrom(obj.GetType()))
                return false;
            return true;
        }
    }
}
