using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace PlayerUpgrades
{
    public class UpgradePath : MonoBehaviour
    {
        [Serializable]
        public class Upgrade
        {
            public float cost;

            public string upgradeName = "New Upgrade";
            public string upgradeDescription = "New Description";

            public float fValue = 0f;
            public int iValue = 0;
        }

        private TextMeshProUGUI buyText, nameText, descriptionText;
        public List<Upgrade> upgrades = new();

        public int Index { get; set; } = 0;

        public bool mouseOver = false;

        public Player Player { get; private set; }

        private void Start()
        {
            Player = FindObjectOfType<Player>();

            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
            try
            {
                buyText = texts[0];
                nameText = texts[1];
                descriptionText = texts[2];
            }
            catch
            {
                print("Could not find text component in children!");
            }
        }

        public void Update()
        {
            Upgrade upgrade = GetUpgrade();
            if (upgrade == null) { return; }

            if (Main.Current.money - upgrade.cost >= 0f)
            {
                buyText.text = mouseOver ? "BUY" : $"${upgrade.cost}";
            }
            else
            {
                buyText.text = mouseOver ? "Can't Afford" : $"${upgrade.cost}";
            }

            nameText.text = upgrade.upgradeName;
            descriptionText.text = upgrade.upgradeDescription;
        }

        public Upgrade GetUpgrade()
        {
            try
            {
                return upgrades[Index];
            }
            catch
            {
                return null;
            }
        }

        public virtual void OnUpgrade(Upgrade currentUpgrade)
        {
            Index++;
        }
    }
}