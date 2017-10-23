namespace CifFile.Lib
{
    public class FieldInfo
    {
        public FieldInfo(string name, int length)
        {
            Name = name;
            Length = length;
        }

        public string Name { get; private set; }

        public int Length { get; private set; }
    }
}