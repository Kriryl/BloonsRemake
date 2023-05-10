using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PlayerUpgrades
{
    public class Upgrades : MonoBehaviour
    {
        [Serializable]
        public class Path
        {
            public List<Upgrade> upgrades = new();

            public int Index { get; private set; } = 0;

            public Upgrade PlaceHolder;

            public Upgrade GetUpgrade()
            {
                return Index > upgrades.Count - 1 ? PlaceHolder : upgrades[Index];
            }

            public void Next()
            {
                Index++;
            }
        }

        [Serializable]
        public class Upgrade
        {
            public float cost;
            public string upgradeName = "";
            public string description = "";
        }

        public Path pOne, pTwo, pThree;

        public GameObject upgradeContainer;

        public Player Player { get; private set; }

        public List<Path> Paths { get; set; } = new();

        private void Awake()
        {
            Paths.Add(pOne);
            Paths.Add(pTwo);
            Paths.Add(pThree);

            Player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            if (!Main.MenuOpen)
            {
                upgradeContainer.SetActive(false);
                return;
            }
            upgradeContainer.SetActive(true);
        }

        public virtual void OnPathOneUpgrade(int index)
        {

        }

        public virtual void OnPathTwoUpgrade(int index)
        {

        }

        public virtual void OnPathThreeUpgrade(int index)
        {

        }

    }
}
