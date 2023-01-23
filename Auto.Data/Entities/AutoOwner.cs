using Auto.Data.Entities;

namespace DefaultNamespace
{
    public class AutoOwner
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string AutoId { get; set; }
        public virtual  Vehicle AutoOwnerVehicle { get; set; }
        
    }
}