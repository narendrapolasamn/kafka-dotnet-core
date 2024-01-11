using Post.Common.DTOs;

namespace Post.Cmd.Api.DTOs
{
    public record class NewPostResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
