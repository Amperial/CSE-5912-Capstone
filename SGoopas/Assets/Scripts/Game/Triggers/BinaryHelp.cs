using System.Collections;
using UnityEngine;

public class BinaryHelp : BinaryTriggerable
{
    public string[] msg;
    private MasterMonoBehaviour ptr;
    private float elapsed;
    private bool isTrig;
    public void Start()
    {
        isTrig = false;
        elapsed = 0f;
        ptr = MasterMonoBehaviour.Instance;
    }

    public override void Trigger()
    {
        base.Trigger();
        if (ptr != null && !isTrig)
        {
            ptr.DisplayMessage(msg);
        }
    }
}
