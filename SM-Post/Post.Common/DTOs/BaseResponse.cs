using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.DTOs
{
    public record class BaseResponse
    {
        public string Message { get; set; }
    }
}
