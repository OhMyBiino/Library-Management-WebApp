using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Areas.Identity.Data;
namespace LibraryManagement.Models
{
    public class TransactionModel
    {

        [Key]
        public Guid TransactionID { get; set; } = Guid.NewGuid();

        [Required]
        public string UserID { get; set; }

        [Required]
        public LibraryManagementUser User{ get; set; }

        [MaxLength(100)]
        public string BorrowerName{ get; set; }

        [Required]
        public string TransactionType { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime DueDate 
        {
            get => BorrowDate.AddDays(3);
            private set { }
        }

    }
}
