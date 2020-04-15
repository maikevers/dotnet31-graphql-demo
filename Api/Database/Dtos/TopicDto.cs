using System;

namespace Api.Database.Dtos
{
    public class TopicDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}