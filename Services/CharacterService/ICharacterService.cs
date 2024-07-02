using dotnet6_rpg.Dtos.Character;

namespace dotnet6_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponce< List<GetCharacterDto>>>GetAllCharacters();
         Task<ServiceResponce<GetCharacterDto> >GetCharacterById(int id);
         Task<ServiceResponce<List<GetCharacterDto>>>AddCharacter(AddCharacterDto newCharacter);
         Task<ServiceResponce<GetCharacterDto>>UpdateCharacter(UpdateCharacterDto updatedCharacter);

          Task<ServiceResponce<List<GetCharacterDto>>>DeleteCharacter(int id);
          Task<ServiceResponce<GetCharacterDto>>AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);

    }
}