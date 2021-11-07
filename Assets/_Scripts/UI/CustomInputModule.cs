using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CustomBaseInput))]
public class CustomInputModule : StandaloneInputModule
{
    protected override void Start()
    {
        inputOverride = GetComponent<CustomBaseInput>();
        base.Start();
    }
}
