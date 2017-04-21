using System;
using GalaSoft.MvvmLight;

namespace Opora.Models
{
    public class BaseModel : ObservableObject
    {
        public BaseModel()
        {
        }

        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}