using BreezyDrive.Common.Middleware;

namespace BreezyDrive.ConversationServices.Infrastructure.DependencyInjection
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseApplicationMiddleware(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthorization();

            return app;
        }

        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BreezyDrive.ConversationServices_API");
                    c.RoutePrefix = "";
                    c.EnableTryItOutByDefault();
                });
            }

            return app;
        }
    }
}
