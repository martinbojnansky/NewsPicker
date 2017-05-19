using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Robot.Models.Facebook
{
    [DataContract]
    public class OpenGraphShare
    {
        [DataMember(Name = "comment_count")]
        public int? CommentCount { get; set; }

        [DataMember(Name = "share_count")]
        public int? ShareCount { get; set; }
    }
}
