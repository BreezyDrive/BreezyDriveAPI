using BreezyDrive.CommonService.Contracts;
using BreezyDrive.UserServices.Application.Interfaces;
using MassTransit;

namespace BreezyDrive.UserServices.RabbitMQ;

public class CheckUserExistConsumer : IConsumer<CheckUserExistRequest>
{
    private readonly IUserService _userService;
    private readonly ILogger<CheckUserExistConsumer> _logger;

    public CheckUserExistConsumer(ILogger<CheckUserExistConsumer> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    
    public async Task Consume(ConsumeContext<CheckUserExistRequest> context)
    {
        var request = context.Message;
        _logger.LogInformation("Received request to check existence of userId: {UserId}", request.UserId);
        
        
        var user = await  _userService.GetUserById(request.UserId);

        if (user == null)
        {
            _logger.LogInformation("This user not found: {UserId}", request.UserId);
            await context.RespondAsync(new CheckUserExistResponse { IsUserExists = false });
            return;
        }
        
        // Gửi phản hồi về cho requester
        await context.RespondAsync(new CheckUserExistResponse { IsUserExists = true });
        
    }
}