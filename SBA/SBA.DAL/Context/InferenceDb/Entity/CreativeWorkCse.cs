using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public abstract class CreativeWorkCse : ThingCse
    {
        public DateTime? DateCreated { get; set; }
        public string Keywords { get; set; }
        public string Text { get; set; }
        public string InLanguage { get; set; }
        public DateTime? DatePublished { get; set; }
       
        [ForeignKey(nameof(AuthorOrganization))]
        public int? AuthorOrganizationId { get; set; }

        [ForeignKey(nameof(AuthorPerson))]
        public int? AuthorPersonId { get; set; }

        [ForeignKey(nameof(Character))]
        public int? CharacterId { get; set; }

        [ForeignKey(nameof(Video))]
        public int? VideoId { get; set; }

        public virtual OrganizationCse AuthorOrganization { get; set; }
        public virtual PersonCse AuthorPerson { get; set; }
        public virtual PersonCse Character { get; set; }
        public virtual VideoCse Video { get; set; }
    }
}