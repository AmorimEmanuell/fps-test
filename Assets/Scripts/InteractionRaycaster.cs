using System.Collections.Generic;
using UnityEngine;

public class InteractionRaycaster : MonoBehaviour
{
    [SerializeField] private Transform myTransform;
    [SerializeField] private LayerMask placeModeLayerMask;
    [SerializeField] private LayerMask selectionModeLayerMask;

    private const int RaycastBuffer = 5;
    private const float MaxDistance = 10f;
    private const string MainButtonName = "Fire1";

    private readonly RaycastHit[] results = new RaycastHit[RaycastBuffer];
    private readonly Dictionary<Mode, InteractionMode> interactionModes = new Dictionary<Mode, InteractionMode>();

    private InteractionMode activeMode;

    public enum Mode
    {
        Place,
        Selection
    }

    private void Awake()
    {
        interactionModes.Add(Mode.Place, new PlaceInteractionMode(this, myTransform, placeModeLayerMask));
        interactionModes.Add(Mode.Selection, new SelectionInteractionMode(selectionModeLayerMask));

        activeMode = interactionModes[Mode.Selection];
    }

    private void Update()
    {
        var ray = new Ray(myTransform.position, myTransform.forward);
        var hits = Physics.RaycastNonAlloc(ray, results, MaxDistance, activeMode.GetLayerMask());

        if (hits == 0)
        {
            activeMode.HandleZeroHits();
            return;
        }

        var hit = GetClosestHit(results, hits);

        activeMode.HandleInteraction(results, hits, hit);

        if (Input.GetButtonDown(MainButtonName))
        {
            activeMode.HandleInteractionDown();
        }
        else if (Input.GetButtonUp(MainButtonName))
        {
            activeMode.HandleInteractionUp();
        }
    }

    public void SwitchMode(Mode mode)
    {
        activeMode = interactionModes[mode];
    }

    private RaycastHit GetClosestHit(RaycastHit[] results, int hits)
    {
        var closestDistance = Mathf.Infinity;
        var closestIndex = 0;

        for (var i = 0; i < hits; i++)
        {
            if (results[i].distance < closestDistance)
            {
                closestDistance = results[i].distance;
                closestIndex = i;
            }
        }

        return results[closestIndex];
    }
}
