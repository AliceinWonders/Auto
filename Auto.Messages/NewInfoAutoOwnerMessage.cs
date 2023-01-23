using System;
using GraphQL;

namespace Auto.Messages
{
    public class NewInfoAutoOwnerMessage
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string AutoId { get; set; }
        public string ModelName{ get; set; }
        public DateTimeOffset CreatedAt{ get; set; }
        
        public int Income { get; set; }
        public int Age { get; set; }

        public NewInfoAutoOwnerMessage()
        {
            
        }

        public NewInfoAutoOwnerMessage(NewAutoOwnerMessage autoOwner, int income, int age)
        {
            this.Name = autoOwner.Name;
            this.Surname = autoOwner.Surname;
            this.Email = autoOwner.Email;
            this.Number = autoOwner.Number;
            this.AutoId = autoOwner.AutoId;
            this.ModelName = autoOwner.ModelName;
            this.CreatedAt = autoOwner.CreatedAt;
            this.Income = income;
            this.Age = age;
        }
    }
}