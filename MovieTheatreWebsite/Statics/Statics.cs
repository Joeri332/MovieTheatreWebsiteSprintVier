using System.ComponentModel;
using System.Reflection;

namespace MovieTheatreWebsite.Statics
{
    public static class Statics
    {
        public static TimeSpan MovieLengthLong = new(1, 30, 00);

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
