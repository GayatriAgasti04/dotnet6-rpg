using System.Security.Claims;
using dotnet6_rpg.Dtos.Character;
using dotnet6_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace dotnet6_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Character")]
    public class CharacterController:ControllerBase
    {
        private readonly ICharacterService _characterService;
       public CharacterController(ICharacterService characterService)
       {
            _characterService = characterService;
        
       }
        //[AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> Get()
        {
           // int userId=int.Parse(User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier).Value);

            return Ok( await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task< ActionResult<ServiceResponce<GetCharacterDto>>>GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

         [HttpDelete("{id}")]
        public async Task< ActionResult<ServiceResponce<List<GetCharacterDto>>>>Delete(int id)
        {
            //return Ok(await _characterService.GetCharacterById(id));
            var response=await _characterService.DeleteCharacter(id);
            if(response.Data==null)
            {
                return NotFound(response);
            }
           
            return Ok (response);
        }
        
        [HttpPost]   
      public  async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>>AddCharacter(AddCharacterDto newCharacter)
        {
           
            return Ok ( await _characterService.AddCharacter(newCharacter));
        }
        [HttpPut]   
      public  async Task<ActionResult<ServiceResponce<GetCharacterDto>>>UpdateCharacter(UpdateCharacterDto  updatedCharacter)
        {
            var response=await _characterService.UpdateCharacter(updatedCharacter);
            if(response.Data==null)
            {
                return NotFound(response);
            }
           
            return Ok (response);
        }
        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponce<GetCharacterDto>>>AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            return Ok(await _characterService.AddCharacterSkill(newCharacterSkill));
            
        }
    }
}