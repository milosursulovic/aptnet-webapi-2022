using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public Owner GetOwner(int id)
        {
            return _context
                .Owners
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _context
                .PokemonOwners
                .Where(p => p.Pokemon.Id == pokeId)
                .Select(o => o.Owner)
                .ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context
                .Owners
                .ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int id)
        {
            return _context
                .PokemonOwners
                .Where(p => p.Owner.Id == id)
                .Select(p => p.Pokemon)
                .ToList();
        }

        public bool OwnerExists(int id)
        {
            return _context
                .Owners
                .Any(o => o.Id == id);
        }
    }
}
