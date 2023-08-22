using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Comment.Response
{
    public class CommentResponse
    {
        public CommentResponse(int Id, string Body, int UserId, int TickerId, DateTime CreatedAt, DateTime UpdatedAt)
        {
            this.Id = Id;
            this.Body = Body;
            this.UserId = UserId;
            this.TicketId = TickerId;
            this.CreatedAt = CreatedAt;
            this.UpdatedAt = UpdatedAt;
        }
        public CommentResponse()
        {

        }
        public int Id;
        public string Body;
        public int UserId;
        public int TicketId;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
    }
}