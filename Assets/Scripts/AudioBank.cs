using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBank : MonoBehaviour
{
    // SubspaceAudio - 8-bit sound effects w/ a creative commons license w/ 
    //      Public domain dedication from creator Juhani Junkala (SubspaceAudio)
    //      @ opengameart.org - 
    //      https://opengameart.org/content/512-sound-effects-8-bit-style
    // Beginning of assets from SubspaceAudio
    public AudioClip gameLoopMusic;
    public AudioClip gameLose;
    public AudioClip gameWin;
    public AudioClip playerLowHealth;
    public AudioClip precedePlayerDeath;
    public AudioClip playerDeath;
    public AudioClip playerHurt;
    public AudioClip turretSwitch;
    public AudioClip turretFire;
    public AudioClip shieldSwitch;
    public AudioClip shieldAmbient;
    public AudioClip shieldDeflect;
    public AudioClip basicShipFire;
    public AudioClip basicShipDestroyed;
    // End of assets from SubspaceAudio

    public AudioClip starshipFire;
    public AudioClip starshipDamaged; // From LittleRobotSoundFactory @ freesound.org
    public AudioClip starshipDestroyed;


    AudioSource battleMusic; // From GooseNinja @ itch.io

    public float starshipFireVol = 1f;
    public float starshipDamagedVol = 1f;
    public float starshipDestroyedVol = 1f;

    public float battleMusicIncrement;

    void Awake()
    {
        battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();
    }


    public IEnumerator BeginBattleMusic()
    {
        battleMusic.Play();
        battleMusic.volume = 0;
        while (battleMusic.volume < 1)
        {
            battleMusic.volume += battleMusicIncrement;
            yield return null;
        }
    }
}
