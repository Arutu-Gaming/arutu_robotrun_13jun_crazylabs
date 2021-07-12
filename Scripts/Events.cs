
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Events : MonoBehaviour
{
   public GameObject gamePausedPanel;
   public Text speedText;
   public Text jumpHeight;
   public Text cameraXRotate;
   public Text cameraYRotate;

   [SerializeField]
   private float xRotate = 33.873f;
   [SerializeField]
   private float yRotate = 0;

   public Button pauseButton;
   // Start is called before the first frame update
   void Awake()
    {
      speedText.text = PlayerControl.forwardSpeed.ToString();
      jumpHeight.text = PlayerControl.jumpHeight.ToString();
      cameraXRotate.text = xRotate.ToString();
      cameraYRotate.text = yRotate.ToString();
   }

    // Update is called once per frame
    void Update()
    {
      if (!PlayerManager.isGameStarted)
         return;

      if (PlayerManager.gameOver)
         pauseButton.interactable = false;

      if (PlayerManager.gameOver)
         return;

      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (PlayerManager.isGamePaused)
         {
            ResumeGame();
            gamePausedPanel.SetActive(false);
         }
         else
         {
            PauseGame();
            gamePausedPanel.SetActive(true);
         }

         speedText.text = PlayerControl.forwardSpeed.ToString();

      }
   }
   public void ReplayGame()
   {
      UnityEngine.SceneManagement.SceneManager.LoadScene("level");
   }
   public void QuitGame()
   {
      Application.Quit();
   }
   public void PauseGame()
   {
      if (!PlayerManager.isGamePaused && !PlayerManager.gameOver)
      {
         Time.timeScale = 0;
         PlayerManager.isGamePaused = true;
         gamePausedPanel.SetActive(true);
      }
   }
   public void ResumeGame()
   {
      if (PlayerManager.isGamePaused)
      {
         Time.timeScale = 1;
         PlayerManager.isGamePaused = false;
         gamePausedPanel.SetActive(false);
      }
   }

   public void SpeedIncrement()
   {
      PlayerControl.forwardSpeed += 1;
      speedText.text = PlayerControl.forwardSpeed.ToString();
   }
   public void SpeedDecrement()
   {
      PlayerControl.forwardSpeed -= 1;
      speedText.text = PlayerControl.forwardSpeed.ToString();
   }

   public void JumpHeightIncrease()
   {
      PlayerControl.jumpHeight += 1;
      jumpHeight.text = PlayerControl.jumpHeight.ToString();
   }
   public void JumpHeightDecrease()
   {
      PlayerControl.jumpHeight -= 1;
      jumpHeight.text = PlayerControl.jumpHeight.ToString();
   }

   public void CameraAngleYIncrease()
   {
      
      yRotate += 1;
      Camera.main.transform.rotation = Quaternion.Euler(transform.rotation.x, yRotate, transform.rotation.z);
      cameraYRotate.text = yRotate.ToString();
   }
   public void CameraAngleYDecrease()
   {
      yRotate -= 1;
      Camera.main.transform.rotation = Quaternion.Euler(transform.rotation.x, yRotate, transform.rotation.z);
      cameraYRotate.text = yRotate.ToString();
   }
   public void CameraAngleXIncrease()
   {

      xRotate += 1;
      Camera.main.transform.rotation = Quaternion.Euler(xRotate, transform.rotation.y, transform.rotation.z);
      cameraXRotate.text = xRotate.ToString();
   }
   public void CameraAngleXDecrease()
   {
      xRotate -= 1;
      Camera.main.transform.rotation = Quaternion.Euler(xRotate, transform.rotation.y, transform.rotation.z);
      cameraXRotate.text = xRotate.ToString();
   }
}
