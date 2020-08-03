using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskDrawer : MonoBehaviour
{
    private enum DrawerState { IdleClosed, IdleOpened};
    private DrawerState drawerState;
    private Animator anim;

    private void Start()
    {
        drawerState = DrawerState.IdleClosed;
        anim = GetComponent<Animator>();
    }

    public void InteractWithDrawer()
    {
        Interact();
    }

    private void Interact()
    {
        switch (drawerState)
        {
            case DrawerState.IdleClosed:
                StartCoroutine(WaitForAnimation("Opening","IdleOpen"));  //Opening true the open false
                drawerState = DrawerState.IdleOpened;
                break;
            case DrawerState.IdleOpened:    // Closing tue
                anim.SetBool("IdleOpen", false);
                StartCoroutine(WaitForAnimation("Closing", "IdleClosed"));
                drawerState = DrawerState.IdleClosed;
                break;
        }
    }
    private IEnumerator WaitForAnimation(string lastState, string newState)
    {
        anim.SetBool(lastState, true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool(lastState, false);
        anim.SetBool(newState, true);
    }
}

