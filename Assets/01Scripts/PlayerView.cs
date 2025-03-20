using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Slider _hpSlider;
    [SerializeField] TMP_Text _hpText;
    [SerializeField] Slider _mpSlider;
    [SerializeField] TMP_Text _mpText;

    public void SetHP(int hp)
    {
        _hpSlider.value = hp;
        _hpText.text = hp.ToString();
    }

    public void SetMP(int mp)
    {
        _mpSlider.value = mp;
        _mpText.text = mp.ToString();
    }
    
}
