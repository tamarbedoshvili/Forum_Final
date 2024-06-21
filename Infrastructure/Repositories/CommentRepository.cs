using Application.Interfaces;
using Final.database;
using Final.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        private readonly DatabaseContext _context;
        public CommentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment Comment)
        {
            await _context.Comments.AddAsync(Comment);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteComment(int id)
        {
            var Comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(Comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetSingleComment(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _context.Comments.Include(x => x.User).ToListAsync();


        }

        public async Task UpdateComment(Comment Comment)
        {

            _context.Comments.Update(Comment);
            await _context.SaveChangesAsync();
        }
    }
}
