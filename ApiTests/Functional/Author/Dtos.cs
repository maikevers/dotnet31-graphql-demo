using System;
using System.Collections.Generic;

namespace ApiTests.Functional.Author.Dtos
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class Data
    {
        public List<Author> Authors { get; set; }
        public Author CreateAuthor { get; set; }
        public Author Author { get; set; }
    }

    public class AuthorsResponse
    {
        public Data Data { get; set; }
    }
}