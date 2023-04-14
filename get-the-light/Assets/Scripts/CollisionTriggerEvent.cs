using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriggerEvent : MonoBehaviour
{
    [SerializeField]
    private Color monstercolor;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = monstercolor;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            GetComponent<MonsterHP>().TakeDamage(1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = Color.red;
    }

}
