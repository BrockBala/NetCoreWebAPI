﻿namespace NetCoreWebAPI.Models
{
    public class UpdateContact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
