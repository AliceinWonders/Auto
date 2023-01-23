using System;
using System.Runtime.CompilerServices;
using Auto.Data.Entities;

namespace Auto.Messages
{
    public class NewAutoOwnerMessage
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string AutoId { get; set; }
        public string ModelName{ get; set; }
        public DateTimeOffset CreatedAt{ get; set; }
        
        public NewAutoOwnerMessage(){}
    }

}