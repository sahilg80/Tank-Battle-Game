using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    public float CurrentHealth { get; set; }
    private float maxHealth;
    public bool IsEnemy { get; set; }
    public bool IsLocalPlayer;

    // Start is called before the first frame update
    void OnEnable()
    {
        maxHealth = 100f;
        CurrentHealth = maxHealth;
        healthBar.fillAmount = 1;
    }

    private void Start()
    {
        IsLocalPlayer = GetComponent<PlayerController>().IsLocalPlayer;
    }


    public void TakeDamage(GameObject playerFrom,float value)
    {
        Debug.Log("value " + value + "curent health " + CurrentHealth);
        UpdateHealth(value, playerFrom);
    }


    // Update is called once per frame
    private void UpdateHealth(float damage, GameObject playerFrom)
    {
        CurrentHealth = CurrentHealth - damage;
        NetworkManager.Instance.CommandHealthChange(playerFrom, this.gameObject, damage, IsEnemy);
        //OnHealthChange();
    }

    public void OnHealthChange()
    {
        UpdateHealthBar();
        CheckHealthStatus();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount =  (CurrentHealth / maxHealth);
    }

    private void CheckHealthStatus()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        healthBar.fillAmount = 0;
    }
}
