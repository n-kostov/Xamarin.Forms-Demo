using System;

namespace XamarinDemo
{
    public class Contact
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }


        public string Photo
        {
            get
            {
                return $"http://lorempixel.com/50/50/sports/{Id}";
            }
        }
    }
}
