using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputHandle : MonoBehaviour
{
    public abstract Vector2 GetMoveDir();
    public abstract Vector2 GetShootDir();
    public abstract bool GetShoot();
}
