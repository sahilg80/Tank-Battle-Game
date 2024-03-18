using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BaseView : MonoBehaviour
    {
        public abstract void OnAttacked(float damage);
    }
}
