using BreezyDrive.CommonService.Middleware;
using BreezyDrive.NotificationServices.Application.Hubs;
using BreezyDrive.NotificationServices.Infrastructure.Persistance;

namespace BreezyDrive.NotificationServices.Infrastructure.DependencyInjection
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseApplicationMiddleware(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDBContext>();
                Console.WriteLine("✅ NotificationDBContext has been resolved successfully.");
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notification");
            });
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BreezyDrive.NotificationServices_API");
                    c.RoutePrefix = "";
                    c.EnableTryItOutByDefault();
                });
            }

            return app;
        }
    }
}
