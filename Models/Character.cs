namespace dotnet6_rpg.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }="Gayatri";
        public  int HitPoints { get; set; }=100;
        public int Strength { get; set; }=10;
        public int Defence { get; set; }=10;
        public int Intelligence{ get; set; }=10;
        public RpgClass Class{get;set;}=RpgClass.Knight;
        public User?User { get; set; }
        public Weapon weapon{get;set;}
        public List<Skill>skills{get;set;}
     
    }
}