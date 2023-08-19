using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBladeSword : MonoBehaviour
{
    public BoxCollider checkCollider;
    public Animator anim;
    public void Slice()
    {
        BoulderBladeManager.Instance.player.OnSwing?.Invoke();
        foreach (Collider boulder in GetCollidersWithTag(checkCollider, "Boulder"))
        {
            boulder.GetComponent<Boulder>().Break(false);
        }
    }

    Collider[] GetCollidersWithTag(BoxCollider collider, string tag)
    {
        Bounds colliderBounds = collider.bounds;
        Collider[] collidersInsideBox = Physics.OverlapBox(colliderBounds.center, colliderBounds.extents);

        List<Collider> collidersWithTag = new List<Collider>();

        foreach (Collider col in collidersInsideBox)
        {
            if (col.CompareTag(tag))
            {
                collidersWithTag.Add(col);
            }
        }

        return collidersWithTag.ToArray();
    }


}
