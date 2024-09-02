using System;
using System.Collections.Generic;
using System.Linq;


namespace Chapter2
{
    // Class UserAddress
    public class UserAddress
    {
        public int AddressId { get; private set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Street { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; private set; }


        public void AddAddress()
        {
            // Logic to add address
        }


        public void UpdateAddress()
        {
            // Logic to update address
        }


        public void DeleteAddress()
        {
            // Logic to delete address
        }


        public void SetAsDefault()
        {
            IsDefault = true;
            // Logic to set this address as default
        }
    }
}