using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
     {
         if (collision.gameObject.tag == "Player")
         {
             StartCoroutine(ParticleProcess());
         }
         if (this.gameObject.tag == "Fruit")
         {
            ScoreUI.score += 1;
         }
         else if (this.gameObject.tag == "Hamburger")
         {
            ScoreUI.score -= 1;
         }

    }

    IEnumerator ParticleProcess()
    {
        ps.Play();
        yield return new WaitForSeconds(0.15f);
        Destroy(this.gameObject);
    }  
}
