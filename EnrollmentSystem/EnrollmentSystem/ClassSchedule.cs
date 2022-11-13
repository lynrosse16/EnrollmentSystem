namespace EnrollmentSystem
{
    public class ClassSchedule
    {
        public int class_schedule_id { get; set; }

        public int professor_id { get; set; }

        public int course_id { get; set; }

        public string room { get; set; } = string.Empty;

        public TimeSpan from_time { get; set; }

        public TimeSpan to_time { get; set; }

        public string days { get; set; }


    }
}
