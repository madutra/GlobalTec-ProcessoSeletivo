using System.ComponentModel.DataAnnotations;

namespace GlobalTec.Models {
    public class User {
        [Key]
        public int Id { get; set; }
        public string UserCode { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string UF { get; set; }
        public string BirthDate { get; set; }

        //authentication vars;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
