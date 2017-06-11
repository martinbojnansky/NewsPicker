using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Robot.Models.Facebook
{
    [DataContract]
    public class OpenGraphEngagement
    {
        [DataMember(Name = "reaction_count")]
        public int ReactionCount { get; set; } = 0;

        [DataMember(Name = "engagement_count")]
        public int EngagementCount { get; set; } = 0;

        [DataMember(Name = "comment_count")]
        public int CommentCount { get; set; } = 0;

        public int TotalEngagementCount => ReactionCount + EngagementCount + CommentCount;
    }
}