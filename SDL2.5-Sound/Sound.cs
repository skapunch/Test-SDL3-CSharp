namespace Test;

public class Sound
{
    public enum PlayBackMode
    {
        ONESHOT = 0,
        LOOP = -1,
    }
    public enum EffectChannel
    {
        LOW,
        MEDIUM,
        HIGH,
        SCRATCH,
        TOTAL
    }
    public const uint SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK = 0xFFFFFFFFu;
}