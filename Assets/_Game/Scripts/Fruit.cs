using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] ParticleSystem fruitVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().AddStrawBerry();
            Instantiate(fruitVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
