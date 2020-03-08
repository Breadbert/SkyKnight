using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI levelText;
	public Slider hpSlider;

	public void SetHUD(Enemy unit)
	{
		nameText.text = unit.Name;
		levelText.text = "Lvl " + unit.Level;
		hpSlider.maxValue = unit.MaxHP;
		hpSlider.value = unit.HP;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}