using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using SDL3;
using EventType = SDL3.SDL.SDL_EventType;
using KeyType = SDL3.SDL.SDL_Keycode;
namespace Test;

public class Dot
{
    struct Velocity { public int x; public int y; }
    struct Position { public int x; public int y; }

    private Velocity velocity;
    private Position position;

    private int width = 20;
    public int Width { get { return width; } }

    private int height = 20;
    public int Height { get { return height; } }

    public const int MaxVelocity = 10;

    public void HandleEvent(in SDL.SDL_Event @event)
    {
        //press
        if (@event.type == (uint)EventType.SDL_EVENT_KEY_DOWN && @event.key.repeat == false)
        {
            KeyType key = (KeyType)@event.key.key;
            switch (key)
            {
                case KeyType.SDLK_UP: this.velocity.y -= MaxVelocity; break;
                case KeyType.SDLK_DOWN: this.velocity.y += MaxVelocity; break;
                case KeyType.SDLK_LEFT: this.velocity.x -= MaxVelocity; break;
                case KeyType.SDLK_RIGHT: this.velocity.x += MaxVelocity; break;
            }
        }

        //release
        else if (@event.type == (uint)EventType.SDL_EVENT_KEY_UP && @event.key.repeat == false)
        {
            KeyType key = (KeyType)@event.key.key;
            switch (key)
            {
                case KeyType.SDLK_UP: this.velocity.y += MaxVelocity; break;
                case KeyType.SDLK_DOWN: this.velocity.y -= MaxVelocity; break;
                case KeyType.SDLK_LEFT: this.velocity.x += MaxVelocity; break;
                case KeyType.SDLK_RIGHT: this.velocity.x -= MaxVelocity; break;
            }
        }
    }

    public void Move(int xbound, int ybound)
    {
        this.position.x += this.velocity.x;
        if (this.position.x < 0 || this.position.x + this.width > xbound)
        {
            this.position.x -= this.velocity.x;
        }
        this.position.y += this.velocity.y;
        if (this.position.y < 0 || this.position.y + this.height > ybound)
        {
            this.position.y -= this.velocity.y;
        }
    }

    public void Render(IntPtr renderer, Texture? texture)
    {
        if(texture != null)
            texture.Render(this.position.x, this.position.y, renderer);
    }
}