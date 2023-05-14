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

            public bool Locked { get; set; }

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

        public int usedUpgrades = 0;
        public List<Path> unlocked = new();

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
            if (index == 0)
            {
                unlocked.Add(Paths[0]);
                usedUpgrades++;
                if (usedUpgrades >= 2)
                {
                    CheckLocked(Paths[1], Paths[2]);
                }
            }
        }

        public virtual void OnPathTwoUpgrade(int index)
        {
            if (index == 0)
            {
                unlocked.Add(Paths[1]);
                usedUpgrades++;
                if (usedUpgrades >= 2)
                {
                    CheckLocked(Paths[0], Paths[2]);
                }
            }
        }

        public virtual void OnPathThreeUpgrade(int index)
        {
            if (index == 0)
            {
                unlocked.Add(Paths[2]);
                usedUpgrades++;
                if (usedUpgrades >= 2)
                {
                    CheckLocked(Paths[0], Paths[1]);
                }
            }
        }

        private void CheckLocked(Path check, Path check2)
        {
            if (unlocked.Contains(check))
            {
                check2.Locked = true;
            }
            else if (unlocked.Contains(check2))
            {
                check.Locked = true;
            }
        }
    }
}
