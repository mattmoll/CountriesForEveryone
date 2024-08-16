using CountriesForEveryone.Core.Entities;
using CountriesForEveryone.Core.Repositories;
using CountriesForEveryone.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CountriesForEveryone.Repository.Repositories
{
    public class LanguageRepository : BaseRepository, ILanguageRepository
    {
        private readonly CountriesForEveryoneContext _context;

        public LanguageRepository(CountriesForEveryoneContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task InsertAll(IEnumerable<Language> languagesToUpsert)
        {
            if (languagesToUpsert == null) throw new ArgumentNullException(nameof(languagesToUpsert));

            foreach (var language in languagesToUpsert)
            {
                var existingLanguage = await _context.Languages.FirstOrDefaultAsync(r => r.Name == language.Name);

                if (existingLanguage == null)
                {
                    language.Id = Guid.NewGuid();
                    _context.Languages.Add(language);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Language>> GetAll()
        {
            return await _context.Languages.ToListAsync();
        }
    }
}
