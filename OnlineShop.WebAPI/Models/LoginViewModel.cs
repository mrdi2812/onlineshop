using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.WebAPI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        public string UserName { set; get; }

        [Required(ErrorMessage ="Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}