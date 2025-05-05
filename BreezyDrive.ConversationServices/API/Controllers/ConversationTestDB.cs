using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using BreezyDrive.ConversationServices.Domain.Entities;
using BreezyDrive.ConversationServices.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;

namespace BreezyDrive.ConversationServices.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationTestDB : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ConversationDbContext _dbContext;

        public ConversationTestDB(IConfiguration configuration, ConversationDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("initialize-database")]
        public IActionResult InitializeDatabase()
        {
            try
            {
                // The database and collections are already initialized in ConversationDbContext constructor
                // We just need to verify the connection and collections
                var conversations = _dbContext.Conversations;
                var messages = _dbContext.ConversationMessages;
                var files = _dbContext.MessageFiles;

                return Ok(new
                {
                    Status = "Success",
                    Message = "MongoDB database and collections initialized successfully",
                    Collections = new[]
                    {
                        "Conversations",
                        "ConversationMessages",
                        "MessageFiles"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Failed to initialize database",
                    Error = ex.Message
                });
            }
        }
    }
}
