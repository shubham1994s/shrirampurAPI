//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SwachhBharatAPI.Dal.DataContexts
{
    using System;
    using System.Collections.Generic;
    
    public partial class HouseMaster
    {
        public int houseId { get; set; }
        public string houseNumber { get; set; }
        public string houseOwner { get; set; }
        public string houseOwnerMar { get; set; }
        public string houseOwnerMobile { get; set; }
        public string houseAddress { get; set; }
        public string houseLat { get; set; }
        public string houseLong { get; set; }
        public string houseQRCode { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public string ReferanceId { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        public Nullable<int> userId { get; set; }
        public string FCMID { get; set; }
        public Nullable<System.DateTime> lastModifiedEntry { get; set; }
        public string RFIDTagId { get; set; }
        public string WasteType { get; set; }
        public string QRCodeImage { get; set; }
    }
}