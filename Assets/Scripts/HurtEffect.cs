using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HurtEffect : MonoBehaviour
{
    [SerializeField] private Image image;

    public void StartEffect()
    {
        image.color = new Color(1f, 0f, 0f, 0.2f);
        image.DOFade(0f, 0.33f).SetAutoKill(true);
    }
}
