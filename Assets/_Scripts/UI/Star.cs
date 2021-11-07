#pragma warning disable 0649
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject _starFill;

    public void SetActive(bool active)
    {
        _starFill.SetActive(active);
    }
}
