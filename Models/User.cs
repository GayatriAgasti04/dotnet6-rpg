namespace dotnet6_rpg.Models
{
    public class User
    {
        public int Id{get;set;}
        public string  UserName { get; set; }=string.Empty;
        public byte []PasswordHash{get;set;}
        public byte []PasswordSalt{get;set;}
        public List< Character>?Characters { get; set; }
    }
}