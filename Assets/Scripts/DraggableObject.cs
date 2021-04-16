using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),
    typeof(BoxCollider2D),
    typeof(SpriteRenderer))]
public class DraggableObject : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _boxCollider;
    protected Vector3 screenPoint;
    protected Vector3 offset;
    protected ObjectState currentState = ObjectState.InitialFalling;

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position -
                 Camera.main.ScreenToWorldPoint(
                     new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        currentState = ObjectState.Dragged;
        _rigidbody.WakeUp();
    }

    protected virtual void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
        _spriteRenderer.sortingLayerName = "Over Everything";
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.sortingLayerName = "Over Everything";
        }
    }

    protected virtual void OnMouseExit()
    {
        currentState = ObjectState.Falling;
        _spriteRenderer.sortingLayerName = "Over Table";
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.sortingLayerName = "Over Table";
        }
    }
}