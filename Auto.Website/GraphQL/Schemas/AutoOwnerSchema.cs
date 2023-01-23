using Auto.Data;
using Auto.Website.GraphQL.Mutations;
using Auto.Website.GraphQL.Queries;
using DefaultNamespace;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Schemas
{
    public class AutoOwnerSchema : Schema
    {
        public AutoOwnerSchema(IAutoDatabase db)
        {
            Query = new AutoOwnerQuery(db);
            Mutation = new AutoOwnerMutation(db);
        }
    }
}