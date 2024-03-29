﻿using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes
{
    public class ModelGraphType : ObjectGraphType<Model>
    {
        public ModelGraphType()
        {
            Name = "model";
            Field(m => m.Name).Description("The name of this model, e.g. Golf, Beetle, 5 Series, Model X");
            Field(m => m.Manufacturer, type: typeof(ManufactureGraphType))
                .Description("The make of this model of car");
        }
    }
}