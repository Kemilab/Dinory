using Plugin.Maui.Audio;
using System.Threading.Tasks;

namespace Dinory.Services
{
    public class AudioPlayerService
    {
        private static readonly Lazy<AudioPlayerService> _instance = new(() => new AudioPlayerService());

        public static AudioPlayerService Instance => _instance.Value;

        private IAudioPlayer _audioPlayer;

        private AudioPlayerService()
        {
            LoadAudioFile();
        }

        private async Task LoadAudioFile()
        {
            _audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("back.mp3"));
            _audioPlayer.Loop = true;
        }

        public void ToggleAudio(bool isPlaying)
        {
            if (_audioPlayer == null) return;

            if (isPlaying)
            {
                _audioPlayer.Play();
            }
            else
            {
                _audioPlayer.Stop();
            }
        }
    }
}
