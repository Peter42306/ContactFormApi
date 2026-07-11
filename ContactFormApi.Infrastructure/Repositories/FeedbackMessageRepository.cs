using ContactFormApi.Application.Interfaces.Repositories;
using ContactFormApi.Domain.Entities;
using ContactFormApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Infrastructure.Repositories
{
    public sealed class FeedbackMessageRepository : IFeedbackMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(
            FeedbackMessage message, 
            CancellationToken ct = default)
        {
            await _context.FeedbackMessages.AddAsync(message, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(
            FeedbackMessage message,
            CancellationToken ct = default)
        {
            _context.FeedbackMessages.Update(message);
            await _context.SaveChangesAsync(ct);
        }
    }
}
