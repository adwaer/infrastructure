using System;

namespace In.Domain
{
    public interface ITimeTrackable
    {
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
        string ImpersonalId { get; set; }
    }
}
