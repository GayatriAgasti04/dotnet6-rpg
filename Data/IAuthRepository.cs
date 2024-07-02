namespace dotnet6_rpg.Data
{
    public interface IAuthRepository
    {
          Task<ServiceResponce<int>>Register(User user,string password);
           Task<ServiceResponce<string >>Login(string username,string password);
            Task<bool>UserExits(string username);
    }
}