#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

public class StarPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _myRectTransform;
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private GameObject[] _stars;

    public void ShowStars(int objective)
    {
        for (var i=0; i< _stars.Length; i++)
        {
            _stars[i].SetActive(!(i < objective));
        }

        _layoutGroup.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_myRectTransform);
        _layoutGroup.enabled = false;
    }
}
