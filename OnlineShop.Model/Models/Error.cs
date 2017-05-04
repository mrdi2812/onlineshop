using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Model.Models
{
    [Table("Errors")]
    public class Error
    {
        [Key]
        public int ID { set; get; }

        public string Messeage { set; get; }
        public string StackTrace { set; get; }
        public DateTime CreateDate { set; get; }
    }
}