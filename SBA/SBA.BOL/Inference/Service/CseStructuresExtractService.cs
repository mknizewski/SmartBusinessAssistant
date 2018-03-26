using SBA.DAL.Context.InferenceDb.Entity;
using System;
using System.Collections.Generic;

namespace SBA.BOL.Inference.Service
{
    public interface ICseStructuresExtractService
    {
        ArticleCse GetArticleCseStruct(KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair);
        EventCse GetEventCseStruct(KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair);
        OrganizationCse GetOrganizationCseStruct(KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair);
        PersonCse GetPersonCseStruct(KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair);
        SoftwareApplicationCse GetSoftwareApplicationCseStruct(KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair);
        SoftwareSourceCodeCse GetSoftwareSourceCodeCseStruct(KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair);
        VideoCse GetVideoCseStruct(KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair);
    }

    public class CseStructuresExtractService : ICseStructuresExtractService
    {
        public ArticleCse GetArticleCseStruct(
            KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair)
        {
            var item = keyValuePair.Value[0];
            return new ArticleCse
            {
                Name = item.TryGetValue(nameof(ArticleCse.Name).ToLower(), out object _name) ? Convert.ToString(_name) : string.Empty,
                Description = item.TryGetValue(nameof(ArticleCse.Description).ToLower(), out object _description) ? Convert.ToString(_description) : string.Empty,
                Url = item.TryGetValue(nameof(ArticleCse.Url).ToLower(), out object _url) ? Convert.ToString(_url) : string.Empty,
                DateCreated = item.TryGetValue(nameof(ArticleCse.DateCreated).ToLower(), out object _dateCreated) ? Convert.ToDateTime(_dateCreated) : (DateTime?) null,
                Keywords = item.TryGetValue(nameof(ArticleCse.Keywords).ToLower(), out object _keyword) ? Convert.ToString(_keyword) : string.Empty,
                Text = item.TryGetValue(nameof(ArticleCse.Text).ToLower(), out object _text) ? Convert.ToString(_text) : string.Empty,
                InLanguage = item.TryGetValue(nameof(ArticleCse.InLanguage).ToLower(), out object _inLanguage) ? Convert.ToString(_inLanguage) : string.Empty,
                DatePublished = item.TryGetValue(nameof(ArticleCse.DatePublished).ToLower(), out object _datePublished) ? Convert.ToDateTime(_datePublished) : (DateTime?) null,
                ArticleBody = item.TryGetValue(nameof(ArticleCse.ArticleBody).ToLower(), out object _articleBody) ? Convert.ToString(_articleBody) : string.Empty,
                ArticleSection = item.TryGetValue(nameof(ArticleCse.ArticleSection).ToLower(), out object _articleSection) ? Convert.ToString(_articleSection) : string.Empty
            };
        }

        public EventCse GetEventCseStruct(
            KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair)
        {
            var item = keyValuePair.Value[0];
            return new EventCse
            {
                Name = item.TryGetValue(nameof(EventCse.Name).ToLower(), out object _name) ? Convert.ToString(_name) : string.Empty,
                Description = item.TryGetValue(nameof(EventCse.Description).ToLower(), out object _description) ? Convert.ToString(_description) : string.Empty,
                Url = item.TryGetValue(nameof(EventCse.Url).ToLower(), out object _url) ? Convert.ToString(_url) : string.Empty,
                Location = item.TryGetValue(nameof(EventCse.Location).ToLower(), out object _location) ? Convert.ToString(_location) : string.Empty,
                StartTime = item.TryGetValue(nameof(EventCse.StartTime).ToLower(), out object _startTime) ? Convert.ToDateTime(_startTime) : (DateTime?) null
            };
        }

