using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class GoogleDriveFile
    {
        public int Id { get; set; }
        public string TiTle { get; set; }
        public string FileId { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        //Clé Etrangére
        public int ExpirimentId { get; set; }

        public Expiriment Expiriments { get; set; }
    }
}