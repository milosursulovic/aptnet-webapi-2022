﻿using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);

            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int id)
        {
            return _context
                .Reviewers
                .Where (r => r.Id == id)
                .Include(e => e.Reviews)
                .FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context
                .Reviewers
                .ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int id)
        {
            return _context
                .Reviews
                .Where(r => r.Reviewer.Id == id)
                .ToList();
        }

        public bool ReviewerExists(int id)
        {
            return _context
                .Reviewers
                .Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
    }
}
