using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WPF_BAP.Models
{
    public partial class Article
    {
        public long Artikel { get; set; }
        public string KassaNed { get; set; }
        public string KassaFr { get; set; }
        public string Kwaliteit { get; set; }
        public string PubliDate { get; set; }
        public string VkpEur { get; set; }
        public short? Hoofdgroep { get; set; }
        public short? Seizoen { get; set; }
        [NotMapped]
        public string URI
        {
            get
            {
                return String.Format(@"E:/Downloads/wetransfer-3b50b2/foto_s8/{0}.JPG", Artikel);
            }
        }
    }
}
