using UnityEngine;

public interface ISequenceControllable
{
    void LockControl();
    void UnlockControl();
    void SetVisible(bool visible);
    void SetWorldPosition(UnityEngine.Vector3 position);
}
