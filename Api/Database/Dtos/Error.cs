using HotChocolate.Types;
using System;

namespace Api.Database.Dtos
{
    public class Error
    {
        public string ErrorMessage { get; set; }

        public static Error InvalidId(UuidType id)
        {
            return new Error { ErrorMessage = $"Invalid id: {id}" };
        }

        public static Error AuthorNotFound(Guid authorId)
        {
            return new Error { ErrorMessage = $"Could not find author with id {authorId}" };
        }

        public static Error AuthorNotCreated()
        {
            return new Error { ErrorMessage = $"Could not create new author." };
        }

        public static Error TopicNotRemoved(Guid topicId)
        {
            return new Error { ErrorMessage = $"Topic {topicId} could not be removed from author." };
        }

        internal static Error TopicNotFound(Guid topicId)
        {
            return new Error { ErrorMessage = $"Topic with id {topicId} not found." };
        }

        internal static Error TopicNotUpdated()
        {
            return new Error { ErrorMessage = $"Update topic failed." };
        }
    }
}