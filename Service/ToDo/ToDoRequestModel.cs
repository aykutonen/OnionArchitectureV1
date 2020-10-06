using System.ComponentModel.DataAnnotations;

namespace Service
{
    public class ToDoRequestModel
    {
        public class Create
        {
            public long UserId { get; set; }
            [Required(ErrorMessage = "Description alanı gerekli"),
            StringLength(500, ErrorMessage = "Açıklama uzunluğu en fazla 500 karakter uzunluğunda olmalıdır.")]
            public string Description { get; set; }
            public int Order { get; set; } = 0;
        }

        public class Update
        {
            [Required,
                StringLength(500, MinimumLength = 1, ErrorMessage = "Açıklama uzunluğu en az 1 en fazla 500 karakter uzunluğunda olmalıdır.")]
            public string Description { get; set; }
            public int Order { get; set; } = 1;
            public int Status { get; set; }
        }
    }
}
