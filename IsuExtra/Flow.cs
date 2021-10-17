using System.Collections.Generic;
using IsuExtra.Tools;
namespace IsuExtra
{
    public class Flow
    {
        private const int LongWeek = 6;
        private const string TimeFirstLesson = "8:20";
        private const string TimeSecondLesson = "10:00";
        private const string TimeThirdLesson = "11:40";
        private const string TimeFourthLesson = "13:30";
        private const string TimeFifthLesson = "15:20";
        private const string TimeSixthLesson = "17:00";
        private const string TimeSeventhLesson = "18:40";
        private static int _idNext = 0;
        private int _id;
        private int _spots;
        private Schedule _schedule;
        private List<StudentProfile> _students = new List<StudentProfile>();
        public Flow(int spots)
        {
            _spots = spots;
            _id = ++_idNext;
        }

        public void SetSpots(int newSpots)
        {
            _spots = newSpots;
        }

        public int GetSpots()
        {
            return _spots;
        }

        public bool IsStudentInFlow(StudentProfile studentProfile)
        {
            foreach (var item in _students)
            {
                if (item.GetId() == studentProfile.GetId())
                {
                    return true;
                }
            }

            return false;
        }

        public void AddSchedule(Schedule schedule)
        {
            _schedule = schedule;
        }

        public int GetId()
        {
            return _id;
        }

        public List<StudentProfile> GetListStudents()
        {
            return _students;
        }

        public void AddStudent(StudentProfile studentProfile)
        {
            if (_schedule == null)
            {
                throw new IsuExtraException("This flow hasn't a schedule");
            }

            CheckMatches(studentProfile);
            _students.Add(studentProfile);

            Schedule newSchedule = studentProfile.GetSchedule();
            for (int i = 0; i < LongWeek; i++)
            {
                foreach (Lesson ognpDaySchedule in _schedule.Lessons(i))
                {
                    switch (ognpDaySchedule.GetStartTime())
                    {
                        case TimeFirstLesson:
                            newSchedule.Lessons(i)[0] = ognpDaySchedule;
                            break;
                        case TimeSecondLesson:
                            newSchedule.Lessons(i)[1] = ognpDaySchedule;
                            break;
                        case TimeThirdLesson:
                            newSchedule.Lessons(i)[2] = ognpDaySchedule;
                            break;
                        case TimeFourthLesson:
                            newSchedule.Lessons(i)[3] = ognpDaySchedule;
                            break;
                        case TimeFifthLesson:
                            newSchedule.Lessons(i)[4] = ognpDaySchedule;
                            break;
                        case TimeSixthLesson:
                            newSchedule.Lessons(i)[5] = ognpDaySchedule;
                            break;
                        case TimeSeventhLesson:
                            newSchedule.Lessons(i)[6] = ognpDaySchedule;
                            break;
                    }
                }
            }

            studentProfile.SetShedule(newSchedule);
        }

        public void DeleteStudent(StudentProfile studentProfile)
        {
            for (int i = 0; i < _students.Count; i++)
            {
                if (_students[i].GetId() == studentProfile.GetId())
                {
                    _students.RemoveAt(i);
                    break;
                }
            }

            var newSchedule = studentProfile.GetSchedule();
            for (int i = 0; i < LongWeek; i++)
            {
                foreach (Lesson ognpDaySchedule in _schedule.Lessons(i))
                {
                    switch (ognpDaySchedule.GetStartTime())
                    {
                        case TimeFirstLesson:
                            newSchedule.Lessons(i)[0] = new Lesson();
                            break;
                        case TimeSecondLesson:
                            newSchedule.Lessons(i)[1] = new Lesson();
                            break;
                        case TimeThirdLesson:
                            newSchedule.Lessons(i)[2] = new Lesson();
                            break;
                        case TimeFourthLesson:
                            newSchedule.Lessons(i)[3] = new Lesson();
                            break;
                        case TimeFifthLesson:
                            newSchedule.Lessons(i)[4] = new Lesson();
                            break;
                        case TimeSixthLesson:
                            newSchedule.Lessons(i)[5] = new Lesson();
                            break;
                        case TimeSeventhLesson:
                            newSchedule.Lessons(i)[6] = new Lesson();
                            break;
                    }
                }
            }

            studentProfile.SetShedule(newSchedule);
        }

        private void CheckMatches(StudentProfile studentProfile)
        {
            Schedule schedule = studentProfile.GetSchedule();
            for (int i = 0; i < LongWeek; i++)
            {
                foreach (Lesson userDayLesson in schedule.Lessons(i))
                {
                    foreach (Lesson ognpDayLesson in _schedule.Lessons(i))
                    {
                        if (userDayLesson.GetStartTime() == ognpDayLesson.GetStartTime() && userDayLesson.GetStartTime() != null)
                        {
                            throw new IsuExtraException("Find a matches of schedules");
                        }
                    }
                }
            }
        }
    }
}