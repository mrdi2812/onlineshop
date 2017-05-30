using OnlineShop.Common;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Service
{
    public interface IComonService
    {
        Footer GetFooter();
        IEnumerable<Slide> GetSlide();

    }
    public class ComonService : IComonService
    {
         IFooterRepository _footerRepository;
         IUnitOfWork _unitOfWork;
         ISlideRepository _slideRepository;
        public ComonService(IFooterRepository footerRepository,IUnitOfWork unitOfWork,ISlideRepository slideRepository)
        {
            this._footerRepository = footerRepository;
            this._unitOfWork = unitOfWork;
            this._slideRepository = slideRepository;
        }
        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterID);
        }

        public IEnumerable<Slide> GetSlide()
        {
            return _slideRepository.GetMulti(x => x.Status);
        }

    }
}
