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
    public static IntPtr Font;
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

                //              
                bool vsyncEnabled = true;
                bool FPSCapEnabled = false;
                string vsyn = "";
                string cap = "";
                UInt64 timeSpentRendering = 0;
                Test.Timer timer = new Test.Timer();

                string timeText = "";

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
                            if (e.key.key == (uint)KeyType.SDLK_RETURN)
                            {
                                vsyncEnabled = !vsyncEnabled;
                                SDL.SDL_SetRenderVSync(Renderer, (vsyncEnabled) ? VsyncFlags.SDL_RENDERER_VSYNC_ENABLED : VsyncFlags.SDL_RENDERER_VSYNC_DISABLED);
                            }
                            else if (e.key.key == (uint)KeyType.SDLK_SPACE)
                            {
                                FPSCapEnabled = !FPSCapEnabled;
                            }
                        }
                    }

                    if (timeSpentRendering != 0)
                    {
                        double fPS = SymbolicConstants.PerSecondNS / timeSpentRendering;
                        vsyn = (vsyncEnabled) ? "(Vsync)" : "";
                        cap = (FPSCapEnabled) ? "(Cap)" : "";
                        timeText = "Frames per second: " + vsyn + cap + $"{fPS}";
                        SDL.SDL_Color textColor = new SDL.SDL_Color { r = 0x00, b = 0x00, g = 0x00, a = 0xFF };
                        RenderTexture.LoadFromRenderedText(timeText, Font, textColor, Renderer);
                    }

                    SDL.SDL_SetRenderDrawColor(Renderer, 0xFF, 0xFF, 0xFF, 0xFF);
                    SDL.SDL_RenderClear(Renderer);

                    RenderTexture.Render((WindowWidth - RenderTexture.Width) * 0.5f,
                                            (WindowHeight - RenderTexture.Height) * 0.5f, Renderer);

                    SDL.SDL_RenderPresent(Renderer);

                    //get the time spent rendering
                    timeSpentRendering = timer.GetTicksNS();

                    //skip if there are still time remains
                    if (FPSCapEnabled && timeSpentRendering < NsPerFrame)
                    {
                        UInt64 sleepTime = NsPerFrame - timeSpentRendering;
                        SDL.SDL_DelayNS(sleepTime);

                        //continue to get ticks during sleep
                        timeSpentRendering = timer.GetTicksNS();
                    }
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
            if (SDL.SDL_CreateWindowAndRenderer("SDL3 Tutorial: Frame Rate and VSync",
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
                if (SDL.SDL_SetRenderVSync(Renderer, 1) == false)
                {
                    SDL.SDL_Log("Could not enable V-Sync! SDL errors: " + SDL.SDL_GetError() + "\n");
                    result = false;
                }
                if (SDL.TTF_Init() == false)
                {
                    SDL.SDL_Log("SDL_ttf could not initialize! SDL_ttf error: " + SDL.SDL_GetError() + "\n");
                    result = false;
                }
            }
        }
        return result;
    }

    public static bool LoadMedia()
    {
        bool result = true;
        string path = "assets/lazy.ttf";
        if ((Font = SDL.TTF_OpenFont(path, 28)) == IntPtr.Zero)
        {
            SDL.SDL_Log("Could not load" + path + "! SDL_ttf Error:" + SDL.SDL_GetError() + "\n");
            result = false;
        }
        else
        {
            SDL.SDL_Color textColor = new SDL.SDL_Color { r = 0x00, b = 0x00, g = 0x00, a = 0xFF };
            if (!RenderTexture.LoadFromRenderedText("Press enter to start the timer", Font, textColor, Renderer))
            {
                SDL.SDL_Log("Could not load text texture" + path + "! SDL_ttf Error:" + SDL.SDL_GetError() + "\n");
                result = false;
            }
        }
        return result;
    }

    public static void Close()
    {
        //free the texture
        RenderTexture.Destroy();

        SDL.TTF_CloseFont(Font);

        SDL.SDL_DestroyRenderer(Renderer);
        SDL.SDL_DestroyWindow(GameWindow);

        //quit subsystems
        SDL.TTF_Quit();
        SDL.SDL_Quit();
    }
}


