using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Text Text;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void InitHealthBar(int maxHp)
    {
        slider.maxValue = maxHp;

        SetHealth(maxHp);
    }

    public void SetHealth(int hp)
    {
        slider.value = hp;
        Text.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
    }
}
