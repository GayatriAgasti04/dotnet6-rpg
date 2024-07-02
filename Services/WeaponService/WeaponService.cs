using System.Security.Claims;
using AutoMapper;
using dotnet6_rpg.Data;
using dotnet6_rpg.Dtos.Character;
using dotnet6_rpg.Dtos.Weapon;
using Microsoft.EntityFrameworkCore;

namespace dotnet6_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public WeaponService(DataContext context ,IHttpContextAccessor httpContextAccessor,IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            
        }
        public  async Task<ServiceResponce<GetCharacterDto>> AddWeapon(AddWeaponDto newweapon)
        {
           // throw new NotImplementedException();
           ServiceResponce<GetCharacterDto>responce=new ServiceResponce<GetCharacterDto>();
           try{
            Character character=await _context.characters
            .FirstOrDefaultAsync(c=>c.Id==newweapon.CharacterId &&
            c.User.Id==int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
         if(character == null)
         {
          responce.Sucess=false;
          responce.Message="Character not found";
          return responce;
         }
         Weapon weapon= new Weapon
         {
            Name=newweapon.Name,
            Damage=newweapon.Damage,
            character=character
             };
         _context.weapons.Add(weapon);
         await _context.SaveChangesAsync();
         responce.Data=_mapper.Map<GetCharacterDto>(character);
}
catch( Exception ex)
           {
            responce.Sucess=false;
            responce.Message=ex.Message;

           }
           return  responce;
        }

    }
}