using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ClientsApi.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long CPF { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }



        public ICollection<string> PhoneNumbersList { get; set; }

        [Required]
        [JsonIgnore]
        public string PhoneNumbers
        {
            get { return string.Join("|", PhoneNumbersList); }
            set { PhoneNumbersList = value.Split('|').ToList(); }
        }



        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Zip { get; set; }

        public Client()
        {
            this.PhoneNumbersList = new List<string>();
        }
    }
}