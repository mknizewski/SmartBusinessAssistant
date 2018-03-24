using SBA.DAL.Context.InferenceDb.Entity;

namespace SBA.DAL.Context.InferenceDb.Repository.CseStructures
{
    public interface ICseStructuresRepository : IBaseRepository
    {
        void AddArticle(ArticleCse articleCse);
        void AddEvent(EventCse eventCse);
        void AddOrganization(OrganizationCse organizationCse);
        void AddPerson(PersonCse personCse);
        void AddVideo(VideoCse videoCse);
        void AddSoftwareApplication(SoftwareApplicationCse softwareApplicationCse);
        void AddSoftwareSourceCoce(SoftwareSourceCodeCse softwareSourceCodeCse);
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
    }
}