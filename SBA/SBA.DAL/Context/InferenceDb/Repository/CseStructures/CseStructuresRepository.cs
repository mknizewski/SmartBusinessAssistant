using SBA.DAL.Context.InferenceDb.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.CseStructures
{
    public interface ICseStructuresRepository : IBaseRepository
    {
        void AddArticle(ArticleCse articleCse);
        List<ArticleCse> GetNotShowedArticles();

        void AddEvent(EventCse eventCse);
        List<EventCse> GetNotShowedEvents();

        void AddOrganization(OrganizationCse organizationCse);
        List<OrganizationCse> GetNotShowedOrganizations();

        void AddPerson(PersonCse personCse);
        List<PersonCse> GetNotShowedPersons();

        void AddVideo(VideoCse videoCse);
        List<VideoCse> GetNotShowedVideos();

        void AddSoftwareApplication(SoftwareApplicationCse softwareApplicationCse);
        void AddSoftwareSourceCoce(SoftwareSourceCodeCse softwareSourceCodeCse);
        void AddThingWithRecognizeType(ThingCse thingCse);

    }

    public class CseStructuresRepository : BaseRepository, ICseStructuresRepository
    {
        public void AddArticle(ArticleCse articleCse)
        {
            Add(articleCse);
            SaveChanges();
        }

        public void AddEvent(EventCse eventCse)
        {
            Add(eventCse);
            SaveChanges();
        }

        public void AddOrganization(OrganizationCse organizationCse)
        {
            Add(organizationCse);
            SaveChanges();
        }

        public void AddPerson(PersonCse personCse)
        {
            Add(personCse);
            SaveChanges();
        }

        public void AddSoftwareApplication(SoftwareApplicationCse softwareApplicationCse)
        {
            Add(softwareApplicationCse);
            SaveChanges();
        }

        public void AddSoftwareSourceCoce(SoftwareSourceCodeCse softwareSourceCodeCse)
        {
            Add(softwareSourceCodeCse);
            SaveChanges();
        }

        public void AddVideo(VideoCse videoCse)
        {
            Add(videoCse);
            SaveChanges();
        }

        public void AddThingWithRecognizeType(ThingCse thingCse)
        {
            if (thingCse is ArticleCse)
                AddArticle((ArticleCse)thingCse);
            else if (thingCse is EventCse)
                AddEvent((EventCse)thingCse);
            else if (thingCse is OrganizationCse)
                AddOrganization((OrganizationCse)thingCse);
            else if (thingCse is PersonCse)
                AddPerson((PersonCse)thingCse);
            else if (thingCse is SoftwareApplicationCse)
                AddSoftwareApplication((SoftwareApplicationCse)thingCse);
            else if (thingCse is SoftwareSourceCodeCse)
                AddSoftwareSourceCoce((SoftwareSourceCodeCse)thingCse);
            else if (thingCse is VideoCse)
                AddVideo((VideoCse)thingCse);

            SaveChanges();
        }

        public List<ArticleCse> GetNotShowedArticles() =>
            Queryable<ArticleCse>()
                .Where(x => !x.IsShowed)
                .ToList();

        public List<EventCse> GetNotShowedEvents() =>
            Queryable<EventCse>()
                .Where(x => !x.IsShowed)
                .ToList();

        public List<OrganizationCse> GetNotShowedOrganizations() =>
            Queryable<OrganizationCse>()
                .Where(x => !x.IsShowed)
                .ToList();

        public List<PersonCse> GetNotShowedPersons() =>
            Queryable<PersonCse>()
                .Where(x => !x.IsShowed)
                .ToList();

        public List<VideoCse> GetNotShowedVideos() =>
            Queryable<VideoCse>()
                .Where(x => !x.IsShowed)
                .ToList();
    }
}