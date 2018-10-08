using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIFSMState
{
    protected AIStateManager stateManager;

    public AIFSMState()
    {
    }

    public virtual void Act()
    {

    }
}
