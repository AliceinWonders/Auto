using System.Collections.Generic;
using System.Linq;
using Auto.Data;
using Auto.Website.GraphQL.GraphTypes;
using Auto.Website.GraphQL.Mutations;
using Auto.Website.GraphQL.Queries;
using Auto.Website.GraphQL.Schemas;
using DefaultNamespace;
using GraphQL;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Queries
{
    public class AutoOwnerQuery : ObjectGraphType
    {
        private readonly IAutoDatabase _db;

        public AutoOwnerQuery(IAutoDatabase db)
        {
            _db = db;

            Field<ListGraphType<AutoOwnerGraphType>>("AutoOwners", "Query to retrieve all autoOwners",
                resolve: GetAllAutoOwners);
            Field<AutoOwnerGraphType>("AutoOwner", "Query to retrieve a specific owner by phone number",
                new QueryArguments(MakeNonNullStringArgument("number", "Phone number to find user by")),
                resolve: GetAutoOwner);
            Field<ListGraphType<AutoOwnerGraphType>>("AutoOwnersByEmail", "Query to retrieve a specific owner by email",
                new QueryArguments(new QueryArgument<StringGraphType>
                {
                    Name = "email",
                    Description = "Email of desirable owners"
                }),
                resolve: GetAutoOwnersByEmail);
        }

        private QueryArgument MakeNonNullStringArgument(string name, string description)
        {
            return new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = name, Description = description
            };
        }

        private IEnumerable<AutoOwner> GetAllAutoOwners(IResolveFieldContext<object> context) => _db.ListAutoOwners();

        private AutoOwner GetAutoOwner(IResolveFieldContext<object> context)
        {
            var number = context.GetArgument<string>("number");
            return _db.FindAutoOwner(number);
        }

        private IEnumerable<AutoOwner> GetAutoOwnersByEmail(IResolveFieldContext<object> context)
        {
            var email = context.GetArgument<string>("email");
            var autoOwners = _db.ListAutoOwners()
                .Where(o => o.Email == email);
            return autoOwners;
        }
    }
}
            