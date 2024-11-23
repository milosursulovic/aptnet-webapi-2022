﻿using PokemonReviewApp.Data;
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

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }
    }
}
