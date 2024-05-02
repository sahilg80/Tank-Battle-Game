using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TankPlayerPool : GenericObjectPool<TankPlayerController>
    {

        //public TankPlayerController GetTankPlayer() => GetItem();

        //public void ReturnTankPlayerToPool(TankPlayerController controller) => ReturnItem(controller);

        //protected override TankPlayerController CreateItem() => new TankPlayerController();
    }
}
