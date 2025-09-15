// NOTE: This file is auto-generated.
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.CompilerServices;
using System.Text;

namespace SDL3;

public static unsafe partial class SDL
{

    // /home/khoa-deb/SDL3-CS/SDL3-release/include/SDL3_mixer/SDL_mixer.h

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_Version();

    [Flags]
    public enum MIX_InitFlags : uint
    {
        MIX_INIT_FLAC = 0x00000001,
        MIX_INIT_MOD = 0x00000002,
        MIX_INIT_MP3 = 0x00000008,
        MIX_INIT_OGG = 0x00000010,
        MIX_INIT_MID = 0x00000020,
        MIX_INIT_OPUS = 0x00000040,
        MIX_INIT_WAVPACK = 0x00000080,
    }

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial MIX_InitFlags Mix_Init(MIX_InitFlags flags);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_Quit();

    [StructLayout(LayoutKind.Sequential)]
    public struct Mix_Chunk
    {
        public int allocated;
        public byte* abuf;
        public uint alen;
        public byte volume;
    }

    public enum Mix_Fading
    {
        MIX_NO_FADING = 0,
        MIX_FADING_OUT = 1,
        MIX_FADING_IN = 2,
    }

    public enum Mix_MusicType
    {
        MUS_NONE = 0,
        MUS_WAV = 1,
        MUS_MOD = 2,
        MUS_MID = 3,
        MUS_OGG = 4,
        MUS_MP3 = 5,
        MUS_FLAC = 6,
        MUS_OPUS = 7,
        MUS_WAVPACK = 8,
        MUS_GME = 9,
    }

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_OpenAudio(uint devid, IntPtr spec);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_PauseAudio(int pause_on);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_QuerySpec(out int frequency, out SDL_AudioFormat format, out int channels);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_AllocateChannels(int numchans);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Mix_Chunk* Mix_LoadWAV_IO(IntPtr src, SDLBool closeio);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Mix_Chunk* Mix_LoadWAV(string file);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Mix_LoadMUS(string file);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Mix_LoadMUS_IO(IntPtr src, SDLBool closeio);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Mix_LoadMUSType_IO(IntPtr src, Mix_MusicType type, SDLBool closeio);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Mix_Chunk* Mix_QuickLoad_WAV(byte* mem);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Mix_Chunk* Mix_QuickLoad_RAW(byte* mem, uint len);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_FreeChunk(Mix_Chunk* chunk);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_FreeMusic(IntPtr music);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_GetNumChunkDecoders();

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetChunkDecoder(int index);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_HasChunkDecoder(string name);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_GetNumMusicDecoders();

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetMusicDecoder(int index);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_HasMusicDecoder(string name);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Mix_MusicType Mix_GetMusicType(IntPtr music);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetMusicTitle(IntPtr music);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetMusicTitleTag(IntPtr music);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetMusicArtistTag(IntPtr music);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetMusicAlbumTag(IntPtr music);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetMusicCopyrightTag(IntPtr music);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Mix_MixCallback(IntPtr udata, byte* stream, int len);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_SetPostMix(Mix_MixCallback mix_func, IntPtr arg);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_HookMusic(Mix_MixCallback mix_func, IntPtr arg);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Mix_MusicFinishedCallback();

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_HookMusicFinished(Mix_MusicFinishedCallback music_finished);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr Mix_GetMusicHookData();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Mix_ChannelFinishedCallback(int channel);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_ChannelFinished(Mix_ChannelFinishedCallback channel_finished);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Mix_EffectFunc_t(int chan, IntPtr stream, int len, IntPtr udata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Mix_EffectDone_t(int chan, IntPtr udata);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_RegisterEffect(int chan, Mix_EffectFunc_t f, Mix_EffectDone_t d, IntPtr arg);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_UnregisterEffect(int channel, Mix_EffectFunc_t f);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_UnregisterAllEffects(int channel);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_SetPanning(int channel, byte left, byte right);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_SetPosition(int channel, short angle, byte distance);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_SetDistance(int channel, byte distance);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_SetReverseStereo(int channel, int flip);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_ReserveChannels(int num);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_GroupChannel(int which, int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_GroupChannels(int from, int to, int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_GroupAvailable(int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_GroupCount(int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_GroupOldest(int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_GroupNewer(int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_PlayChannel(int channel, Mix_Chunk* chunk, int loops);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_PlayChannelTimed(int channel, Mix_Chunk* chunk, int loops, int ticks);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_PlayMusic(IntPtr music, int loops);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_FadeInMusic(IntPtr music, int loops, int ms);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_FadeInMusicPos(IntPtr music, int loops, int ms, double position);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_FadeInChannel(int channel, Mix_Chunk* chunk, int loops, int ms);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_FadeInChannelTimed(int channel, Mix_Chunk* chunk, int loops, int ms, int ticks);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_Volume(int channel, int volume);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_VolumeChunk(Mix_Chunk* chunk, int volume);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_VolumeMusic(int volume);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_GetMusicVolume(IntPtr music);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_MasterVolume(int volume);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_HaltChannel(int channel);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_HaltGroup(int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_HaltMusic();

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_ExpireChannel(int channel, int ticks);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_FadeOutChannel(int which, int ms);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_FadeOutGroup(int tag, int ms);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_FadeOutMusic(int ms);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Mix_Fading Mix_FadingMusic();

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Mix_Fading Mix_FadingChannel(int which);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_Pause(int channel);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_PauseGroup(int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_Resume(int channel);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_ResumeGroup(int tag);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_Paused(int channel);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_PauseMusic();

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_ResumeMusic();

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_RewindMusic();

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_PausedMusic();

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_ModMusicJumpToOrder(int order);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_StartTrack(IntPtr music, int track);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_GetNumTracks(IntPtr music);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_SetMusicPosition(double position);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double Mix_GetMusicPosition(IntPtr music);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double Mix_MusicDuration(IntPtr music);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double Mix_GetMusicLoopStartTime(IntPtr music);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double Mix_GetMusicLoopEndTime(IntPtr music);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double Mix_GetMusicLoopLengthTime(IntPtr music);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Mix_Playing(int channel);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_PlayingMusic();

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_SetSoundFonts(string paths);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetSoundFonts();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool Mix_EachSoundFontCallback(string path, IntPtr data);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_EachSoundFont(Mix_EachSoundFontCallback function, IntPtr data);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDLBool Mix_SetTimidityCfg(string path);

    [LibraryImport("SDL3_mixer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    public static partial string Mix_GetTimidityCfg();

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Mix_Chunk* Mix_GetChunk(int channel);

    [LibraryImport("SDL3_mixer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Mix_CloseAudio();


}
