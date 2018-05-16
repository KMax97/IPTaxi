using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Order
    {
        public int NumberOfOrder { get; set; }
        public int StartStreetId { get; set; }
        public string NumberOfStartHouse { get; set; }
        public DateTime? Time { get; set; }
        public int FinalStreetId { get; set; }
        public string NumberOfFinalHouse { get; set; }
        public DateTime? TimeOfEndingOrder { get; set; }
        public int NumberOfRecord { get; set; }
        public int DispetcherId { get; set; }
        public int ClientId { get; set; }
        public int? RealValue { get; set; }
        public int? AmountOfWrittenPoints { get; set; }
        public int? Value { get; set; }
        public int? AmountOfAccruedPoints { get; set; }

        public Client Client { get; set; }
        public Dispetcher Dispetcher { get; set; }
        public Street FinalStreet { get; set; }
        public Record NumberOfRecordNavigation { get; set; }
        public Street StartStreet { get; set; }
    }
}
