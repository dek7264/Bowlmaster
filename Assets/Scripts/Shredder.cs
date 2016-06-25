using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

    void OnTriggerExit(Collider collider)
    {
        GameObject collidingObj = collider.gameObject;
        if (collidingObj.GetComponentInParent<Pin>() != null)
        {
            Destroy(collidingObj.transform.parent.gameObject);
        }
    }

}
