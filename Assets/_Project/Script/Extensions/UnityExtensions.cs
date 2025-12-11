using System.Collections.Generic;
using UnityEngine;

namespace _Project.Script.Extensions
{
    public static class UnityExtensions
    {
        public static T1 GetNearest<T1,T2>(this IEnumerable<T1> components, T2 owner) where T1 : Component where T2 : Component
        {
            double min = double.MaxValue;
            T1 nearest = null;
            Vector3 ownerPos = owner.transform.position;

            foreach (var component in components)
            {
                if (component == null) continue;
                
                var dist = Vector3.Distance(ownerPos, component.transform.position);

                if (dist < min)
                {
                    min = dist;
                    nearest = component;
                }
            }

            return nearest;
        }
    }
}
