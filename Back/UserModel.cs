namespace Back
{
    public class UserModel
    {
        public String username { get; set; } = string.Empty;
        public byte[] Passwordhash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
