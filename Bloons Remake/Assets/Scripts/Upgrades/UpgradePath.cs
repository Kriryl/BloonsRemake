using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace PlayerUpgrades
{
    public enum Path { PathOne, PathTwo, PathThree }

    public class UpgradePath : MonoBehaviour
    {
        public Path path;
        
        private Upgrades upgrades;

        public TextMeshProUGUI upgradeName;
        public TextMeshProUGUI upgradeDescription;
        public TextMeshProUGUI buttonText;
        public Button upgradeButton;

        public bool isLocked = false;

        public float Money { get => Main.Current.money; set => Main.Current.money = value; }

        public Upgrades.Upgrade CurrentUpgrade { get; set; }

        private void Start()
        {
            upgrades = FindObjectOfType<Upgrades>();
        }

        private void Update()
        {
            Upgrades.Path path = GetPath();

            if (path == null) { return; }

            isLocked = path.Locked;

            if (isLocked)
            {
                upgradeName.text = "Locked Path";
                upgradeDescription.text = "";
                buttonText.text = "Locked";

                Image i = upgradeButton.GetComponent<Image>();
                if (!i) { return; }
                i.color = Color.gray;
                return;
            }

            CurrentUpgrade = path.GetUpgrade();

            if (CurrentUpgrade == null) { return; }

            upgradeName.text = CurrentUpgrade.upgradeName;
            upgradeDescription.text = CurrentUpgrade.description;
            buttonText.text = $"${CurrentUpgrade.cost}";

            Image im = upgradeButton.GetComponent<Image>();
            if (!im) { return; }
            im.color = CanAfford(CurrentUpgrade.cost) ? Color.green : Color.red;
        }

        public void OnBuy()
        {
            if (isLocked) { return; }

            if (!upgrades) { return; }

            if (!CanAfford(CurrentUpgrade.cost)) { return; }

            CallPath();
        }

        public Upgrades.Path GetPath()
        {
            return path switch
            {
                Path.PathOne => upgrades.Paths[0],
                Path.PathTwo => upgrades.Paths[1],
                Path.PathThree => upgrades.Paths[2],
                _ => upgrades.Paths[0]
            };
        }

        private void CallPath()
        {
            Upgrades.Path pathIndex = GetPath();

            switch (path)
            {
                case Path.PathOne:
                    upgrades.OnPathOneUpgrade(pathIndex.Index);
                    break;
                case Path.PathTwo:
                    upgrades.OnPathTwoUpgrade(pathIndex.Index);
                    break;
                case Path.PathThree:
                    upgrades.OnPathThreeUpgrade(pathIndex.Index);
                    break;
                default:
                    break;
            }
            Main.Current.money -= pathIndex.GetUpgrade().cost;
            pathIndex.Next();
        }

        public bool CanAfford(float cost)
        {
            return Money - cost >= 0f;
        }
    }
}