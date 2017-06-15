using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.WebAPI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Không được để trống tên.")]
        public string FullName { set; get; }

        [Required(ErrorMessage = "Không được để trống tên tài khoản.")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Không được để trống mật khẩu.")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự.")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Không được để trống email.")]
        [EmailAddress(ErrorMessage ="Địa chỉ email không đúng.")]
        public string Email { set; get; }

        public string Address { set; get; }

        [Required(ErrorMessage = "Không được để trống số điện thoại.")]
        public string PhoneNumber { set; get; }
    }
}