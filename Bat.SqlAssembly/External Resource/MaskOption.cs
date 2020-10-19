namespace Bat.Sql
{
    public class MaskOption
    {
        public MaskMode Mode { get; set; }
        public char MaskWith { get; set; }
        public int MaskLength { get; set; }
        public int MaxMaskLength { get; set; }
    }
}
