using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class poketmonUI : MonoBehaviour
{
    public poketmon poketmon;

    public Image poketmon_image;
    public Slider poketmon_hp;
    public Slider poketmon_exp;
    public Text poketmon_name;
    public Text poketmon_level;

    public GameObject poketmon_UI;

    private void Start()
    {
        poketmon_image.sprite = poketmon.image;
        poketmon_name.text = poketmon.name;
        poketmon_level.text = poketmon.level;

        poketmon_hp.maxValue = poketmon.max_hp;
        poketmon_hp.value = poketmon.hp;
        poketmon_exp.maxValue = poketmon.max_exp;
        poketmon_exp.value = poketmon.exp;
    }
}
