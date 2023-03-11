using System;

namespace SODP.Domain.ValueObjects
{
    public record Stage 
    {

        public string Sign { get; init; }

        public string Name { get; init; }

        private Stage() { }

        public static Stage Create(string sign, string name)
        {
            if (string.IsNullOrWhiteSpace(sign))
            {
                throw new ArgumentException("Sign is empty.");
            }

            return new Stage
            {
                Sign = sign,
                Name = name
            };
        }
    }
}
