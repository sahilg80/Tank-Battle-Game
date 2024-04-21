using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        public abstract void RecieveDamage(float value);
    }
}
