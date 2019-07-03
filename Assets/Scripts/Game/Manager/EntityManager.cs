using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class EntityManager
    {
        public static EntityManager Inst { get { return inst; } }
        private static EntityManager inst = new EntityManager();

        private Dictionary<int, EntityBase> entityDict = new Dictionary<int, EntityBase>();
    }
}