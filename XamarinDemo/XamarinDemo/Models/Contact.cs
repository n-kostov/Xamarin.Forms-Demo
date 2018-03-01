namespace XamarinDemo
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Photo
        {
            get
            {
                return $"http://lorempixel.com/50/50/sports/{Id}";
            }
        }
    }
}
