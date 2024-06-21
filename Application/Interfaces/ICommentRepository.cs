using Final.Entities;

namespace Application.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetComments();
        public Task<Comment> GetSingleComment(int id);

        public Task AddComment(Comment comment);

        public Task DeleteComment(int id);

        public Task UpdateComment(Comment comment);
    }
}
