using SDL3;

namespace Test;

public class Timer
{
    public UInt64 GetTicksNS()
    {
        UInt64 time = 0;
        if (this.isStarted)
        {
            if (this.isPaused)
                time = this.pauseAt;
            else
            {
                time = SDL.SDL_GetTicksNS() - this.startAt;
            }
        }
        return time;
    }

    public void Start()
    {
        this.isStarted = true;
        this.isPaused = false;
        this.startAt = SDL.SDL_GetTicksNS();
        this.pauseAt = 0;
    }

    public void Stop()
    {
        this.isStarted = false;
        this.isPaused = false;
        this.startAt = 0;
        this.pauseAt = 0;
    }

    public void Pause()
    {
        if (this.isStarted && !this.isPaused)
        {
            this.isPaused = true;
            this.pauseAt = SDL.SDL_GetTicksNS() - this.startAt;
            this.startAt = 0;
        }
    }

    public void Resume()
    {
        if (this.isPaused && this.isStarted)
        {
            this.isPaused = false;
            this.startAt = SDL.SDL_GetTicksNS() - this.pauseAt;
            this.pauseAt = 0;
        }
    }

    private UInt64 startAt = 0;
    private UInt64 pauseAt = 0;

    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }
    private bool isStarted = false;
    public bool IsStarted { get { return isStarted; } }
}