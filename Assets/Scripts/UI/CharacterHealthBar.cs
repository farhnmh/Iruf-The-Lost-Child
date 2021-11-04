using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTemplateProjects.UI
{
    public class CharacterHealthBar : MonoBehaviour
    {
        [SerializeField] private CharacterData data;
        [SerializeField] private Image bar;
        [SerializeField] private float lerpSpeed = 0.2f;

        private void OnEnable() => data.OnHealthChanged += OnHealthChanged;

        private void OnDisable() => data.OnHealthChanged -= OnHealthChanged;

        private void Start()
        {
            bar.DOFillAmount(1f, lerpSpeed);
        }

        private void OnHealthChanged(float oldVal, float newVal)
        {
            bar.DOFillAmount(newVal / data.MaxHealth, lerpSpeed);
        }
    }
}