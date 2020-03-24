using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using TimeTracker.DataAccess.Contracts;

namespace TimeTracker.Infrastructure.Entities
{
    [Table("Description")]
    [AutoMap(typeof(Domain.ValueObjects.Description))]
    public partial class Description : IEntity
    {
        public int Id { get; set; }
        public string Template { get; set; }
    }
}
