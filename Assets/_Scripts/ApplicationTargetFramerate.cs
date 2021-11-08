using UnityEngine;

public class ApplicationTargetFramerate : MonoBehaviour
{
    [SerializeField] private int _target = 90;

    private void Awake()
    {
        Application.targetFrameRate = _target;
    }
}
