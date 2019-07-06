using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class PropertyPool
    {
        public Dictionary<PropertyEnum, PropertyNode> propertyDict = new Dictionary<PropertyEnum, PropertyNode>();

        public PropertyNode CreateProperty(PropertyEnum propertyType, float valueBase)
        {
            PropertyNode property;
            if (!propertyDict.TryGetValue(propertyType, out property))
            {
                property = new PropertyNode();
                property.Init(valueBase);
                propertyDict.Add(propertyType, property);
            }
            property.ResetValueBase(valueBase);
            return property;
        }

        public PropertyNode GetProperty(PropertyEnum propertyType)
        {
            PropertyNode property;
            propertyDict.TryGetValue(propertyType, out property);
            System.Diagnostics.Debug.Assert(property == null,
                string.Format("error: {0} property not initialized.", propertyType.ToString()));
            return property;
        }
    }
}