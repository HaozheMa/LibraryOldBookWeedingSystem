//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibrarySystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Books
    {
        public string CallNum { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public int Duplicates { get; set; }
        public double CirculationFrequency { get; set; }
        public string PublisherName { get; set; }
        public bool PublisherBaiJia { get; set; }
        public int CirculationCount { get; set; }
        public Nullable<int> PublishYear { get; set; }
        public Nullable<int> YearCount { get; set; }
        public int OffTime { get; set; }
        public int OffTimeCount { get; set; }
        public Nullable<double> BWI { get; set; }
        public long Id { get; set; }
    }
}
