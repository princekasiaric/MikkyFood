using System;

namespace MFR.Core.DTO.Request
{
    public class OrderRequest : BaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Location { get; set; } 

        public int NumberOfPeople { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}
