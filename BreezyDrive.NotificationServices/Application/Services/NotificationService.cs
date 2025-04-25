using AutoMapper;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.NotificationServices.Application.DTOs.Request;
using BreezyDrive.NotificationServices.Application.DTOs.Response;
using BreezyDrive.NotificationServices.Application.Interfaces;
using BreezyDrive.NotificationServices.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BreezyDrive.NotificationServices.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMongoRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(IMongoUnitOfWork unitOfWork, IMapper mapper)
        {
            _notificationRepository = unitOfWork.Repository<Notification>("Notifications");
            _mapper = mapper;
        }

        public async Task<NotificationResponse> CreateNotificationAsync(NotificationRequest request)
        {
            var notification = _mapper.Map<Notification>(request);
            notification.CreateDate = DateTimeOffset.UtcNow;

            await _notificationRepository.InsertAsync(notification);

            return _mapper.Map<NotificationResponse>(notification);
        }

        public async Task<IEnumerable<NotificationResponse>> GetNotificationByUser(Guid receiverId)
        {

            var all = await _notificationRepository.GetAllAsync();

            var filtered = all
                .Where(n => n.ReceiverId == receiverId)
                .OrderByDescending(n => n.CreateDate)
                .ToList();

            return _mapper.Map<IEnumerable<NotificationResponse>>(filtered);
        }

        public async Task UpdateIsSeenStatus(Guid id)
        {
            var existing = await _notificationRepository.GetByIdAsync(id.ToString());
            if (existing == null)
                throw new CustomExceptions.DataNotFoundException("Notification not found.");

            existing.IsSeen = true;
            await _notificationRepository.UpdateAsync(id.ToString(), existing);
        }

        public async Task UpdateAllIsSeenStatus(Guid receiverId)
        {
            var all = await _notificationRepository.GetAllAsync();
            var toUpdate = all.Where(n => n.ReceiverId == receiverId).ToList();

            foreach (var noti in toUpdate)
            {
                noti.IsSeen = true;
                await _notificationRepository.UpdateAsync(noti.Id.ToString(), noti);
            }
        }
    }
}
