namespace EnrollmentSystem
{
    public class StudentCourse
    {
        public int student_course_id { get; set; }

        public int student_id { get; set; }

        public int course_id { get; set; }

        public int class_schedule_id { get; set; }

        public DateTime enrollment_date { get; set; }

        public int units { get; set; }
    }
}
