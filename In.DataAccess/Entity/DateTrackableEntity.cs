using System;

namespace In.DataAccess.Entity
{
    public abstract class DateTrackableEntity : DateTrackableEntity<int>
    {
    }

    public abstract class DateTrackableEntity<TKey> : HasKeyBase<TKey>
    {
        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}