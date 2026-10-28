using Microsoft.EntityFrameworkCore;
using PersonService.Data;
using PersonService.Entities;

namespace PersonService.Repository
{
    public class PersonRepository : IPersonRepository
    {

        private readonly PersonDbContext _context;

        public PersonRepository(PersonDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public Task<List<Person>> GetAllAsync()
        {
            return _context.Persons.ToListAsync();
        }

        public Task<Person?> GetByIdAsync(Guid id)
        {
            return _context.Persons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PersonEmbedding>> GetEmbeddingsByPersonIdAsync( Guid personId, CancellationToken cancellationToken)
        {
            return await _context.Set<PersonEmbedding>()
                .AsNoTracking()
                .Where(e => e.PersonId == personId)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}