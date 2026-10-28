namespace PersonService.dtos
{
    public class CreatePersonRequest
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string DocumentNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }

        public List<IFormFile> Photos { get; set; } = [];
    }    
}