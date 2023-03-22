using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _comboText;


    public void SetLevel(int level)
    {
        _levelText.text = level.ToString();
    }

    public void SetCombo(int combo)
    {
        _comboText.text = combo.ToString();
    }

}
