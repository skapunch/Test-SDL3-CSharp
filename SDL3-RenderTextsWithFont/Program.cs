using SDL3;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Test;
public unsafe class Program
{
    public static IntPtr GameWindow;
    public static IntPtr Renderer;
    public static IntPtr Font;
    public const int WindowWidth = 640;
    public const int WindowHeight = 480;
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
                while (quit == false)
                {
                    while (SDL.SDL_PollEvent(out e) == true)
                    {
                        if (e.type == (uint)SDL.SDL_EventType.SDL_EVENT_QUIT)
                        {
                            quit = true;
                        }
                    }
                    SDL.SDL_SetRenderDrawColor(Renderer, 0xFF, 0xFF, 0xFF, 0xFF);
                    SDL.SDL_RenderClear(Renderer);

                    RenderTexture.Render(   (WindowWidth - RenderTexture.Width) * 0.5f,
                                            (WindowHeight - RenderTexture.Height) * 0.5f, Renderer);
                    SDL.SDL_RenderPresent(Renderer);
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
            if (SDL.SDL_CreateWindowAndRenderer("SDL3 Tutorial: True Type Fonts",
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
                //Initialize font loading
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
            SDL.SDL_Log("Could not load " + path + "! SDL_ttf Error: " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        else
        {
            //load text
            SDL.SDL_Color textColor;
            textColor.r = 0x00; textColor.g = 0x00; textColor.b = 0x00; textColor.a = 0xFF;
            if (RenderTexture.LoadFromRenderedText(
                                                    "The quick brown fox jumps over the lazy dog",
                                                    Font,
                                                    in textColor,
                                                    Renderer
                                                    ) == false)
            {
                SDL.SDL_Log("Could not load text texture " + path + "! SDL_ttf error: " + SDL.SDL_GetError() + "\n");
                result = false;
            }
        }
        return result;
    }

    public static void Close()
    {
        //free the texture
        RenderTexture.Destroy();

        //free the font
        SDL.TTF_CloseFont(Font);

        SDL.SDL_DestroyRenderer(Renderer);
        SDL.SDL_DestroyWindow(GameWindow);

        //quit subsystems
        SDL.TTF_Quit();
        SDL.SDL_Quit();
    }
}


