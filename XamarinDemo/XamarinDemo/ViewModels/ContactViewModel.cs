using System;

namespace XamarinDemo.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        private string displayName;
        private string name;
        private string description;
        private string phoneNumber;
        private DateTime dateOfBirth;
        private bool isColleague;

        public int Id { get; set; }

        public string DisplayName
        {
            get { return displayName; }
            set
            {
                SetValue(ref displayName, value);
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                SetValue(ref name, value);
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                SetValue(ref description, value);
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                SetValue(ref phoneNumber, value);
            }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                SetValue(ref dateOfBirth, value);
            }
        }

        public bool IsColleague
        {
            get { return isColleague; }
            set
            {
                SetValue(ref isColleague, value);
            }
        }
    }
}
