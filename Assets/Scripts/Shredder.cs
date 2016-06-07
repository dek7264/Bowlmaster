using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

    void OnTriggerExit(Collider collider)
    {
        GameObject collidingObj = collider.gameObject;
        //Debug.Log(collidingObj.name + " leaving play space");
        if (collidingObj.GetComponentInParent<Pin>() != null)
        {
            Destroy(collidingObj.transform.parent.gameObject);
        }
    }

}
