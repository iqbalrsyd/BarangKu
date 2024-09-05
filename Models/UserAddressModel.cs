using System;

using namespace BarangKu
{
    public class UserAddressModel
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
    }
}