        public OrganizationCse GetOrganizationCseStruct(
            KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair)
        {
            var item = keyValuePair.Value[0];
            return new OrganizationCse
            {
                Name = item.TryGetValue(nameof(OrganizationCse.Name).ToLower(), out object _name) ? Convert.ToString(_name) : string.Empty,
                Description = item.TryGetValue(nameof(OrganizationCse.Description).ToLower(), out object _description) ? Convert.ToString(_description) : string.Empty,
                Url = item.TryGetValue(nameof(OrganizationCse.Url).ToLower(), out object _url) ? Convert.ToString(_url) : string.Empty,
                Address = item.TryGetValue(nameof(OrganizationCse.Address).ToLower(), out object _address) ? Convert.ToString(_address) : string.Empty,
                Email = item.TryGetValue(nameof(OrganizationCse.Email).ToLower(), out object _email) ? Convert.ToString(_email) : string.Empty,
                Logo = item.TryGetValue(nameof(OrganizationCse.Logo).ToLower(), out object _logo) ? Convert.ToString(_logo) : string.Empty,
                Location = item.TryGetValue(nameof(OrganizationCse.Location), out object _location) ? Convert.ToString(_location) : string.Empty,
                Telephone = item.TryGetValue(nameof(OrganizationCse.Telephone), out object _telephone) ? Convert.ToString(_telephone) : string.Empty
            };
        }

        public PersonCse GetPersonCseStruct(
            KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair)
        {
            var item = keyValuePair.Value[0];
            return new PersonCse
            {
                Name = item.TryGetValue(nameof(PersonCse.Name).ToLower(), out object _name) ? Convert.ToString(_name) : string.Empty,
                Description = item.TryGetValue(nameof(PersonCse.Description).ToLower(), out object _description) ? Convert.ToString(_description) : string.Empty,
                Url = item.TryGetValue(nameof(PersonCse.Url).ToLower(), out object _url) ? Convert.ToString(_url) : string.Empty,
                AdditionalName = item.TryGetValue(nameof(PersonCse.AdditionalName).ToLower(), out object _additionalName) ? Convert.ToString(_additionalName) : string.Empty,
                JobTitle = item.TryGetValue(nameof(PersonCse.JobTitle).ToLower(), out object _jobTitle) ? Convert.ToString(_jobTitle) : string.Empty,
                Nationality = item.TryGetValue(nameof(PersonCse.Nationality), out object _nationality) ? Convert.ToString(_nationality) : string.Empty,
                Telephone = item.TryGetValue(nameof(PersonCse.Telephone), out object _telephone) ? Convert.ToString(_telephone) : string.Empty
            };
        }

        public SoftwareApplicationCse GetSoftwareApplicationCseStruct(
            KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair)
        {
            var item = keyValuePair.Value[0];
            return new SoftwareApplicationCse
            {
                Name = item.TryGetValue(nameof(SoftwareApplicationCse.Name).ToLower(), out object _name) ? Convert.ToString(_name) : string.Empty,
                Description = item.TryGetValue(nameof(SoftwareApplicationCse.Description).ToLower(), out object _description) ? Convert.ToString(_description) : string.Empty,
                Url = item.TryGetValue(nameof(SoftwareApplicationCse.Url).ToLower(), out object _url) ? Convert.ToString(_url) : string.Empty,
                DateCreated = item.TryGetValue(nameof(SoftwareApplicationCse.DateCreated).ToLower(), out object _dateCreated) ? Convert.ToDateTime(_dateCreated) : (DateTime?) null,
                Keywords = item.TryGetValue(nameof(SoftwareApplicationCse.Keywords).ToLower(), out object _keyword) ? Convert.ToString(_keyword) : string.Empty,
                Text = item.TryGetValue(nameof(SoftwareApplicationCse.Text).ToLower(), out object _text) ? Convert.ToString(_text) : string.Empty,
                InLanguage = item.TryGetValue(nameof(SoftwareApplicationCse.InLanguage).ToLower(), out object _inLanguage) ? Convert.ToString(_inLanguage) : string.Empty,
                DatePublished = item.TryGetValue(nameof(SoftwareApplicationCse.DatePublished).ToLower(), out object _datePublished) ? Convert.ToDateTime(_datePublished) : (DateTime?) null,
                ApplicationCategory = item.TryGetValue(nameof(SoftwareApplicationCse.ApplicationCategory).ToLower(), out object _applicationCategory) ? Convert.ToString(_applicationCategory) : string.Empty,
                AvaiableOnDevice = item.TryGetValue(nameof(SoftwareApplicationCse.AvaiableOnDevice).ToLower(), out object _avaiableOnDevice) ? Convert.ToString(_avaiableOnDevice) : string.Empty,
                DownloadURL = item.TryGetValue(nameof(SoftwareApplicationCse.DownloadURL).ToLower(), out object _downloadUrl) ? Convert.ToString(_downloadUrl) : string.Empty,
                FileSize = item.TryGetValue(nameof(SoftwareApplicationCse.FileSize).ToLower(), out object _fileSize) ? Convert.ToString(_fileSize) : string.Empty
            };
        }

