using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   // Start is called before the first frame update
   void Update()
   {
      transform.Rotate(50 * Time.deltaTime, 0, 0);
   }
   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player" && PlayerManager.shieldCount < 1)
      {

        PlayerManager.shieldOn = true;
         FindObjectOfType<AudioManager>().PlaySound("CoinPick");
         Destroy(gameObject);
      }

   }
}
