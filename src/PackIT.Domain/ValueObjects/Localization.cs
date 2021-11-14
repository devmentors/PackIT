using System.Linq;

namespace PackIT.Domain.ValueObjects
{
    public record Localization(string City, string Country)
    {
        public static Localization Create(string value)
        {
            var splitLocalization = value.Split(',');
            return new Localization(splitLocalization.First(), splitLocalization.Last());
        }

        public override string ToString()
            => $"{City},{Country}";
    }
}