using SDL3;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Test;
public unsafe class Program
{
    public static IntPtr GameWindow;
    public static IntPtr ScreenSurface;

    public static IntPtr Renderer;
    public static IntPtr Image;
    public const int WindowWidth = 640;
    public const int WindowHeight = 480;

    public static Texture TestTexture = new Texture();
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

                    /*
                    SDL.SDL_FillSurfaceRect(ScreenSurface, IntPtr.Zero,
                                    SDL.SDL_MapSurfaceRGB(ScreenSurface, 0xFF, 0xFF, 0xFF));
                    SDL.SDL_BlitSurface(Image, IntPtr.Zero, ScreenSurface, IntPtr.Zero);
                    SDL.SDL_UpdateWindowSurface(GameWindow);
                    */

                    SDL.SDL_SetRenderDrawColor(Renderer, 0xFF, 0xFF, 0xFF, 0xFF);
                    SDL.SDL_RenderClear(Renderer);

                    TestTexture.Render(0f, 0f, Renderer);
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
            /*
            if ((GameWindow = SDL.SDL_CreateWindow("SDL3 Tutorial: Hello SDL3", WindowWidth, WindowHeight, 0)) == IntPtr.Zero)
            {
                SDL.SDL_Log("Window could not be created! SDL error: " + SDL.SDL_GetError() + "\n");
                result = false;
            }
            else
            {
                ScreenSurface = (IntPtr)SDL.SDL_GetWindowSurface(GameWindow);
            }
            */

            if (SDL.SDL_CreateWindowAndRenderer("SDL3 Tutorial: Textures and Extension Libraries",
                                                WindowWidth, WindowHeight,
                                                0,
                                                out GameWindow,
                                                out Renderer) == false)
            {
                SDL.SDL_Log("Window could not be created!SDL error: " + SDL.SDL_GetError());
                result = false;
            }
        }
        return result;
    }

    public static bool LoadMedia()
    {
        bool result = true;
        /*
        string imagePath = "01-hello-sdl3/hello-sdl3.bmp";
        if ((Image = (IntPtr)SDL.SDL_LoadBMP(imagePath)) == IntPtr.Zero)
        {
            SDL.SDL_Log("Unable to load image " + imagePath + "! SDL error: " + SDL.SDL_GetError() + "\n");
            result = false;
        }
        */
        if (TestTexture.LoadFromFile("01-hello-sdl3/loaded.png", Renderer) == false)
        {
            SDL.SDL_Log("Unable to load png image\n");
            result = false;
        }
        return result;
    }

    public static void Close()
    {
        TestTexture.Destroy();
        SDL.SDL_DestroyRenderer(Renderer);
        //SDL.SDL_DestroySurface(Image);
        SDL.SDL_DestroyWindow(GameWindow);
        SDL.SDL_Quit();
    }
}


