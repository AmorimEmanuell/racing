#pragma warning disable 0649
using UnityEngine;

public class StarPanel : MonoBehaviour
{
    [SerializeField] private Star[] _stars;

    private void OnEnable()
    {
        GameData.CurrentObjective.OnChange += UpdateStars;
        UpdateStars(GameData.CurrentObjective.Get());
    }

    private void OnDisable()
    {
        GameData.CurrentObjective.OnChange -= UpdateStars;
    }

    private void UpdateStars(Objective objective)
    {
        for (var i = 0; i < _stars.Length; i++)
        {
            _stars[i].SetActive(!(i < objective.Current));
        }
    }
}
