using System;
using GalaSoft.MvvmLight;

namespace Opora.Models
{
    public class BaseModel : ObservableObject
    {
        private DateTimeOffset _updatedAt;

        public BaseModel()
        {
        }

        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt
        {
            get { return _updatedAt; }
            set { Set(() => UpdatedAt, ref _updatedAt, value); }
        }
    }
}