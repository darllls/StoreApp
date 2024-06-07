﻿namespace DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
    }
}
