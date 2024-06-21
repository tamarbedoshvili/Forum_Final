using Final.Domain.Dto;
using Final.Dto;

namespace Final.Interfaces
{
    public interface IPostService
    {
        public Task<List<PostDto>> GetPosts();
        public Task<List<PostDto>> GetPostDetailInfo(int postId);


        public Task AddPost(AddPostDto post, string userId);
        public Task AddComment(AddCommentDto comment, string userId);
        public Task UpdateComment(UpdateCommentDto comment, string userId);

        public Task DeletePost(int id, string userId);
        public Task DeleteComment(int id, string userId);

        public Task UpdatePost(UpdatePostDto Post, string userId);
        public Task ChangPostStatus(ChangePostStatusDto Post);
        public Task ChangPostState(ChangePostStateDto Post);


    }
}
