
using System.Security.Claims;
using AutoMapper;
using Azure;
using dotnet6_rpg.Data;
using dotnet6_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet6_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        //  public static List< Character> Characters=new List<Character>
        // {
        //     new Character(),
        //     new Character{ Id=1,Name="Sam"}

        // };
        private readonly IMapper _mapper;

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CharacterService(IMapper mapper,DataContext context,IHttpContextAccessor httpContextAccessor)
        {
          _context = context;
            _httpContextAccessor = httpContextAccessor;

            _mapper = mapper;

        }
private int GetUserId()=>int.Parse(_httpContextAccessor.HttpContext.User.
FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponce<List<GetCharacterDto> >>AddCharacter(AddCharacterDto newCharacter)
        {
            var ServiceResponce=new ServiceResponce<List<GetCharacterDto>>();
            Character character=_mapper.Map<Character>(newCharacter);

            character.User=await _context.Users.FirstOrDefaultAsync(u=>u.Id==GetUserId());
            _context.characters.Add(character);
            await _context.SaveChangesAsync();  
             //character.Id=Characters.Max(c=>c.Id)+1;
         // Characters.Add(character);
           ServiceResponce.Data= await _context.characters
           .Where(c=>c.User.Id==GetUserId())
           .Select(c=>_mapper.Map<GetCharacterDto>(c))
           .ToListAsync();
            return ServiceResponce;
        }

        public  async Task<ServiceResponce<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            //throw new NotImplementedException();
             ServiceResponce<List<GetCharacterDto>>responce=new ServiceResponce<List<GetCharacterDto>>();
           try{
           
       Character character= await _context.characters
       .FirstOrDefaultAsync(c=>c.Id==id &&c.User.Id==GetUserId());
       if(character !=null)
       {
          _context.characters.Remove(character);
       await _context.SaveChangesAsync();
       responce.Data=_context.characters
       .Where(c=>c.User.Id==GetUserId())
       .Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
       
       }else
       {
           responce.Sucess=false;
           responce.Message="Character not found";
       }
       
           }
           catch(Exception ex)
           {
         responce.Sucess=false;
         responce.Message=ex.Message;
           }
             return responce;
            }
        


        public  async Task<ServiceResponce< List<GetCharacterDto>>> GetAllCharacters()
        {
          var responce=new ServiceResponce<List<GetCharacterDto>>();
          var dbCharacters=await _context.characters
          .Where(c=>c.User.Id==GetUserId())
          .ToListAsync();
          responce.Data=dbCharacters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
          return responce;
           
        }

        public async Task<ServiceResponce<GetCharacterDto >>GetCharacterById(int id)
        {
            var ServiceResponse=new ServiceResponce<GetCharacterDto>();
            var dbCharacter= await _context.characters
            .FirstOrDefaultAsync(c=>c.Id==id && c.User.Id==GetUserId());
            ServiceResponse.Data=_mapper.Map<GetCharacterDto>(dbCharacter);
             return  ServiceResponse;
        }

        public async Task<ServiceResponce<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
           // throw new NotImplementedException();
           ServiceResponce<GetCharacterDto>responce=new ServiceResponce<GetCharacterDto>();
           try{

            var character= await _context.characters
            .Include(c=>c.User)
            .FirstOrDefaultAsync(c=>c.Id==updatedCharacter.Id);
            if(character.User.Id==GetUserId())
            {
            // _mapper.Map(updatedCharacter,character );
           character.Name=updatedCharacter.Name;
           character.HitPoints=updatedCharacter.HitPoints;
           character.Strength=updatedCharacter.Strength;
           character.Defence=updatedCharacter.Defence;
           character.Intelligence=updatedCharacter.Intelligence;
           character.Class=updatedCharacter.Class;

           await _context.SaveChangesAsync();

           responce.Data=_mapper.Map<GetCharacterDto>(character);
           }
           else{
            responce.Sucess=false;
            responce.Message="Character not found...";
           }
           }
           catch(Exception ex)
           {
         responce.Sucess=false;
         responce.Message=ex.Message;
           }
             return responce;
            }

        public  async Task<ServiceResponce<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
           // throw new NotImplementedException();
             var responce=new ServiceResponce<GetCharacterDto>();
             try{
              var character=await _context.characters
              .Include(c=>c.weapon)
              .Include(c=>c.skills)
              .FirstOrDefaultAsync(c=>c.Id==newCharacterSkill.CharacterId &&
              c.User.Id==GetUserId());

              if(character==null)
              {
                responce.Sucess=false;
                responce.Message="Character not found";
                return responce;
              }
              var skill=await _context.Skills.FirstOrDefaultAsync(s=>s.Id==newCharacterSkill.SkillId);
              if(skill==null)
              {
                responce.Sucess=false;
                responce.Message="skill not found";
                return responce;
              }
              character.skills.Add(skill);
              await _context.SaveChangesAsync();
              responce.Data=_mapper.Map<GetCharacterDto>(character);

             }catch(Exception ex)
             {
            responce.Sucess=false;
            responce.Message=ex.Message;
             }
             return responce;

        }

       
    }
}