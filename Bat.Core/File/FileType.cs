namespace Bat.Core;

public enum FileType : byte
{
    [Description(nameof(DisplayName.Unknown))]
    Unknown = 0,

    [Description(nameof(DisplayName.Image))]
    Image = 1,

    [Description(nameof(DisplayName.Document))]
    Document = 2,

    [Description(nameof(DisplayName.Archive))]
    Archive = 3,

    [Description(nameof(DisplayName.Audio))]
    Audio = 4,

    [Description(nameof(DisplayName.Video))]
    Video = 5
}