namespace CDNUserMgtWeb.Model
{
    public class FreelanceUser
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Skillsets { get; set; }
        public List<string> Hobby { get; set; }

    }
}
