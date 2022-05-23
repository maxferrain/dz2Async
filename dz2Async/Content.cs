using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dz2Async
{
    public class Content
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Language { get; set; }
        
        public double Score { get; set; }

        public string Description { get; set; }

        [ForeignKey("MainActor")]
        public virtual int MainRoleActorId { get; set; }

        [ForeignKey("Actor")]
        public virtual int ActorId { get; set; }

        [ForeignKey("Country")]
        public virtual int CountryId { get; set; }
    }
}
