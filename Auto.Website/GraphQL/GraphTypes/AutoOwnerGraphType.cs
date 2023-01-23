using Auto.Data.Entities;
using DefaultNamespace;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes
{
    public class AutoOwnerGraphType : ObjectGraphType<AutoOwner>
    {
        public AutoOwnerGraphType()
        {
            Name = "owner";
            Field(o => o.Name).Description("Owner's name");
            Field(o => o.Surname).Description("Owner's surname");
            Field(o => o.Number).Description("Owner's number");
            Field(o => o.Email).Description("Owner's email");
            Field(o => o.AutoOwnerVehicle, type: typeof(VehicleGraphType))
                .Description("Owner's vehicle");
        }  
    }
}