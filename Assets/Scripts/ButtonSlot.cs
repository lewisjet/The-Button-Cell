using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSlot : MonoBehaviour
{

    public bool IsEngaged = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        var button = collision.GetComponentInChildren<Drag>();
        if(!button) { return; }
        else
        {
            button.transform.position = transform.position;
            button.rb.bodyType = RigidbodyType2D.Static;
            button.IsEngaged = true;
            button.OnMouseDown();
            IsEngaged = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        IsEngaged = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsEngaged = false;
    }

}
