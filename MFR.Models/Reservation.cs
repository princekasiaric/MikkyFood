using System;

namespace MFR.Models
{
    public class Reservation
    {
        public long Id { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime Date { get; set; } 
        public string Time { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
