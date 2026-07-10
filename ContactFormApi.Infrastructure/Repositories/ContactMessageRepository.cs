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
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(ContactMessage message, CancellationToken ct = default)
        {
            await _context.ContactMessages.AddAsync(message, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(ContactMessage message, CancellationToken ct = default)
        {
            _context.ContactMessages.Update(message);
            await _context.SaveChangesAsync(ct);
        }
    }
}
