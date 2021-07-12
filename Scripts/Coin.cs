using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
   [SerializeField]
   private AudioClip _explosionClip;


   // Update is called once per frame
   void Update()
    {
      transform.Rotate(50 * Time.deltaTime, 0,0); 

    }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         PlayerManager.coinScore += 1;
         Debug.Log("coin:" + PlayerManager.coinScore);
         AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);
         //FindObjectOfType<AudioManager>().PlaySound("CoinPick");
         Destroy(gameObject);
      }
      
   }
}
