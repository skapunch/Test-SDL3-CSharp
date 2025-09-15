using Microsoft.VisualBasic;
using SDL3;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Test;
public unsafe class Texture : IDisposable
{
    private int width;
    private int height;

    private SDL.SDL_Texture* texture;

    public bool IsLoaded { get; }

    public Texture()
    {
        this.texture = null;
        this.width = 0;
        this.height = 0;
    }


    public bool LoadFromFile(string path, IntPtr renderer)
    {
        Destroy();
        SDL.SDL_Surface* surface;
        if ((surface = SDL.IMG_Load(path)) == null)
        {
            SDL.SDL_Log("Unable to load image " + path + "! SDL_image error " + SDL.SDL_GetError());
        }
        else
        {
            //create texture from image
            if ((this.texture = SDL.SDL_CreateTextureFromSurface(renderer, (IntPtr)surface)) == null)
            {
                SDL.SDL_Log("Unable to create texture from loaded pixels! SDL error: " + SDL.SDL_GetError());
            }
            else
            {
                this.width = surface->w;
                this.height = surface->h;
            }

            //clean up loaded surface
            SDL.SDL_DestroySurface((IntPtr)surface);
        }

        return this.texture != null;
    }

    public void Destroy()
    {
        SDL.SDL_DestroyTexture((IntPtr)this.texture);
        this.texture = null;
        this.width = 0; 
        this.height = 0;
    }

    public void Render(float x, float y, IntPtr renderer)
    {
        SDL.SDL_FRect dstRect = new SDL.SDL_FRect();
        dstRect.x = x;
        dstRect.y = y;
        dstRect.w = (float)this.width;
        dstRect.h = (float)this.height;
        SDL.SDL_RenderTexture(renderer, (IntPtr)this.texture, ref dstRect , ref dstRect);
    }

    public void Dispose()
    {
        Destroy();
    }
}