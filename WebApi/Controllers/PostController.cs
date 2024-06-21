using Final.Domain.Dto;
using Final.Dto;
using Final.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("Post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();
            return Ok(posts);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> AddPost(AddPostDto addPostDto)
        {
            var userId = GetUserId();

            await _postService.AddPost(addPostDto, userId);
            return Ok();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> DeletePost(int id)
        {
            var userId = GetUserId();
            await _postService.DeletePost(id, userId);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> UpdatePost(UpdatePostDto updatePostDto)
        {
            var userId = GetUserId();
            await _postService.UpdatePost(updatePostDto, userId);
            return Ok();
        }

        [HttpPost("AddComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> AddComment(AddCommentDto addCommentDto)
        {
            var userId = GetUserId();

            await _postService.AddComment(addCommentDto, userId);
            return Ok();
        }

        [HttpPut("UpdateComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> EditComment(UpdateCommentDto updateCommentDto)
        {
            var userId = GetUserId();
            await _postService.UpdateComment(updateCommentDto, userId);
            return Ok();
        }

        [HttpDelete("DeleteComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> DeleteComment(int id)
        {
            var userId = GetUserId();
            await _postService.DeleteComment(id, userId);
            return Ok();
        }
        [HttpGet("PostDetailInfo")]

        public async Task<IActionResult> PostDetailInfo(int postId)
        {
            var post = await _postService.GetPostDetailInfo(postId);
            return Ok(post);
        }

        [HttpPut("ChangeStatus")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> ChangeStatus(ChangePostStatusDto changePostStatusDto)
        {
            await _postService.ChangPostStatus(changePostStatusDto);
            return Ok();
        }

        [HttpPut("ChangeState")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> ChangeState(ChangePostStateDto changePostStateDto)
        {
            await _postService.ChangPostState(changePostStateDto);
            return Ok();
        }
        private string GetUserId()
        {
            return User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString();
        }
    }
}
