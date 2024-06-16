using System;

namespace UserSystemFramework.Scripts.System.Data.Interfaces
{
    public interface IDatabaseEntry
    {
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Status { get; set; }
    }
}