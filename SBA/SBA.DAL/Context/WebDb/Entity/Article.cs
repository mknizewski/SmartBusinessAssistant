using System;

namespace SBA.DAL.Context.WebDb.Entity
{
    public class Article
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public string Path { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
