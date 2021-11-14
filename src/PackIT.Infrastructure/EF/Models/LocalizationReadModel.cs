using System.Linq;

namespace PackIT.Infrastructure.EF.Models
{
    internal class LocalizationReadModel
    {
        public string City { get; set; }
        public string Country { get; set; }

        public static LocalizationReadModel Create(string value)
        {
            var splitLocalization = value.Split(',');
            return new LocalizationReadModel
            {
                City = splitLocalization.First(),
                Country = splitLocalization.Last()
            };
        }
        
        public override string ToString()
            => $"{City},{Country}";
    }
}