namespace Bat.AspNetCore
{
    public class BaseTagHelperModel : FormGroupModel
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Value { get; set; }
    }
}