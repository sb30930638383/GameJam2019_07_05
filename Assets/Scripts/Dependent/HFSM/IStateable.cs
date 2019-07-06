using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public interface IStateable
    {
        //HFSM HFSMAttr { get; }
        bool Post(string msg);
        bool Post(HFSMEvent evt);
    }
}
