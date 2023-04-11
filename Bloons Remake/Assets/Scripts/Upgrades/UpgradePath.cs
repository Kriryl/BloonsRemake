using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradePath : MonoBehaviour
{
    [Serializable]
    public class Upgrade
    {
        public float cost;
    }

    public Upgrade upgradeOne, upgradeTwo, upgradeThree;

    public int Index { get; set; } = 0;

    public Upgrade GetUpgrade()
    {
        return Index switch
        {
            0 => upgradeOne,
            1 => upgradeTwo,
            2 => upgradeThree,
            _ => null,
        };
    }

    public virtual void OnUpgrade()
    {
        print("upgraded!");
    }
}
