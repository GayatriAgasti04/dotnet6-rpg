namespace dotnet6_rpg.Models
{
    public class ServiceResponce<T>
    {
        public T ? Data{ get; set; }
        public bool Sucess { get; set; }=true;
        public string Message { get; set; }=string.Empty;
        
        
    }
}