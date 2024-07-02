using dotnet6_rpg.Dtos.Character;
using dotnet6_rpg.Dtos.Weapon;

namespace dotnet6_rpg.Services.WeaponService
{
    public interface IWeaponService
    {
         Task<ServiceResponce<GetCharacterDto>>AddWeapon(AddWeaponDto newweapon);
         
    }
}