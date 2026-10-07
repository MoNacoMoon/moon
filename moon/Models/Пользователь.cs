using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moon.Models
{
    [Table("Пользователь")]
    public class Пользователь
    {
        [Key]
        [Column("КодПользователя")]
        public int КодПользователя { get; set; }

        [Column("Фамилия")]
        public string Фамилия { get; set; } = string.Empty;

        [Column("Имя")]
        public string Имя { get; set; } = string.Empty;

        [Column("ЭлектроннаяПочта")]
        public string ЭлектроннаяПочта { get; set; } = string.Empty;

        [Column("Пароль")]
        public string Пароль { get; set; } = string.Empty;

        [Column("КодовоеСлово")]
        public string КодовоеСлово { get; set; } = string.Empty;

        [Column("ОтветНаСекретныйВопрос")]
        public string ОтветНаСекретныйВопрос { get; set; } = string.Empty;

        [Column("КодСекретногоВопроса")]
        public int КодСекретногоВопроса { get; set; }

        [ForeignKey("КодСекретногоВопроса")]
        public virtual СекретныйВопрос? СекретныйВопрос { get; set; }
    }
}
