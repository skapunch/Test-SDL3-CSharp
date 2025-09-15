using System.Data;

namespace TinTin;

public static class VsyncFlags
{
    public const int SDL_RENDERER_VSYNC_DISABLED = 0;
    public const int SDL_RENDERER_VSYNC_ENABLED = 1;
    public const int SDL_RENDERER_VSYNC_ADAPTIVE = -1;
    
}

public static class SymbolicConstants
{
    public const int PerSecondNS = 1000000000;
}