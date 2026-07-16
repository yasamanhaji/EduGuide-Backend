using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.Entities.Common;
using EduGuide.Domain.Enums;

namespace EduGuide.Domain.Entities
{
    public class RequestCounselor:BaseEntity
    {
        public RequestStatus status {  get; set; } 
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long CounselorId { get; set; }
        public Counselor Counselor { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
  