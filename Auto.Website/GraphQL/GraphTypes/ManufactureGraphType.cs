using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes
{
    public sealed class ManufactureGraphType : ObjectGraphType<Manufacturer>
    {
        public ManufactureGraphType()
        {
            Name = "manufacturer";
            Field(c => c.Name).Description("The name of the manufacturer, e.g. Tesla, Volkswagen, Ford");
        }  
    }
}