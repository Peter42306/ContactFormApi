using ContactFormApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Interfaces.Repositories
{
    public interface IFeedbackMessageRepository
    {
        Task AddAsync(
            FeedbackMessage message,
            CancellationToken ct = default);

        Task UpdateAsync(
            FeedbackMessage message,
            CancellationToken ct = default);
    }
}
