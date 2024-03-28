using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUiEffectPos
{
    public Vector2 normalPos { get; set; }
    public Vector2 effectPos { get; set; }
    public Vector2 effectScale { get; set; }
}
