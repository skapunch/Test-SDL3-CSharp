using SDL3;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Test;
using TinTin;
using EventType = SDL3.SDL.SDL_EventType;
using KeyType = SDL3.SDL.SDL_Keycode;
public unsafe class Program
{
    public static IntPtr GameWindow;
    public static IntPtr Renderer;

    public const int WindowWidth = 640;
    public const int WindowHeight = 480;

    public const int FpsCap = 60;
    public const UInt64 NsPerFrame = SymbolicConstants.PerSecondNS / FpsCap;
    public static Texture RenderTexture = new Texture();

    public static uint AudioDeviceID = 0;

    public static int ChannelCount = 0;

    public static IntPtr MusicChannel = new();
    public static SDL.Mix_Chunk* ScratchChannel = null;
    public static SDL.Mix_Chunk* LowChannel = null;
    public static SDL.Mix_Chunk* MediumChannel = null;
    public static SDL.Mix_Chunk* HighChannel = null;
    public static int Main(string[] args)
    {
        int exitCode = 0;
        //SDL.SDL_SetMainReady();
        if (Init() == false)
        {
            SDL.SDL_Log("Unable to initialize program!\n");
            exitCode = 1;
        }
        else
        {
            if (LoadMedia() == false)
            {
                SDL.SDL_Log("Unable to load media!\n");
                exitCode = 2;
            }
            else
            {
                bool quit = false;
                SDL.SDL_Event e;
                Unsafe.InitBlock(&e, 0, (uint)sizeof(SDL.SDL_Event));


                UInt64 timeSpentRendering = 0;
                Test.Timer timer = new Test.Timer();

                while (quit == false)
                {
                    timer.Start();
                    while (SDL.SDL_PollEvent(out e) == true)
                    {
                        if (e.type == (uint)EventType.SDL_EVENT_QUIT)
                        {
                            quit = true;
                        }
                        else if (e.type == (uint)EventType.SDL_EVENT_KEY_DOWN)
                        {
                            switch ((KeyType)e.key.key)
                            {
                                case KeyType.SDLK_1:
                                    SDL.Mix_PlayChannel((int)Sound.EffectChannel.HIGH, HighChannel, (int)Sound.PlayBackMode.ONESHOT);
                                    break;
                                case KeyType.SDLK_2:
                                    SDL.Mix_PlayChannel((int)Sound.EffectChannel.MEDIUM, MediumChannel, (int)Sound.PlayBackMode.ONESHOT);
                                    break;
                                case KeyType.SDLK_3:
                                    SDL.Mix_PlayChannel((int)Sound.EffectChannel.LOW, LowChannel, (int)Sound.PlayBackMode.ONESHOT);
                                    break;
                                case KeyType.SDLK_4:
                                    SDL.Mix_PlayChannel((int)Sound.EffectChannel.SCRATCH, ScratchChannel, (int)Sound.PlayBackMode.ONESHOT);
                                    break;
                                case KeyType.SDLK_9:
                                    if (SDL.Mix_PlayingMusic() == false)
                                    {
                                        SDL.Mix_PlayMusic(MusicChannel, (int)Sound.PlayBackMode.LOOP);
                                    }
                                    else
                                    {
                                        if (SDL.Mix_PausedMusic() == true)
                                        {
                                            SDL.Mix_ResumeMusic();
                                        }
                                        else
                                        {
                                            SDL.Mix_PauseMusic();
                                        }
                                    }
                                    break;

                                case KeyType.SDLK_0:
                                    SDL.Mix_HaltMusic();
                                    break;
                            }
                        }
                    }

                    SDL.SDL_SetRenderDrawColor(Renderer, 0xFF, 0xFF, 0xFF, 0xFF);
                    SDL.SDL_RenderClear(Renderer);

                    //Render
                    RenderTexture.Render(0.0f, 0.0f, Renderer);
                    SDL.SDL_RenderPresent(Renderer);

                    //get the time spent rendering
                    timeSpentRendering = timer.GetTicksNS();

                    //skip if there are still time remains -CAPPING THE FPS
                    if (timeSpentRendering < NsPerFrame)
                    {
                        UInt64 sleepTime = NsPerFrame - timeSpentRendering;
                        SDL.SDL_DelayNS(sleepTime);

                        //continue to get ticks during sleep
                        timeSpentRendering = timer.GetTicksNS();
                    }
                    //End of -CAPPING THE FPS
                }
            }
        }
        Close();
        return exitCode;
    }

