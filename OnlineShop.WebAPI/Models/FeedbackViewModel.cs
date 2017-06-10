using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.WebAPI.Models
{
    public class FeedbackViewModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(250,ErrorMessage = "Số ký tự vượt quá 250 ký tự")]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { set; get; }

        [MaxLength(250, ErrorMessage = "Số ký tự vượt quá 250 ký tự")]
        public string Email { set; get; }

        [MaxLength(500, ErrorMessage = "Số ký tự vượt quá 500 ký tự")]
        public string Message { set; get; }
        public DateTime CreateDate { set; get; }

        [Required(ErrorMessage = "Phải nhập trạng thái")]
        public bool Status { set; get; }

        public ContactDetailViewModel ContactDetail { set; get; }
    }
}