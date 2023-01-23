using Auto.Data;
using Auto.Website.GraphQL.Mutations;
using Auto.Website.GraphQL.Queries;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Schemas
{
    public class VehicleSchema : Schema
    {
        public VehicleSchema(IAutoDatabase db)
        {
            Query = new VehicleQuery(db);
            Mutation = new VehicleMutation(db);
        }
    }
}