using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Domain.Enums
{
    public enum FeedbackStatus
    {
        New = 1,
        Reviewed = 2,
        InProgress = 3,
        Resolved = 4,
        Dismissed = 5
    }
}
