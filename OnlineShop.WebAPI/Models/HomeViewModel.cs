using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.WebAPI.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<ProductViewModel> LastestProducts { set; get; }
        public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }
        
    }
}