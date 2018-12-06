using UnityEngine;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour
{

    public Text maxHPText;
    public Text currentHPText;
    public Text defenseText;
    public Text offenseText;
    public Text damageText;

    private void Start()
    {
        gameObject.GetComponent<Image>().enabled = false;
        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        foreach (Text t in texts)
        {
            t.enabled = false;
        }
    }

    public void DisplayStats(int maxHP, int currentHP, int defense, int offense, int damage)
    {
        gameObject.GetComponent<Image>().enabled = true;

        Text[] texts = gameObject.GetComponentsInChildren<Text>(); 
        foreach(Text t in texts)
        {
            t.enabled = true; 
        }

        //maxHPText.text = maxHP.ToString();
        currentHPText.text = currentHP.ToString() + " / " + maxHP.ToString();
        defenseText.text = defense.ToString();
        offenseText.text = offense.ToString();
        damageText.text = damage.ToString();
    }

    public void HideStats()
    {
        gameObject.GetComponent<Image>().enabled = false;

        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        foreach (Text t in texts)
        {
            t.enabled = false;
        }
    }
}