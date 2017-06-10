using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);

        void Save();
    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedBackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedBackRepository, IUnitOfWork unitOfWork)
        {
            _feedBackRepository = feedBackRepository;
            _unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            return _feedBackRepository.Add(feedback);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}