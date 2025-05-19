using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image healthGlobe, manaGlobe;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private PlayerExperience playerXP;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }   

    void Start()
    {
        if (playerXP == null)
            playerXP = GameObject.FindWithTag("Player").GetComponent<PlayerExperience>();
        UpdateXPBar();
    }

    void Update()
    {
        UpdateXPBar();
        healthGlobe.fillAmount = playerHealth.GetHealthRatio();
    }

    private void UpdateXPBar()
    {
        if (playerXP != null && xpSlider != null)
        {
            xpSlider.maxValue = playerXP.xpToNextLevel;
            xpSlider.value = playerXP.currentXP;
        }
    }
}
