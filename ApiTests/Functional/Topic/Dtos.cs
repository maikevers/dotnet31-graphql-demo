using Api.Database.Dtos;
using System;
using System.Collections.Generic;

namespace ApiTests.Functional.Topic
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class Data
    {
        public Topic SubmitTopic { get; set; }
        public Topic RevokeTopic { get; set; }
        public Topic EditTopic { get; set; }
        public List<Topic> Topics { get; set; }
    }

    public class TopicsResponse
    {
        public Data Data { get; set; }
    }

    public class TopicsResponseOnError
    {
        public DataOnError Data { get; set; }
    }

    public class DataOnError
    {
        public Error SubmitTopic { get; set; }
        public Error EditTopic { get; set; }
        public Error RevokeTopic { get; set; }
    }
}