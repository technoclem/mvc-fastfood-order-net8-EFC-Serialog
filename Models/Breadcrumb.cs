namespace FastFood.Models
{
    public class Breadcrumb
    {
        public string Title { get; set; }

        public string FaIcon { get; set; }

        public Breadcrumb(string title, string faIcon)
        {
            Title = title;
            FaIcon = faIcon;
        }
    }
}
