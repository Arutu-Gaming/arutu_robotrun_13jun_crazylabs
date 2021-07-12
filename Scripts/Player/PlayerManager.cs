using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
   public static int coinScore;
   [SerializeField]
   private Text coinText;

   public static bool gameOver;
   public static bool isGameStarted;
   public static bool isGamePaused;

   public static int score;
   public Text scoreText;

   public GameObject gameoverPanel;
   public GameObject startText;

   public GameObject shield;
   
   public static bool shieldOn = false;
   
   public static int shieldCount = 0;


   private void Start()
   {
      coinScore = 0;
      score = 0;
      coinText.text = "Coin: " + 0;
      isGameStarted = false;
      Time.timeScale = 1;
      shield.SetActive(false);

      gameOver = isGameStarted = isGamePaused = false;
   }
   private void Update()
   {
      coinText.text = "Coin: " + coinScore;
      scoreText.text = score.ToString();

      if (gameOver)
      {
         gameoverPanel.SetActive(true);
         Time.timeScale = 0;
      }

      if (SwipeManager.tap)
      {
         isGameStarted = true;
         startText.SetActive(false);
      }
      if (shieldCount < 0)
      {
         shieldCount = 0;
      }
      if (shieldOn == true)
      {
         shield.SetActive(true);
      }
      else
      {
         shield.SetActive(false);
      }
   }
   private void OnControllerColliderHit(ControllerColliderHit hit)
   {
      if (hit.transform.tag == "Shield" && shieldCount < 1)
      {
         shieldOn = true;
         shield.SetActive(true);
         shieldCount++;

      }
   }
}
