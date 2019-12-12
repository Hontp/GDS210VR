using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class TowerDropSoundController :Singleton<TowerDropSoundController>
    {
        private Dictionary<string, AudioClip> sfx = new Dictionary<string, AudioClip>();
        private List<AudioClip> bmgs = new List<AudioClip>();

        private AudioSource bmgSource;

        private List<string> GetAudioFilePaths(string path)
        {
            List<string> files = new List<string>();

            string[] tmpPath;
            
            tmpPath = Directory.GetFiles(Application.dataPath + "/MiniGames/TowerDrop/Resources/" + path, "*.mp3");

            for (int i =0; i < tmpPath.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(tmpPath[i]);

                files.Add(fileName);
            }

            return files;
        }

        void Awake()
        {
            List<string> bmgFiles;
            bmgFiles = GetAudioFilePaths("Audio/Music/");


            List<string> sfxFiles;
            sfxFiles = GetAudioFilePaths("Audio/SFX/");


            for (int i =0; i < bmgFiles.Count; i++)
            {
                AudioClip track = Resources.Load<AudioClip>("Audio/Music/" + bmgFiles[i]);

                if (track != null)
                    bmgs.Add(track);
            }

            for ( int k =0; k <sfxFiles.Count; k++ )
            {
                AudioClip clip = Resources.Load<AudioClip>("Audio/SFX/" + sfxFiles[k]);

                if ( clip != null)
                {
                    sfx.Add(sfxFiles[k], clip);
                }
            }


            if (GetComponent<AudioSource>() != null)
            {
                bmgSource = GetComponent<AudioSource>();
            }
        }

        private int Randomizer()
        {
            int rand = Random.Range(0, bmgs.Count);

            return rand;
        }


        public void playSFX(AudioSource source, string name)
        {
            if (source != null && sfx.ContainsKey(name))
            {                
                source.clip = sfx[name];
                source.PlayOneShot(source.clip, 0.55f);
                
            }
        }

        void Update()
        {

            int pick = Randomizer();
            AudioClip current = bmgs[pick];

            if (bmgSource.clip == null)
            {
                bmgSource.clip = current;
                bmgSource.Play();
            }

            if (!bmgSource.isPlaying)
            {

                if (bmgSource.clip == current)
                {
                    if (pick == bmgs.Count - 1)
                        pick = 0;
                    else
                        pick++;

                    current = bmgs[pick];
                }


                bmgSource.clip = current;
                bmgSource.Play();                                      
            }


        }
    }
}
