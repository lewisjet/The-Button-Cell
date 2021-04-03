using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Vector3 forces = new Vector3();
    Vector2 startPos;
    public Rigidbody2D rb;
    public bool IsEngaged { get; set; } = false;
    byte wiggles = 0;
    byte neededWiggles = 0;
   
    private void OnMouseUp()
    {
        if (IsEngaged) { return; }

        rb.velocity = forces;
        rb.velocity += 2*((Vector2)transform.position - startPos);
    }
    private void OnMouseDrag()
    {
        transform.position = GetMousePos();
        if(IsEngaged)
        {
            StartCoroutine(Wiggler());
        }
    }

    IEnumerator Wiggler()
    {
        if (((Vector2)transform.position - startPos) != Vector2.zero)
        {
            wiggles++;
        }
        transform.position = startPos;
        yield return new WaitForSeconds(1f);
        if (wiggles >= neededWiggles)
        {
            IsEngaged = false;
            wiggles = 0;
            OnMouseDown();
        }
    }
    public void OnMouseDown()
    {
        
        if (IsEngaged)
        {
            rb.bodyType = RigidbodyType2D.Static;
            neededWiggles = (byte) Random.Range(30, 130);
            startPos = transform.position;
            return;
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        forces = rb.velocity;
        rb.velocity = Vector3.zero;
        startPos = transform.position;
       
    }

    public static Vector2 GetMousePos() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
