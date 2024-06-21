using StickFun.WeatherForecast.Domain.Exceptions;

namespace StickFun.WeatherForecast.Domain.ValidationExtensions;

public static class ValidationExtensions
{
    public static void ThrowIfNullOrWhitespace(this string value, string message = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(message ?? $"{nameof(value)} не может быть пустой строкой.");
    }

    public static void ThrowIfNull(this object obj, string message)
    {
        if (obj is null)
            throw new DomainException(message ?? $"{nameof(obj)} не может ссылаться на null.");
    }
}