        public SoftwareSourceCodeCse GetSoftwareSourceCodeCseStruct(
            KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair)
        {
            var item = keyValuePair.Value[0];
            return new SoftwareSourceCodeCse
            {
                Name = item.TryGetValue(nameof(SoftwareSourceCodeCse.Name).ToLower(), out object _name) ? Convert.ToString(_name) : string.Empty,
                Description = item.TryGetValue(nameof(SoftwareSourceCodeCse.Description).ToLower(), out object _description) ? Convert.ToString(_description) : string.Empty,
                Url = item.TryGetValue(nameof(SoftwareSourceCodeCse.Url).ToLower(), out object _url) ? Convert.ToString(_url) : string.Empty,
                DateCreated = item.TryGetValue(nameof(SoftwareSourceCodeCse.DateCreated).ToLower(), out object _dateCreated) ? Convert.ToDateTime(_dateCreated) : (DateTime?)null,
                Keywords = item.TryGetValue(nameof(SoftwareSourceCodeCse.Keywords).ToLower(), out object _keyword) ? Convert.ToString(_keyword) : string.Empty,
                Text = item.TryGetValue(nameof(SoftwareSourceCodeCse.Text).ToLower(), out object _text) ? Convert.ToString(_text) : string.Empty,
                InLanguage = item.TryGetValue(nameof(SoftwareSourceCodeCse.InLanguage).ToLower(), out object _inLanguage) ? Convert.ToString(_inLanguage) : string.Empty,
                DatePublished = item.TryGetValue(nameof(SoftwareSourceCodeCse.DatePublished).ToLower(), out object _datePublished) ? Convert.ToDateTime(_datePublished) : (DateTime?)null,
                CodeRepository = item.TryGetValue(nameof(SoftwareSourceCodeCse.CodeRepository).ToLower(), out object _codeRepository) ? Convert.ToString(_codeRepository) : string.Empty,
                ProgrammingLanguage = item.TryGetValue(nameof(SoftwareSourceCodeCse.ProgrammingLanguage).ToLower(), out object _programmingLanguage) ? Convert.ToString(_programmingLanguage) : string.Empty,
                RuntimePlatform = item.TryGetValue(nameof(SoftwareSourceCodeCse.RuntimePlatform).ToLower(), out object _runtimePlatform) ? Convert.ToString(_runtimePlatform) : string.Empty
            };
        }

        public VideoCse GetVideoCseStruct(
            KeyValuePair<string, IList<IDictionary<string, object>>> keyValuePair)
        {
            var item = keyValuePair.Value[0];
            return new VideoCse
            {
                Name = item.TryGetValue(nameof(VideoCse.Name).ToLower(), out object _name) ? Convert.ToString(_name) : string.Empty,
                Description = item.TryGetValue(nameof(VideoCse.Description).ToLower(), out object _description) ? Convert.ToString(_description) : string.Empty,
                Url = item.TryGetValue(nameof(VideoCse.Url).ToLower(), out object _url) ? Convert.ToString(_url) : string.Empty,
            };
        }
    }
}