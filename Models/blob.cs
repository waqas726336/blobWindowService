using System;

namespace init_backend.Model
{
    public class blob
    {
        public int id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string containerName { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime? Updated { get; set; }
        public int isDeleted { get; set; }
    }
}