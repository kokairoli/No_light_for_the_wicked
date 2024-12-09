using Inventory.Model;
using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO inventoryItem { get; private set; }

    [field: SerializeField]
    public int quantity { get; set; } = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = inventoryItem.ItemImage;

        AdjustSpriteRendererSizeToBoxCollider(ref spriteRenderer);


    }

    private void AdjustSpriteRendererSizeToBoxCollider(ref SpriteRenderer spriteRenderer)
    {
        Vector2 boxColliderSize = GetComponent<BoxCollider2D>().size;
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector2 scale = new Vector2(boxColliderSize.x / spriteSize.x, boxColliderSize.y / spriteSize.y);
        spriteRenderer.transform.localScale = scale;
    }

    internal void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(gameObject);
    }
}
