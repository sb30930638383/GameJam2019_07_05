using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public static class Global
    {
        public static Canvas Canvas { get; set; }

        public static Camera MainCamera { get; set; }

        public static PlayerEntity Player { get; set; }

        public static bool IsPerform
        {
            get { return isPerform > 0; }
            set { isPerform += value ? 1 : -1; }
        }
        private static int isPerform = 0;
    }
}