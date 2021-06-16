using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionMode
{
    public abstract void HandleZeroHits();
    public abstract void HandleInteraction(RaycastHit[] results, int hits, RaycastHit closestHit);
    public abstract void HandleInteractionUp();
    public abstract void HandleInteractionDown();
}
