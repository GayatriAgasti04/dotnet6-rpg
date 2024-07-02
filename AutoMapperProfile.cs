using AutoMapper;
using dotnet6_rpg.Dtos.Character;
using dotnet6_rpg.Dtos.Skill;
using dotnet6_rpg.Dtos.Weapon;
using dotnet6rpg.Migrations;
using Weapon = dotnet6_rpg.Models.Weapon;


namespace dotnet6_rpg
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character,GetCharacterDto>();
             CreateMap<AddCharacterDto,Character>();
             CreateMap<UpdateCharacterDto,Character>();
             CreateMap<Weapon,GetWeaponDto>();
             CreateMap<skill,GetSkillDto>();
            
        }
    }
}