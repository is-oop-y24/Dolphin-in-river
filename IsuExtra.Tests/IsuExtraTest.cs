using Isu;
using System.Collections.Generic;
using IsuExtra.Tools;
using NUnit.Framework;
namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IIsuExtraService _isuExtraService;
        
        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
        }

        public Schedule GeneratorEmptyWeek()
        {
            return new Schedule();
        }
        public ScheduleDay GenerateScheduleDayStudentMonday()
        {
            var FirstLessonMonday = new Lesson("8:20", "9:50", "Egorov", 332);
            var SecondLessonMonday = new Lesson("10:00", "11:30", "Gert", 331);
            var LessonsMonday = new List<Lesson>();
            LessonsMonday.Add(FirstLessonMonday);
            LessonsMonday.Add(SecondLessonMonday);
            LessonsMonday.Add(new Lesson());
            LessonsMonday.Add(new Lesson());
            LessonsMonday.Add(new Lesson());
            LessonsMonday.Add(new Lesson());
            LessonsMonday.Add(new Lesson());
            return new ScheduleDay(LessonsMonday);
        }
        public ScheduleDay GenerateScheduleDayStudentTuesday()
        {
            var FourthLessonTuesday = new Lesson("13:30", "15:00", "Mayatin", 466);
            var LessonsTuesday = new List<Lesson>();
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(FourthLessonTuesday);
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(new Lesson());
            return new ScheduleDay(LessonsTuesday);
        }

        public Schedule GenerateScheduleStudent()
        {
            Schedule schedule = GeneratorEmptyWeek();
            schedule.AddInfo(0, GenerateScheduleDayStudentMonday());
            schedule.AddInfo(1, GenerateScheduleDayStudentTuesday());
            return schedule;
        }
        public Schedule GenerateScheduleSecondStudent()
        {
            Schedule schedule = GeneratorEmptyWeek();
            schedule.AddInfo(0, GenerateScheduleDayStudentMonday());
            return schedule;
        }
        
        public Schedule GenerateScheduleOGNP()
        {
            Schedule schedule = GeneratorEmptyWeek();
            var FourthLessonTuesday = new Lesson("13:30", "15:00", "Ivanov", 111);
            var LessonsTuesday = new List<Lesson>();
            LessonsTuesday.Add(FourthLessonTuesday);
            var scheduleTuesday = new ScheduleDay(LessonsTuesday);
            schedule.AddInfo(1, scheduleTuesday);
            return schedule;
        }
        public Schedule GenerateScheduleOGNPWithOutMatching()
        {
            Schedule schedule = GeneratorEmptyWeek();
            var ThirdLessonTuesday = new Lesson("11:40", "13:10", "Ivanov", 111);
            var LessonsTuesday = new List<Lesson>();
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(ThirdLessonTuesday);
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(new Lesson());
            LessonsTuesday.Add(new Lesson());
            var scheduleTuesday = new ScheduleDay(LessonsTuesday);
            schedule.AddInfo(1, scheduleTuesday);
            return schedule;
        }
        
        [Test]
        public void CheckAddOgnp()
        {
            Setup();
            var itip = new MegaFaculty('M', "ITIP");
            OGNP ognp = _isuExtraService.AddOgnp(itip);
            if (_isuExtraService.CheckContainsOgnp(ognp))
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        [Test]
        public void RecordStudentForOgnpWithMatching()
        {
            Setup();
            var itip = new MegaFaculty('M', "ITIP");
            OGNP ognp = _isuExtraService.AddOgnp(itip);
            
            Flow flow1 = ognp.AddFlow(10);
            flow1.AddSchedule(GenerateScheduleOGNP());
            
            var isuService = new IsuService();
            Group group1 = isuService.AddGroup("R3201");
            Student student = isuService.AddStudent(group1, "James");

            var studentProfile1 = new StudentProfile(student, GenerateScheduleStudent());
            Assert.Catch<IsuExtraException>(() =>
            {
                ognp.AddStudent(studentProfile1, flow1);
            });
        }
        
        [Test]
        public void AnnulRecordOgnp()
        {
            Setup();
            var itip = new MegaFaculty('M', "ITIP");
            OGNP ognp = _isuExtraService.AddOgnp(itip);
            
            Flow flow1 = ognp.AddFlow(10);
            flow1.AddSchedule(GenerateScheduleOGNPWithOutMatching());
            
            var isuService = new IsuService();
            Group group1 = isuService.AddGroup("R3201");
            Student student = isuService.AddStudent(group1, "James");
            
            var studentProfile1 = new StudentProfile(student, GenerateScheduleStudent());
            ognp.AddStudent(studentProfile1, flow1);
            flow1.DeleteStudent(studentProfile1);
            if (flow1.FindStudent(studentProfile1))
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        [Test]
        public void GetFlow()
        {
            Setup();
            var itip = new MegaFaculty('M', "ITIP");
            OGNP ognp = _isuExtraService.AddOgnp(itip);
            
            Flow flow1 = ognp.AddFlow(10);
            Flow flow2 = ognp.AddFlow(20);
            if (ognp.GetFlows().Count == 2)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        [Test]
        public void GetListStudentByGroupNumber()
        {
            Setup();
            var itip = new MegaFaculty('M', "ITIP");
            OGNP ognp = _isuExtraService.AddOgnp(itip);
            
            Flow flow1 = ognp.AddFlow(10);
            flow1.AddSchedule(GenerateScheduleOGNPWithOutMatching());
            
            var isuService = new IsuService();
            Group group1 = isuService.AddGroup("R3201");
            Student student = isuService.AddStudent(group1, "James");
            Student student2 = isuService.AddStudent(group1, "Bond");
            
            var studentProfile1 = new StudentProfile(student, GenerateScheduleStudent());
            var studentProfile2 = new StudentProfile(student2, GenerateScheduleSecondStudent());
            ognp.AddStudent(studentProfile1, flow1);
            ognp.AddStudent(studentProfile2, flow1);
            if (flow1.GetListStudents().Count == 2)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        
        [Test]
        public void GetListNotRecordStudentsInGroup()
        {
            Setup();
            var itip = new MegaFaculty('M', "ITIP");
            OGNP ognp = _isuExtraService.AddOgnp(itip);
            
            Flow flow1 = ognp.AddFlow(10);
            flow1.AddSchedule(GenerateScheduleOGNPWithOutMatching());
            
            var isuService = new IsuService();
            Group group1 = isuService.AddGroup("R3201");
            Student student = isuService.AddStudent(group1, "James");
            Student student2 = isuService.AddStudent(group1, "Bond");
            
            var studentProfile1 = new StudentProfile(student, GenerateScheduleStudent());
            ognp.AddStudent(studentProfile1, flow1);
            if (_isuExtraService.StudentsNotJoin(group1).Count == 1)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}