using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.CarServices.Application.Services;

public class CarRatingService (IUnitOfWork unitOfWork, IMapper mapper) : ICarRatingService
{
    public async Task<IEnumerable<CarRatingsResponse>> GetAllAsync()
    {
        var carRatings = await unitOfWork.Repository<CarRatings>().GetAllAsync();

        if (carRatings.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("Car Ratings not found");
        }
        
        return mapper.Map<IEnumerable<CarRatingsResponse>>(carRatings);
    }

    public async Task<CarRatingsResponse> GetByGuid(Guid guid)
    {
        var carRating = await GetCarRatingById(guid);
        return mapper.Map<CarRatingsResponse>(carRating);
    }

    public async Task<CarRatingsResponse> CreateCarRating(CarRatingRequest request)
    {
        var carRating = mapper.Map<CarRatings>(request);
        await unitOfWork.Repository<CarRatings>().InsertAsync(carRating);
        
        await unitOfWork.SaveAsync();
        
        return mapper.Map<CarRatingsResponse>(carRating);

    }

    public async Task<CarRatingsResponse> UpdateCarRating(Guid guid, CarRatingRequest request)
    {
        var carRating = await GetCarRatingById(guid);

        mapper.Map(request, carRating);
        await unitOfWork.Repository<CarRatings>().UpdateAsync(carRating);
        await unitOfWork.SaveAsync();
        return mapper.Map<CarRatingsResponse>(carRating);
    }

    public async Task<bool> Delete(Guid guid)
    {
        var carRating = await GetCarRatingById(guid);

        await unitOfWork.Repository<CarRatings>().DeleteAsync(carRating);
        await unitOfWork.SaveAsync();
        return true;
    }


    private async Task<CarRatings> GetCarRatingById(Guid guid)
    {
        var carRating = await unitOfWork.Repository<CarRatings>().GetByIdAsync(guid);
        if (carRating == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Ratings not found");
        }
        return carRating;
    }
}