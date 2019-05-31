namespace ArcanysExam.Models
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public class File : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Blob { get; set; }
    }
}
