using System;

namespace SBA.DAL.Context.WebDb.Entity
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }

        public enum Type
        {
            ArticleJson = 1
        }
    }
}
