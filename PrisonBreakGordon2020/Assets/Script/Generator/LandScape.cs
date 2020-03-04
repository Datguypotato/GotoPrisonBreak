using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LandScape : MonoBehaviour
{
    public abstract void Generate();
    public virtual void Clean()
    {

    }
}
