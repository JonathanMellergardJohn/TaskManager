using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Data.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
    }
}
