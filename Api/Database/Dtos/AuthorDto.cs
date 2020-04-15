using System;
using System.Collections.Generic;

namespace Api.Database.Dtos
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TopicDto> Topics { get; set; } = new List<TopicDto>();
    }
}