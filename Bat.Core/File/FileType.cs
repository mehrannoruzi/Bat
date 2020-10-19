namespace Bat.Core
{
    public enum FileType : byte
    {
        [LocalizeDescription(nameof(DisplayName.Unknown), typeof(DisplayName))]
        Unknown = 0,

        [LocalizeDescription(nameof(DisplayName.Image), typeof(DisplayName))]
        Image = 1,

        [LocalizeDescription(nameof(DisplayName.Document), typeof(DisplayName))]
        Document = 2,

        [LocalizeDescription(nameof(DisplayName.Archive), typeof(DisplayName))]
        Archive = 3,

        [LocalizeDescription(nameof(DisplayName.Audio), typeof(DisplayName))]
        Audio = 4,

        [LocalizeDescription(nameof(DisplayName.Video), typeof(DisplayName))]
        Video = 5
    }
}
