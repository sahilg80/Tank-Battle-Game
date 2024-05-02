using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class TankPlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private Image healthBar;
        private float currentHealth;
        public float CurrentHealth => currentHealth;
        private float maxHealth;
        private TankPlayerController tankPlayerController;

        void OnEnable()
        {
            maxHealth = 100f;
            currentHealth = maxHealth;
            healthBar.fillAmount = 1;
        }

        public void SetController(TankPlayerController controller) => tankPlayerController = controller;

        public void TakeDamage(float damage, string idOfPlayerFrom)
        {
            currentHealth = currentHealth - damage;
            UpdateHealthBar();
            UpdateConnectedTankPlayersHealth();
            CheckHealthStatus(idOfPlayerFrom);
        }

        public void SetHealth(float health)
        {
            currentHealth = health;
            UpdateHealthBar();
        }

        // Update is called once per frame
        private void UpdateConnectedTankPlayersHealth()
        {
            tankPlayerController.UpdateConnectedTankPlayersHealth(currentHealth);
            //NetworkManager.Instance.CommandHealthChange(this.gameObject, damage);
            //OnHealthChange();
        }

        private void UpdateHealthBar() => healthBar.fillAmount = (currentHealth / maxHealth);

        private void CheckHealthStatus(string idOfPlayerFrom)
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                tankPlayerController.PlayerKilled(idOfPlayerFrom);
                //Destroy(this.gameObject);
            }
        }
    }
}
