namespace EnrollmentSystem
{
    public class StudentSchedule
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string room { get; set; } = string.Empty;

        public TimeSpan from_time { get; set; }

        public TimeSpan to_time { get; set; }

        public string days { get; set; }

        public string ProfFirstName { get; set; } = string.Empty;

        public string ProfLastName { get; set; } = string.Empty;

        public DateTime enrollment_date { get; set; }

        public int units { get; set; }
    }
}
