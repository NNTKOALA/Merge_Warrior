using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCard : MonoBehaviour
{
    public Text nameText;
    public Text attackText;
    public Text healthText;

    public void UpdateCharacterData(string name, int attack, int health)
    {
        nameText.text = " " + name;
        attackText.text = " " + attack.ToString();
        healthText.text = " " + health.ToString();
    }
}
