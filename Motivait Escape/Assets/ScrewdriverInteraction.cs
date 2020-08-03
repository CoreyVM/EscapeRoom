using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverInteraction : MonoBehaviour
{
    public CharacterMovement PlayerRef;

    public void Interact()
    {
        PlayerRef.hasScrewdriver = true;
        Destroy(this);
        this.transform.gameObject.GetComponent<Renderer>().enabled = false;
 
    }
}
