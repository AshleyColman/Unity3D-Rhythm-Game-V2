namespace UIScripts
{
    using UnityEngine;
    using TMPro;

    public sealed class FieldButton : MonoBehaviour
    {
        [SerializeField] private EffectText fieldText = default;
        [SerializeField] private EffectText valueText = default;
        [SerializeField] private FlashEffect flashEffect = default;
    }
}