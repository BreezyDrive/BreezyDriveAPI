using AutoMapper;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.NotificationServices.Application.DTOs.Request;
using BreezyDrive.NotificationServices.Application.DTOs.Response;
using BreezyDrive.NotificationServices.Application.Interfaces;
using BreezyDrive.NotificationServices.Domain.Entities;
using BreezyDrive.NotificationServices.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BreezyDrive.NotificationServices.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMongoUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IMongoUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<NotificationResponse> CreateNotificationAsync(NotificationRequest request)
        {
            var noti = _mapper.Map<Notification>(request);

            noti.CreateDate = DateTimeOffset.UtcNow;
            await _unitOfWork.Notifications.InsertOneAsync(noti);

            return _mapper.Map<NotificationResponse>(noti);
        }


        public async Task<IEnumerable<NotificationResponse>> GetNotificationByUser(Guid receiverId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.ReceiverId, receiverId);

            var sort = Builders<Notification>.Sort.Descending(n => n.CreateDate);

            var notifications = await _unitOfWork.Notifications
                                                   .Find(filter)
                                                   .Sort(sort)
                                                   .ToListAsync();

            return _mapper.Map<IEnumerable<NotificationResponse>>(notifications);
        }

        public async Task UpdateIsSeenStatus(Guid id)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, id);

            var update = Builders<Notification>.Update.Set(n => n.IsSeen, true);

            var result = await _unitOfWork.Notifications.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                throw new CustomExceptions.DataNotFoundException("Notification not found.");
            }

            Console.WriteLine($"Notification with Id {id} updated to IsSeen = true.");
        }
        public async Task UpdateAllIsSeenStatus(Guid receiverId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.ReceiverId, receiverId);

            var update = Builders<Notification>.Update.Set(n => n.IsSeen, true);

            await _unitOfWork.Notifications.UpdateManyAsync(filter, update);
        }

    }
}
