using System;

namespace SBA.DAL.Context.WebDb.Entity
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime InsertTime { get; set; }
    }
}