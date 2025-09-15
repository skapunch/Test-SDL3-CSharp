using SDL3;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Test;
using System.Security.Cryptography.X509Certificates;
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

                Button[] buttons = { new Button(), new Button(), new Button(), new Button() };
                int bWidth = buttons[0].Width;
                int bHeight = buttons[0].Height;

                for (int i = 0, y = 0; i < buttons.Length / 2; i++, y += bHeight)
                {
                    for (int j = 0, x = 0; j < buttons.Length / 2; j++, x += bWidth)
                    {
                        buttons[i*2 + j].SetPosition(x, y);
                    }
                }
                
                while (quit == false)
                {
                    while (SDL.SDL_PollEvent(out e) == true)
                    {
                        if (e.type == (uint)SDL.SDL_EventType.SDL_EVENT_QUIT)
                        {
                            quit = true;
                        }

                        //handle button events
                        for (int i = 0; i < 4; i++)
                        {
                            buttons[i].HandleEvent(ref e);
                        }
                    }
                    SDL.SDL_SetRenderDrawColor(Renderer, 0xFF, 0xFF, 0xFF, 0xFF);
                    SDL.SDL_RenderClear(Renderer);

                    for (int i = 0; i < 4; i++)
                    {
                        buttons[i].Render(RenderTexture, Renderer);
                    }
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
            if (SDL.SDL_CreateWindowAndRenderer("SDL3 Tutorial: Mouse Events",
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
        if (RenderTexture.LoadFromFile("assets/button.png", Renderer) == false)
        {
            SDL.SDL_Log("Unable to load button image!\n");
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


