
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace dotnet6_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;


        public AuthRepository(DataContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public  async Task<ServiceResponce<string>> Login(string username, string password)
        {
            //throw new NotImplementedException();
            var responce=new ServiceResponce<string>();
            var user=await _context.Users
            .FirstOrDefaultAsync(u=>u.UserName.ToLower().Equals(username.ToLower()));
            if(user==null)
            {
                responce.Sucess=false;
                responce.Message="User not Found.....";
            }
            else if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                 responce.Sucess=false;
                responce.Message="Wrong Password.....";
            }
            else{
                responce.Data=CreateToken(user);
            }
            return responce;
            
        }

        public async Task<ServiceResponce<int>> Register(User user, string password)
        {
             ServiceResponce<int>responce=new ServiceResponce<int>();
               if(await UserExits(user.UserName))
               {
                responce.Sucess=false;
                responce.Message="User Already exits.......";
                return responce;
               }

            CreatePasswordHash(password,out byte[]passwordHash,out byte[]passwordSalt);
             user.PasswordHash=passwordHash;
             user.PasswordSalt=passwordSalt;

           // throw new NotImplementedException();
           _context.Users.Add(user);
           await _context.SaveChangesAsync();
          
           responce.Data=user.Id;
           return responce;
        }

        public  async Task<bool> UserExits(string username)
        {
            //throw new NotImplementedException();
            if(await _context.Users.AnyAsync(u=>u.UserName.ToLower()==username.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password,out byte []passwordHash,out byte[]PasswordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt=hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private bool VerifyPasswordHash(string password,byte[]passwordHash,byte[]Passwordsalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(Passwordsalt))
            {
              var ComputeHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
              return ComputeHash.SequenceEqual(passwordHash);
            }
           
        }
        private string CreateToken(User user)
        {
            List<Claim>claims=new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            SymmetricSecurityKey key=new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor=new SecurityTokenDescriptor
            {
              Subject=new ClaimsIdentity(claims),
              Expires=DateTime.Now.AddDays(1),
              SigningCredentials=creds

            };
            JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
        SecurityToken token=tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);//token

        }

    }
}