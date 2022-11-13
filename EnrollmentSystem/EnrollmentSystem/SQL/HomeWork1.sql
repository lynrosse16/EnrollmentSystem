USE [TEST_DATABASE]
GO

/****** Object:  Table [dbo].[student]    Script Date: 23/10/2022 5:38:58 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Student](
	[student_id] [int] NOT NULL,
	[FirstName] [nvarchar](150) NULL,
	[LastName] [nvarchar](150) NULL,
	[Address] [nvarchar](150) NULL,
) ON [PRIMARY]
GO

INSERT INTO Student (student_id, FirstName, LastName, Address)
VALUES (1, 'Rogel', 'Ignacio', 'Kablon, Tupi, So. Cotabato');

INSERT INTO Student (student_id, FirstName, LastName, Address)
VALUES (2, 'Phillip', 'Tampus', 'Mulig, Toril Davao City');

INSERT INTO Student (student_id, FirstName, LastName, Address)
VALUES (3, 'Rosselyn', 'Camacho', 'Davao City');

SELECT * FROM Student;


CREATE TABLE Course (
	course_id INT NOT NULL,
	courseName nvarchar(150) NOT NULL,
	description nvarchar(300) NULL,
)

INSERT INTO Course VALUES (1, 'SD1', 'Software Development');
INSERT INTO Course VALUES (2, 'SE1', 'Software Engineering');
INSERT INTO Course VALUES (3, 'WD1', 'Web Development');
SELECT * FROM Course;

UPDATE Course SET courseName = 'DBMS1', description = 'Database Management System' WHERE course_id = 1;
DELETE FROM Course WHERE course_id = 1 AND description = 'Software Engineering';

CREATE TABLE StudentCourse (
	student_course_id INT NOT NULL,
	student_id INT NOT NULL,
	course_id INT NOT NULL,
	class_schedule_id INT NOT NULL,
	enrollment_date DATETIME NOT NULL,
	units INT NOT NULL,
)

SELECT * FROM StudentCourse;

INSERT INTO StudentCourse (student_course_id, student_id, course_id, class_schedule_id, enrollment_date, units)
VALUES (1, 1, 1, 1, GETDATE(), 3);

INSERT INTO StudentCourse (student_course_id, student_id, course_id, class_schedule_id, enrollment_date, units)
VALUES (2, 1, 2, 2, GETDATE(), 3);

INSERT INTO StudentCourse (student_course_id, student_id, course_id, class_schedule_id, enrollment_date, units)
VALUES (3, 2, 1, 1, GETDATE(), 3);

SELECT Student.FirstName, Student.LastName, Course.courseName, Course.description, enrollment_date, units FROM StudentCourse
JOIN Student ON StudentCourse.student_id = Student.student_id
JOIN Course ON StudentCourse.course_id = Course.course_id;


CREATE TABLE Professor (
	professor_id INT NOT NULL,
	ProfFirstName [nvarchar](150) NULL,
	ProfLastName [nvarchar](150) NULL,
	ProfAddress [nvarchar](150) NULL,
)

SELECT * FROM Professor;

INSERT INTO Professor VALUES (1, 'Rose', 'Abad', 'Toril, Davao City');
INSERT INTO Professor VALUES (2, 'James', 'Diaz', 'Panacan, Davao City');
INSERT INTO Professor VALUES (3, 'April', 'Ermita', 'Buhangin, Davao City');


CREATE TABLE ClassSchedule (
	class_schedule_id INT NOT NULL,
	professor_id INT NOT NULL,
	course_id INT NOT NULL,
	room [nvarchar](150) NULL,
	from_time time NOT NULL,
	to_time time NOT NULL,
	days [nvarchar](500) NOT NULL,
)

INSERT INTO ClassSchedule VALUES (1, 1, 1, 'GEMMA-1', '09:00 AM', '11:00 AM', 'MWF');
INSERT INTO ClassSchedule VALUES (2, 2, 1, 'GEMMA-1', '01:00 PM', '03:00 PM', 'MWF');
INSERT INTO ClassSchedule VALUES (3, 1, 2, 'SCIENCE-1', '09:00 AM', '11:00 AM', 'TThS');

SELECT * FROM ClassSchedule;