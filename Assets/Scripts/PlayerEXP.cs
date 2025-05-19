using UnityEngine;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    public static PlayerExperience Instance;

    public int currentXP = 0;
    public int level = 1;
    public int xpToNextLevel = 100;

    [SerializeField] private TextMeshProUGUI xpText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        UpdateXPText();
    }

    public void AddExperience(int amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        level++;
        xpToNextLevel += 50;
    }

    private void UpdateXPText()
    {
        if (xpText != null)
            xpText.text = "Level " + level + "   XP: " + currentXP + " / " + xpToNextLevel;
    }
}