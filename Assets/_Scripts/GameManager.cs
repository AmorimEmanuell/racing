using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        GameData.ElapsedTime.Set(GameData.ElapsedTime.Get() + Time.deltaTime);
    }
}
