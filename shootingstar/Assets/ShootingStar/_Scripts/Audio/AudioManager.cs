using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PeixeAbissal.Audio {

    public enum Music {

        INTRO,
        WISH
    }

    public enum SFX {

        VICTORY,
        DEFEAT,
        SEND_WISH,
        POPUP
    }

    public class AudioManager : MonoBehaviour {

        private static AudioManager instance;
        public static AudioManager Instance {

            get { return instance; }
            private set { }
        }

        [SerializeField] private AudioClip introSong;
        [SerializeField] private AudioClip wishSong;

        [SerializeField] private AudioClip[] sfx;

        private AudioSource source;
        private AudioSource sfxSource;

        void Awake () {

            instance = this;
            source = GetComponents<AudioSource> () [0];
            sfxSource = GetComponents<AudioSource> () [1];
        }

        public void PlayMusic (Music musicToPlay) {

            var clipToPlay = musicToPlay.Equals (Music.INTRO) ? introSong : wishSong;
            if (source.isPlaying) {

                source.DOFade (0, 1f)
                    .OnComplete (() => {

                        InternalPlay (clipToPlay);
                    });
            } else {

                InternalPlay (clipToPlay);
            }
        }

        public void PlaySFX (SFX sfx) {

            sfxSource.clip = this.sfx[(int) sfx];
            sfxSource.Play ();
        }

        private void InternalPlay (AudioClip clip) {

            source.clip = clip;
            source.Play ();
            source.DOFade (1f, 1f)
                .From (0);
        }
    }
}