    public static bool Init()
    {
        bool result = true;
        if (SDL.SDL_Init(SDL.SDL_InitFlags.SDL_INIT_VIDEO | SDL.SDL_InitFlags.SDL_INIT_AUDIO) == false)
        {
            SDL.SDL_Log(" SDL could not initialize! SDL error: " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        else
        {
            if (SDL.SDL_CreateWindowAndRenderer("SDL3 Tutorial: Sound Effects and Music",
                                                WindowWidth, WindowHeight,
                                                0,
                                                out GameWindow,
                                                out Renderer) == false)
            {
                SDL.SDL_Log("Window could not be created!SDL error: " + SDL.SDL_GetError() + "\n");
                result = false;
            }
            else
            {
                SDL.SDL_AudioSpec audioSpec = new();
                audioSpec.format = SDL.SDL_AudioFormat.SDL_AUDIO_F32;
                audioSpec.channels = 2;
                audioSpec.freq = 44100;

                AudioDeviceID = SDL.SDL_OpenAudioDevice(Sound.SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK, ref audioSpec);
                if (AudioDeviceID == 0)
                {
                    SDL.SDL_Log("Unable to open audio! SDL error: " + SDL.SDL_GetError() + "\n");
                    result = false;
                }
                else
                {
                    if (SDL.Mix_OpenAudio(AudioDeviceID, IntPtr.Zero) == false)
                    {
                        SDL.SDL_Log("SDL_mixer could not initialize! SDL_mixer error: " + SDL.SDL_GetError() + "\n");
                        result = false;
                    }
                }
            }
        }
        return result;
    }

    public static bool LoadMedia()
    {
        bool result = true;
        if (RenderTexture.LoadFromFile("assets/prompt.png", Renderer) == false)
        {
            SDL.SDL_Log("Unable to load image file! SDL errors: " + SDL.SDL_GetError() + "\n");
            result = false;
        }

        //Load Audios
        if ((MusicChannel = SDL.Mix_LoadMUS("assets/beat.wav")) == IntPtr.Zero)
        {
            SDL.SDL_Log("Unable to load music! SDL_mixer error " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        if ((ScratchChannel = SDL.Mix_LoadWAV("assets/scratch.wav")) == null)
        {
            SDL.SDL_Log("Unable to load scratch sound! SDL_mixer error " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        if ((HighChannel = SDL.Mix_LoadWAV("assets/high.wav")) == null)
        {
            SDL.SDL_Log("Unable to load high sound! SDL_mixer error " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        if ((MediumChannel = SDL.Mix_LoadWAV("assets/medium.wav")) == null)
        {
            SDL.SDL_Log("Unable to load medium sound! SDL_mixer error " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        if ((LowChannel = SDL.Mix_LoadWAV("assets/low.wav")) == null)
        {
            SDL.SDL_Log("Unable to load low sound! SDL_mixer error " + SDL.SDL_GetError() + "\n");
            result = false;
        }

        //Allocate channels
        if (result)
        {
            if ((ChannelCount = SDL.Mix_AllocateChannels((int)Sound.EffectChannel.TOTAL)) != (int)Sound.EffectChannel.TOTAL)
            {
                SDL.SDL_Log("Unable to allocate channels! SDL_mixer error " + SDL.SDL_GetError() + "\n");
                result = false;
            }
        }
        return result;
    }

    public static void Close()
    {
        SDL.Mix_FreeMusic(MusicChannel);
        MusicChannel = IntPtr.Zero;

        SDL.Mix_FreeChunk(ScratchChannel);
        ScratchChannel = null;
        SDL.Mix_FreeChunk(LowChannel);
        LowChannel = null;
        SDL.Mix_FreeChunk(MediumChannel);
        MediumChannel = null;
        SDL.Mix_FreeChunk(HighChannel);
        HighChannel = null;
        //free the texture
        RenderTexture.Destroy();

        SDL.Mix_CloseAudio();

        SDL.SDL_CloseAudioDevice(AudioDeviceID);
        AudioDeviceID = 0;

        SDL.SDL_DestroyRenderer(Renderer);
        SDL.SDL_DestroyWindow(GameWindow);

        //quit subsystems
        SDL.Mix_Quit();
        SDL.SDL_Quit();
    }
}


