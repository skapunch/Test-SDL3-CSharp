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

    public static Dot dot = new Dot();
    public const int WindowWidth = 640;
    public const int WindowHeight = 480;

    public const int FpsCap = 60;
    public const UInt64 NsPerFrame = SymbolicConstants.PerSecondNS / FpsCap;
    public static Texture RenderTexture = new Texture();

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

                        dot.HandleEvent(in e);
                    }

                    dot.Move(WindowWidth, WindowHeight);

                    SDL.SDL_SetRenderDrawColor(Renderer, 0xFF, 0xFF, 0xFF, 0xFF);
                    SDL.SDL_RenderClear(Renderer);

                    dot.Render(Renderer, RenderTexture);

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
        if (SDL.SDL_Init(SDL.SDL_InitFlags.SDL_INIT_VIDEO) == false)
        {
            SDL.SDL_Log(" SDL could not initialize! SDL error: " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        else
        {
            if (SDL.SDL_CreateWindowAndRenderer("SDL3 Tutorial: Motion",
                                                WindowWidth, WindowHeight,
                                                0,
                                                out GameWindow,
                                                out Renderer) == false)
            {
                SDL.SDL_Log("Window could not be created!SDL error: " + SDL.SDL_GetError() + "\n");
                result = false;
            }
        }
        return result;
    }

    public static bool LoadMedia()
    {
        bool result = true;
        if (RenderTexture.LoadFromFile("assets/dot.png", Renderer) == false)
        {
            SDL.SDL_Log("Unable to load image file! SDL errors: " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        return result;
    }

    public static void Close()
    {
        //free the texture
        RenderTexture.Destroy();

        SDL.SDL_DestroyRenderer(Renderer);
        SDL.SDL_DestroyWindow(GameWindow);

        //quit subsystems
        SDL.SDL_Quit();
    }
}


