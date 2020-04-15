using Api.Database.Dtos;
using Microsoft.EntityFrameworkCore;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AuthorDto> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorDto>(e =>
                {
                    e.HasKey(a => a.Id);
                    e.OwnsMany(a => a.Topics);
                }
            );
        }

        public async Task<Either<Error, Guid>> AddAuthorAsync(AuthorDto author)
        {
            Authors.Add(author);
            int added = await SaveChangesAsync();
            return added == 1
                    ? Prelude.Right<Error, Guid>(author.Id)
                    : Prelude.Left<Error, Guid>(Error.AuthorNotCreated());
        }

        public async Task<Either<Error, Guid>> AddTopicAsync(Guid authorId, TopicDto topic)
        {
            Either<Error, AuthorDto> author = await GetAuthorAsync(authorId);
            return (await author.MapAsync(async a =>
            {
                Guid topicId = Guid.NewGuid();
                topic.Id = topicId;
                a.Topics.Add(topic);
                Either<Error, AuthorDto> updated = await UpdateAuthorAsync(a);
                return updated.Map(p => topicId);
            })).Flatten();
        }

        public async Task<Either<Error, Unit>> DeleteAuthorAsync(Guid authorId)
        {
            AuthorDto author = new AuthorDto { Id = authorId };
            Authors.Attach(author);
            Authors.Remove(author);
            int removed = await SaveChangesAsync();
            return removed > 0
                    ? Prelude.Right<Error, Unit>(new Unit())
                    : Prelude.Left<Error, Unit>(Error.AuthorNotFound(authorId));
        }

        public async Task<Either<Error, Unit>> DeleteTopicAsync(Guid authorId, Guid topicId)
        {
            Either<Error, AuthorDto> author = await GetAuthorAsync(authorId);
            return (await author.MapAsync(async a =>
            {
                int removed = a.Topics.RemoveAll(p => p.Id.Equals(topicId));
                Either<Error, Unit> removalSuccess = removed > 0
                    ? Prelude.Right<Error, Unit>(new Unit())
                    : Prelude.Left<Error, Unit>(Error.TopicNotRemoved(topicId));

                Either<Error, AuthorDto> updated = (await removalSuccess.MapAsync(r => UpdateAuthorAsync(a))).Flatten();
                return updated.Map(p => new Unit());
            })).Flatten();
        }

        public async Task<Either<Error, AuthorDto>> GetAuthorAsync(Guid authorId)
        {
            List<AuthorDto> authors = await Authors.Where(author => author.Id.Equals(authorId)).ToListAsync();
            return authors.HeadOrLeft(Error.AuthorNotFound(authorId));
        }

        public async Task<Either<Error, TopicDto>> GetTopicAsync(Guid authorId, Guid topicId)
        {
            Either<Error, AuthorDto> author = await GetAuthorAsync(authorId);
            return author.Map(a => a.Topics.Where(p => p.Id.Equals(topicId)).HeadOrLeft(Error.TopicNotFound(topicId))).Flatten();
        }

        public async Task<IReadOnlyList<AuthorDto>> GetAllAuthorsAsync()
        {
            return await Authors.ToListAsync();
        }

        public async Task<IReadOnlyList<TopicDto>> GetAllTopicsAsync()
        {
            IReadOnlyList<AuthorDto> authors = await GetAllAuthorsAsync();
            return authors.SelectMany(p => p.Topics).ToList();
        }

        public async Task<Either<Error, AuthorDto>> UpdateAuthorAsync(AuthorDto author)
        {
            Authors.Update(author);
            int updated = await SaveChangesAsync();
            return updated > 0
                    ? Prelude.Right<Error, AuthorDto>(author)
                    : Prelude.Left<Error, AuthorDto>(Error.AuthorNotFound(author.Id));
        }

        public async Task<Either<Error, TopicDto>> UpdateTopicAsync(Guid authorId, TopicDto topic)
        {
            Either<Error, AuthorDto> author = await GetAuthorAsync(authorId);
            return (await author.MapAsync(async a =>
            {
                int removed = a.Topics.RemoveAll(p => p.Id.Equals(topic.Id));
                if (removed == 0)
                {
                    return Prelude.Left(Error.TopicNotFound(topic.Id));
                }
                a.Topics.Add(topic);
                Either<Error, AuthorDto> updatedAuthor = await UpdateAuthorAsync(a);
                return updatedAuthor.Map(a => a.Topics.Where(t => t.Id.Equals(topic.Id)).HeadOrLeft(Error.TopicNotUpdated())).Flatten();
            })).Flatten();
        }
    }
}