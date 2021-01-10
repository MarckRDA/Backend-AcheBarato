using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Models.AlarmPrices;
using Domain.Models.Products;

namespace Domain.Models.Users
{
    public class User 
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public IReadOnlyCollection<Product> WishListProducts => _wishListProducts;
        private List<Product> _wishListProducts;
        public IReadOnlyCollection<AlarmPrice> WishProductsAlarmPrices => _wishProductsAlarmPrices; 
        private List<AlarmPrice> _wishProductsAlarmPrices;

        public User(string name, string email, string password)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Email = email;
            Password = password;
            _wishListProducts = new List<Product>();
            _wishProductsAlarmPrices = new List<AlarmPrice>();
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
            _wishListProducts.Add(productToPutIn);
        }

        public void AddAlarmPrice(AlarmPrice alarm)
        {
            _wishProductsAlarmPrices.Add(alarm);
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
