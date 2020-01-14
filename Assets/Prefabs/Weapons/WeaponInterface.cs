// The weapon interface is used so that we don't have to worry about what weapon we are using
using UnityEngine;

abstract public class WeaponInterface : MonoBehaviour
{
    // Should be called only during 1st frame of pressing a button
    public virtual void OnPress()
    {
    }

    // Called on every frame that the key is hold (including the first and last frame)
    public virtual void OnHold()
    {
    }

    // Called on the frame that the key is released
    public virtual void OnRelease()
    {
    }
}
