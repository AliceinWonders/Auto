using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.GraphQL.GraphTypes;
using DefaultNamespace;
using GraphQL;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Mutations
{
    public class AutoOwnerMutation : ObjectGraphType
    {
        private readonly IAutoDatabase _db;

        public AutoOwnerMutation(IAutoDatabase db)
        {
            _db = db;

            Field<AutoOwnerGraphType>(
                "createAutoOwner",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "name"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "surname"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "number"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "email"},
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "autoId"}
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var surname = context.GetArgument<string>("surname");
                    var number = context.GetArgument<string>("number");
                    var email = context.GetArgument<string>("email");
                    var autoId = context.GetArgument<string>("autoId");
                    
                    var vehicle = _db.FindVehicle(autoId);
                    var autoOwner = new AutoOwner
                    {
                        Name = name,
                        Surname = surname,
                        Number = number,
                        Email = email,
                        AutoOwnerVehicle = vehicle
                    };
                    _db.CreateAutoOwner(autoOwner);
                    return autoOwner;
                    
                }
            );
        }
    }
}