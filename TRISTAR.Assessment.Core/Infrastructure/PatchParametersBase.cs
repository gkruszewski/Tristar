using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TRISTAR.Assessment.Infrastructure
{
    /// <summary>
    /// Base class for parameters that can patch other objects.
    /// </summary>
    public class PatchParametersBase : ITrackChanges
    {
        private readonly HashSet<string> _changes;

        protected PatchParametersBase()
        {
            _changes = new HashSet<string>();
        }

        public IEnumerable<string> GetChangedProperties()
        {
            return _changes;
        }

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!_changes.Contains(propertyName))
                _changes.Add(propertyName);
            field = value;
        }
    }
}
