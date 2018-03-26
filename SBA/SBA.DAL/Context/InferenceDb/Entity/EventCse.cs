using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class EventCse : ThingCse
    {
        [NotMapped]
        public const string PageMapName = "event";

        public string Location { get; set; }
        public DateTime? StartTime { get; set; }

        [ForeignKey(nameof(OrganizerOrganization))]
        public int? OrganizerOrganizationId { get; set; }

        [ForeignKey(nameof(OrganizerPerson))]
        public int? OrganizerPersonId { get; set; }

        public virtual OrganizationCse OrganizerOrganization { get; set; }
        public virtual PersonCse OrganizerPerson { get; set; }
    }
}