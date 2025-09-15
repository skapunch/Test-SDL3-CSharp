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

    public static Texture UpTexture = new Texture();
    public static Texture DownTexture = new Texture();
    public static Texture LeftTexture = new Texture();
    public static Texture RightTexture = new Texture();
    
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

                var curretTexture = UpTexture!;
                SDL.SDL_Color bgColor = new SDL.SDL_Color();


                Unsafe.InitBlock(&e, 0, (uint)sizeof(SDL.SDL_Event));
                while (quit == false)
                {
                    while (SDL.SDL_PollEvent(out e) == true)
                    {
                        if (e.type == (uint)SDL.SDL_EventType.SDL_EVENT_QUIT)
                        {
                            quit = true;
                        }
                        else if (e.type == (uint)SDL.SDL_EventType.SDL_EVENT_KEY_DOWN)
                        {
                            if (e.key.key == (uint)SDL.SDL_Keycode.SDLK_UP)
                            {
                                curretTexture = UpTexture!;
                            }
                            if (e.key.key == (uint)SDL.SDL_Keycode.SDLK_DOWN)
                            {
                                curretTexture = DownTexture!;
                            }
                            if (e.key.key == (uint)SDL.SDL_Keycode.SDLK_LEFT)
                            {
                                curretTexture = LeftTexture!;
                            }
                            if (e.key.key == (uint)SDL.SDL_Keycode.SDLK_RIGHT)
                            {
                                curretTexture = RightTexture!;
                            }  
                        }
                    }

                    bgColor.r = 0xFF;
                    bgColor.b = 0xFF;
                    bgColor.g = 0xFF;

                    var key = SDL.SDL_GetKeyboardState();
                    if (key[(int)SDL.SDL_Scancode.SDL_SCANCODE_UP] == true)
                    {
                        bgColor.r = 0xFF;
                        bgColor.b = 0x00;
                        bgColor.g = 0x00;
                    }
                    else if (key[(int)SDL.SDL_Scancode.SDL_SCANCODE_DOWN] == true)
                    {
                        bgColor.r = 0x00;
                        bgColor.b = 0xFF;
                        bgColor.g = 0x00;
                    }
                    else if (key[(int)SDL.SDL_Scancode.SDL_SCANCODE_LEFT] == true)
                    {
                        bgColor.r = 0xFF;
                        bgColor.b = 0xFF;
                        bgColor.g = 0x00;
                    }
                    else if (key[(int)SDL.SDL_Scancode.SDL_SCANCODE_RIGHT] == true)
                    {
                        bgColor.r = 0x00;
                        bgColor.b = 0x00;
                        bgColor.g = 0xFF;
                    }


                    /*
                        SDL.SDL_FillSurfaceRect(ScreenSurface, IntPtr.Zero,
                                        SDL.SDL_MapSurfaceRGB(ScreenSurface, 0xFF, 0xFF, 0xFF));
                        SDL.SDL_BlitSurface(Image, IntPtr.Zero, ScreenSurface, IntPtr.Zero);
                        SDL.SDL_UpdateWindowSurface(GameWindow);
                        */

                    SDL.SDL_SetRenderDrawColor(Renderer, bgColor.r, bgColor.g, bgColor.b, 0xFF);
                    SDL.SDL_RenderClear(Renderer);

                    curretTexture.Render(   (WindowWidth - curretTexture.Width) * 0.5f,
                                            (WindowHeight - curretTexture.Height) * 0.5f, Renderer);
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

            if (SDL.SDL_CreateWindowAndRenderer("SDL3 Tutorial: Key Presses and Key States",
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
        if (UpTexture.LoadFromFile("assets/up.png", Renderer) == false)
        {
            SDL.SDL_Log("Unable to load png image\n");
            result = false;
        }
        if (DownTexture.LoadFromFile("assets/down.png", Renderer) == false)
        {
            SDL.SDL_Log("Unable to load png image\n");
            result = false;
        }
        if (LeftTexture.LoadFromFile("assets/left.png", Renderer) == false)
        {
            SDL.SDL_Log("Unable to load png image\n");
            result = false;
        }
        if (RightTexture.LoadFromFile("assets/right.png", Renderer) == false)
        {
            SDL.SDL_Log("Unable to load png image\n");
            result = false;
        }
        return result;
    }

    public static void Close()
    {
        UpTexture.Destroy();
        DownTexture.Destroy();
        LeftTexture.Destroy();
        RightTexture.Destroy();
        SDL.SDL_DestroyRenderer(Renderer);
        //SDL.SDL_DestroySurface(Image);
        SDL.SDL_DestroyWindow(GameWindow);
        SDL.SDL_Quit();
    }
}


