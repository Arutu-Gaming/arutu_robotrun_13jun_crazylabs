using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;

   [SerializeField]
   private Audio[] _sounds;

   void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
         return;
      }

      foreach (Audio s in _sounds)
      {
         s.source = gameObject.AddComponent<AudioSource>();
         s.source.clip = s.clip;
         s.source.volume = s.volume;
         s.source.loop = s.loop;
         s.source.pitch = s.pitch;
      }

      PlaySound("Main");

   }

   public void PlaySound(string name)
   {
      foreach (Audio s in _sounds)
      {
         if (s.name == name)
            s.source.Play();
      }
   }
}
