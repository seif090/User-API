using User.Presentation.Middlewares;

namespace User.Presentation.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
