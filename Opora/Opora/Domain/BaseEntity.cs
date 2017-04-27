using System;

using SQLite;

namespace Opora.Domain
{
    public class BaseEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}