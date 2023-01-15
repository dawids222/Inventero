namespace LibLite.Inventero.DAL.Extensions
{
    internal static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime dateTime)
        {
            return dateTime.Kind switch
            {
                DateTimeKind.Utc => dateTime,
                DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc),
                DateTimeKind.Local => dateTime.ToUniversalTime(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
