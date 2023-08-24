using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SoundFx : MonoBehaviour
{
        private AudioSource audioSource;

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            Assert.IsTrue(audioSource != null);
        }

        /// <summary>
        /// Plays the specified audio clip.
        /// </summary>
        /// <param name="clip">The audio clip to play.</param>
        /// <param name="loop">True if the clip should be looped; false otherwise.</param>
        public void Play(AudioClip clip, bool loop = false)
        {
            if (clip == null)
            {
                return;
            }
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
            Invoke("DisableSoundFx", clip.length + 0.1f);
        }

        /// <summary>
        /// Returns the sound effect to the sound effects pool.
        /// </summary>
        private void DisableSoundFx()
        {
            GetComponent<PooledObject>().pool.ReturnObject(gameObject);
        }
    }