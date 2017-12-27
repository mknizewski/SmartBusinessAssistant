using System;
using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.WebDb.Entity
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
