using UnityEngine;
using UnityEngine.EventSystems;

public class CustomBaseInput : BaseInput
{
    public override Vector2 mousePosition => new Vector2(Screen.width/2, Screen.height/2);
}
