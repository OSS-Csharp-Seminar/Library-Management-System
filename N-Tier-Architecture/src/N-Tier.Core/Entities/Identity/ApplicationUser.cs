using Microsoft.AspNetCore.Identity;

namespace N_Tier.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public string Oib { get; set; }

        public string IdCardNumber { get; set; }

        public ICollection<Loan> CustomerLoan { get; } = new List<Loan>();

        public ICollection<Loan> LibrarianLoan { get; } = new List<Loan>();
    }
}
