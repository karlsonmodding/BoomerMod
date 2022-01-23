using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BoomerMod
{
    class BM_MonoHooks : MonoBehaviour
    {
        public static BM_MonoHooks instance;

        public void Start()
        {
            instance = this;
        }
    }
}
