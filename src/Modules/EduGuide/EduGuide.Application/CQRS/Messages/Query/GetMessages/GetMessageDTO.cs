using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduGuide.Application.CQRS.Messages
{
    public class GetMessageDTO
    {
        public long Id { get; set; }
        public long ContactId { get; set; }
        public long SenderId { get; set; }
        public List<string> Messages { get; set; }
        public string ContactPicUrl { get; set; }
        public string ContactFullName { get; set; }
    }
}
