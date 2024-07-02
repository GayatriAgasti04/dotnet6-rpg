using dotnet6_rpg.Dtos.Skill;
using dotnet6_rpg.Dtos.Weapon;

namespace dotnet6_rpg.Dtos.Character
{
    public class GetCharacterDto
    {
         public int Id { get; set; }
        public string Name { get; set; }="Gayatri";
        public  int HitPoints { get; set; }=100;
        public int Strength { get; set; }=10;
        public int Defence { get; set; }=10;
        public int Intelligence{ get; set; }=10;
        public RpgClass Class{get;set;}=RpgClass.Knight;
        public GetWeaponDto Weapon{get;set;}
        public List<GetSkillDto>Skills{get;set;}
    }
}