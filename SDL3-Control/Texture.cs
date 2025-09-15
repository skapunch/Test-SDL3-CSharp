using Microsoft.VisualBasic;
using SDL3;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Test;
public unsafe class Texture : IDisposable
{
    private int width;
    public int Width { get { return this.width; } }
    private int height;
    public int Height { get { return this.height; } }

    private SDL.SDL_Texture* texture;

    public bool IsLoaded { get {return this.texture != null;} }

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
            //Color key image
            if (SDL.SDL_SetSurfaceColorKey(surface, true, SDL.SDL_MapSurfaceRGB(surface, 0x00, 0xFF, 0xFF)) == false)
            {
                SDL.SDL_Log("Unable to load image " + path + "! SDL_image error: " + SDL.SDL_GetError());
            }
            
            if ((this.texture = SDL.SDL_CreateTextureFromSurface(renderer, surface)) == null)
            {
                SDL.SDL_Log("Unable to create texture from loaded pixels! SDL error: " + SDL.SDL_GetError());
            }
            else
            {
                this.width = surface->w;
                this.height = surface->h;
            }

            //clean up loaded surface
            SDL.SDL_DestroySurface(surface);
        }

        return this.texture != null;
    }

    public bool LoadFromRenderedText(string textureText, nint font, SDL.SDL_Color textColor, IntPtr renderer)
    {
        Destroy();
        SDL.SDL_Surface* surface = SDL.TTF_RenderText_Blended(font, textureText, 0, textColor);
        //Create the render text and put it on the surface
        if (surface == null)
        {
            SDL.SDL_Log("Unable to render text surface! SDL_ttf error:" + SDL.SDL_GetError() + "\n");
        }
        else
        {
            //Make Texture from the surface
            if ((this.texture = SDL.SDL_CreateTextureFromSurface(renderer, surface)) == null)
            {
                SDL.SDL_Log("Unable to create texture from text rendered surface! SDL error: " + SDL.SDL_GetError() + "\n");
            }
            else
            {
                this.width = surface->w;
                this.height = surface->h;
            }

            //clean up
            SDL.SDL_DestroySurface(surface);
        }
        return this.texture != null;
    }

    public void SetColor(byte r, byte g, byte b)
    {

    }

    public void SetAlpha(byte alpha)
    {

    }

    public void SetBlending(SDL.SDL_BlendMode blendmode)
    {

    }

    public void Destroy()
    {
        SDL.SDL_DestroyTexture(this.texture);
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

        SDL.SDL_FRect srcRect = new SDL.SDL_FRect();
        srcRect.x = 0;
        srcRect.y = 0;
        srcRect.w = (float)this.width;
        srcRect.h = (float)this.height;

        SDL.SDL_RenderTexture(renderer, this.texture, ref srcRect , ref dstRect);
    }

    public void Dispose()
    {
        Destroy();
    }
}