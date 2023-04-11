using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Upgrades : MonoBehaviour
{
    public UpgradePath pathOne, pathTwo, pathThree;

    public void OnUpgrade(int index)
    {
        UpgradePath path = GetPath(index);
        if (path == null) { return; }

        UpgradePath.Upgrade upgrade = path.GetUpgrade();
        if (upgrade == null) { return; }

        if (!CanAfford(upgrade.cost)) { return; }

        Main.Current.money -= upgrade.cost;
        path.Index++;

        path.OnUpgrade();
    }

    private UpgradePath GetPath(int index)
    {
        return index switch
        {
            1 => pathOne,
            2 => pathTwo,
            3 => pathThree,
            _ => null
        };
    }

    private bool CanAfford(float cost)
    {
        return Main.Current.money - cost >= 0f;
    }

    public virtual void OnPathOneUpgrade()
    {

    }

    public virtual void OnPathTwoUpgrade()
    {

    }

    public virtual void OnPathThreeUpgrade()
    {

    }
}
