using Final.Entities;

namespace Application.Interfaces
{
    public interface IPostRepository
    {
        public Task<List<Post>> GetPosts();
        public Task<Post> GetSinglePost(int id);

        public Task AddPost(Post post);

        public Task DeletePost(int id);

        public Task UpdatePost(Post Post);



    }
}
