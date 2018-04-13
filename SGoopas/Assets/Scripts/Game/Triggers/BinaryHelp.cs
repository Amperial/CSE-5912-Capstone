using System.Collections;
using UnityEngine;

public class BinaryHelp : BinaryTriggerable
{
    public string[] msg;
    private MasterMonoBehaviour ptr;

    public void Start()
    {
        ptr = MasterMonoBehaviour.Instance;
    }

    public override void Trigger()
    {
        base.Trigger();
        if (ptr != null)
        {
            ptr.DisplayMessage(msg);
        }
    }
}
