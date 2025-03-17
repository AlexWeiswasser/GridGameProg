using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrbScript : MonoBehaviour
{
   float lifetime = 3f;

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }

    public void OnPlayerCollection()
    {
        lifetime = 100f;
        gameObject.GetComponent<Collider2D>().enabled = false;
        transform.DOScale(transform.localScale * 100f, 3f);
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 3f);
        Destroy(gameObject, 3f);
    }
}
