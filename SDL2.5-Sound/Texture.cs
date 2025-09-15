using Microsoft.VisualBasic;
using SDL3;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Test;
public unsafe class Texture : IDisposable
{
    private int width = 0;
    public int Width { get { return this.width; } }
    private int height = 0;
    public int Height { get { return this.height; } }

    private SDL.SDL_Texture* texture = null;

    const float OGSize = -1.0f; //used to state that we need full size of dimension w and h

    public bool IsLoaded { get { return this.texture != null; } }

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

    public bool LoadFromRenderedText(string textureText, nint font, in SDL.SDL_Color textColor, IntPtr renderer)
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
        SDL.SDL_SetTextureColorMod(this.texture,r,g,b);
    }

    public void SetAlpha(byte alpha)
    {
        SDL.SDL_SetTextureAlphaMod(this.texture, alpha);
    }

    public void SetBlending(SDL.SDL_BlendMode blendmode)
    {
        SDL.SDL_SetTextureBlendMode(this.texture, (uint)blendmode);
    }

    public void Destroy()
    {
        SDL.SDL_DestroyTexture(this.texture);
        this.texture = null;
        this.width = 0;
        this.height = 0;
    }

    public void Render  (float inX, float inY, IntPtr renderer,
                         SDL.SDL_FRect? inClip = null,
                         float inWidth = OGSize, float inHeight = OGSize,
                         double inDegree = 0,
                         SDL.SDL_FPoint? inCenter = null,
                         SDL.SDL_FlipMode inFlipMode = SDL.SDL_FlipMode.SDL_FLIP_NONE )
    {
        SDL.SDL_FRect dstRect = new SDL.SDL_FRect(); //0s as fields
        SDL.SDL_FRect @outClip = new SDL.SDL_FRect(); //0s as fields
        SDL.SDL_FPoint @outCenter = new SDL.SDL_FPoint(); //0s as fields

        dstRect.x = inX; dstRect.y = inY;
        dstRect.w = (float)this.width; dstRect.h = (float)this.height;
        //if clip is given
        if (inClip != null)
        {
            @outClip = inClip.Value;
            //modify the destination surface dimensions too
            dstRect.w = @outClip.w;
            dstRect.h = @outClip.h;
        }
        else
        {
            //ok clip is null, make it dimensions the same as texture
            // equalint to no clip
            @outClip.w = (float)this.width;
            @outClip.h = (float)this.height;
        }

        if (inWidth > 0)
            dstRect.w = inWidth;
        if (inHeight > 0)
            dstRect.h = inHeight;

        //SDL.SDL_RenderTexture(renderer, this.texture, ref srcRect , ref dstRect);
            SDL.SDL_RenderTextureRotated(renderer, this.texture, ref @outClip, ref dstRect, inDegree, ref @outCenter, inFlipMode);
    }

    public void Dispose()
    {
        Destroy();
    }
}