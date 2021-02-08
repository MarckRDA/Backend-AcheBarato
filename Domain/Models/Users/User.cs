using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Attributes;
using Domain.Models.AlarmPrices;
using Domain.Models.Products;

namespace Domain.Models.Users
{
    [BsonCollection("Users")]
    public class User 
    {
        public Guid Id { get; private set; } = new Guid();
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Profile Profile { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public List<string> SearchTags { get; private set; } = new List<string>();
        public List<Product> WishListProducts {get; private set;} = new List<Product>();
        public List<AlarmPrice> WishProductsAlarmPrices {get; private set;} = new List<AlarmPrice>();

        public User(string name, string email, string password, Profile profile, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Profile = profile;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        public User(string name, string password, string email,string celphone)
        {
            Name = name;
            Password = password;
            Email = email;
        }

        private bool ValidateEmail()
        {
            return Regex.IsMatch(
                Email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase
            );
        }

        public void AddProductInWishList(Product productToPutIn)
        {
            WishListProducts.Add(productToPutIn);
        }

        public void AddAlarmPrice(AlarmPrice alarm)
        {
            WishProductsAlarmPrices.Add(alarm);
        }

        public void AddTagSearch(string searchTag)
        {
            SearchTags.Add(searchTag);
        }

        private bool ValidateName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            var words = Name.Split(' ');
            if (words.Length < 2)
            {
                return false;
            }

            foreach (var word in words)
            {
                if (word.Trim().Length < 2)
                {
                    return false;
                }
                if (word.Any(x => !char.IsLetter(x)))
                {
                    return false;
                }
            }

            return true;
        }

        public (IList<string> errors, bool isValid) Validate()
        {
            var errors = new List<string>();
            if (!ValidateName())
            {
                errors.Add("Nome inválido.");
            }

            if (!ValidateEmail())
            {
                errors.Add("Email inválido.");
            }

            return (errors, errors.Count == 0);
        }
    }
}
