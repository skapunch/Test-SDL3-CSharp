using System.Diagnostics;
using System.Globalization;
using SDL3;
using EventType = SDL3.SDL.SDL_EventType;
namespace Test;

public unsafe class Button 
{
    //**Properties
    private int width = 300;
    public int Width { get { return this.width; }}
    private int height = 200;
    public int Height { get { return this.height; }}
    //**End of Properties
    public void SetPosition(float x, float y)
    {
        this.position.x = x;
        this.position.y = y;
    }

    public void HandleEvent(ref SDL.SDL_Event @event)
    {
        if (@event.type == (uint)EventType.SDL_EVENT_MOUSE_MOTION ||
            @event.type == (uint)EventType.SDL_EVENT_MOUSE_BUTTON_DOWN ||
            @event.type == (uint)EventType.SDL_EVENT_MOUSE_BUTTON_UP)
        {
            float x = -1.0f, y = -1.0f;
            SDL.SDL_GetMouseState(out x, out y);

            //Check mouse position against button
            bool inside = true;
            if (x < this.position.x)
                inside = false;
            else if (x > this.position.x + this.width)
                inside = false;
            else if (y < this.position.y)
                inside = false;
            else if (y > this.position.y + this.height)
                inside = false;

            //change the button state
            if (!inside)
                this.state = ButtonState.MouseOut;
            else
            {
                switch ((EventType)@event.type)
                {
                    case EventType.SDL_EVENT_MOUSE_BUTTON_DOWN:
                        this.state = ButtonState.MouseDown; break;
                    case EventType.SDL_EVENT_MOUSE_BUTTON_UP:
                        this.state = ButtonState.MouseUp; break;
                    case EventType.SDL_EVENT_MOUSE_MOTION:
                        this.state = ButtonState.MouseOverMotion; break;
                    default:
                        this.state = ButtonState.MouseOut;break;

                }
            }
        }
    }

    public void Render(Test.Texture? texture, IntPtr renderer)
    {
        if (texture != null)
        {
            SDL.SDL_FRect[] rectangle = new SDL.SDL_FRect[(int)ButtonState.Total];
            for (int i = 0; i < rectangle.Length; i++)
            {
                rectangle[i].x = 0.0f;
                rectangle[i].y = i * this.height;
                rectangle[i].w = this.width;
                rectangle[i].h = this.height;
            }

            texture.Render(this.position.x, this.position.y, renderer, rectangle[(int)this.state]);
        }
    }

    enum ButtonState
    {
        MouseOut = 0,
        MouseOverMotion = 1,
        MouseDown = 2,
        MouseUp = 3,
        Total
    }

    SDL.SDL_FPoint position = new SDL.SDL_FPoint();
    ButtonState state = ButtonState.MouseOut;
}
