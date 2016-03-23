using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewPrep.Helpers;

namespace InterviewPrep.Questions
{
    public class SchedulingConflict: Runable, IQuestion<Appointment[], ComparableArray<Appointment>>
    {
        private ExpectedInputAndOutput<Appointment[], ComparableArray<Appointment>>[] _output;

        public SchedulingConflict()
        {
            List<ExpectedInputAndOutput<Appointment[], ComparableArray<Appointment>>> output = new List<ExpectedInputAndOutput<Appointment[], ComparableArray<Appointment>>>();
            DateTime now = DateTime.Now;

            output.Add(new ExpectedInputAndOutput<Appointment[], ComparableArray<Appointment>>() {Input = new []{new Appointment() {StartTime = now, EndTime = now.AddDays(1)}}, Output = new ComparableArray<Appointment>() { Array = new[] { new Appointment() { StartTime = now, EndTime = now.AddDays(1) } } } });
            output.Add(new ExpectedInputAndOutput<Appointment[], ComparableArray<Appointment>>()
            {
                Input = new[]
                {
                    new Appointment() {StartTime = now, EndTime = now.AddDays(1)},
                    new Appointment() {StartTime = now.AddHours(3), EndTime = now.AddHours(4)},
                    new Appointment() {StartTime = now.AddHours(1), EndTime = now.AddHours(2)},
                    new Appointment() {StartTime = now.AddHours(4), EndTime = now.AddHours(5)},
                    new Appointment() {StartTime = now.AddHours(2), EndTime = now.AddHours(3)},
                    new Appointment() {StartTime = now.AddHours(5), EndTime = now.AddHours(6)}
                },
                Output =
                    new ComparableArray<Appointment>()
                    {
                        Array = new[]
                        {
                            new Appointment() {StartTime = now, EndTime = now.AddDays(1), IsConflict = true},
                            new Appointment() {StartTime = now.AddHours(3), EndTime = now.AddHours(4), IsConflict = true},
                            new Appointment() {StartTime = now.AddHours(1), EndTime = now.AddHours(2), IsConflict = true},
                            new Appointment() {StartTime = now.AddHours(4), EndTime = now.AddHours(5), IsConflict = true},
                            new Appointment() {StartTime = now.AddHours(2), EndTime = now.AddHours(3), IsConflict = true},
                            new Appointment() {StartTime = now.AddHours(5), EndTime = now.AddHours(6), IsConflict = true}
                        }
                    }
            });

            output.Add(new ExpectedInputAndOutput<Appointment[], ComparableArray<Appointment>>()
            {
                Input = new[]
                {
                    new Appointment() {StartTime = now.AddHours(3), EndTime = now.AddHours(4)},
                    new Appointment() {StartTime = now.AddHours(1), EndTime = now.AddHours(2)},
                    new Appointment() {StartTime = now.AddHours(4), EndTime = now.AddHours(5)},
                    new Appointment() {StartTime = now.AddHours(2), EndTime = now.AddHours(3)},
                    new Appointment() {StartTime = now.AddHours(5), EndTime = now.AddHours(6)}
                },
                Output =
                    new ComparableArray<Appointment>()
                    {
                        Array = new[]
                        {
                            new Appointment() {StartTime = now.AddHours(3), EndTime = now.AddHours(4)},
                            new Appointment() {StartTime = now.AddHours(1), EndTime = now.AddHours(2)},
                            new Appointment() {StartTime = now.AddHours(4), EndTime = now.AddHours(5)},
                            new Appointment() {StartTime = now.AddHours(2), EndTime = now.AddHours(3)},
                            new Appointment() {StartTime = now.AddHours(5), EndTime = now.AddHours(6)}
                        }
                    }
            });

            output.Add(new ExpectedInputAndOutput<Appointment[], ComparableArray<Appointment>>()
            {
                Input = new[]
                {
                    new Appointment() {StartTime = now.AddHours(3), EndTime = now.AddHours(4)},
                    new Appointment() {StartTime = now.AddHours(1), EndTime = now.AddHours(2)},
                    new Appointment() {StartTime = now.AddHours(4), EndTime = now.AddHours(6)},
                    new Appointment() {StartTime = now.AddHours(2), EndTime = now.AddHours(3)},
                    new Appointment() {StartTime = now.AddHours(5), EndTime = now.AddHours(6)}
                },
                Output =
                    new ComparableArray<Appointment>()
                    {
                        Array = new[]
                        {
                            new Appointment() {StartTime = now.AddHours(3), EndTime = now.AddHours(4)},
                            new Appointment() {StartTime = now.AddHours(1), EndTime = now.AddHours(2)},
                            new Appointment() {StartTime = now.AddHours(4), EndTime = now.AddHours(6), IsConflict = true},
                            new Appointment() {StartTime = now.AddHours(2), EndTime = now.AddHours(3)},
                            new Appointment() {StartTime = now.AddHours(5), EndTime = now.AddHours(6), IsConflict = true}
                        }
                    }
            });

            _output = output.ToArray();
        }

        public ExpectedInputAndOutput<Appointment[], ComparableArray<Appointment>>[] TestValues {
            get { return _output; }
        }
        public ComparableArray<Appointment> Run(Appointment[] input)
        {
            var sortedAppointments = input.OrderBy(i => i.StartTime).ToArray();

            Appointment current = sortedAppointments[0];
            current.IsConflict = sortedAppointments.Length > 1 && sortedAppointments[1].StartTime < current.EndTime;
            Appointment max = current;
            DateTime maxEnd = sortedAppointments[0].EndTime;
            
            for(int i = 1; i < sortedAppointments.Length; i++)
            {
                current = sortedAppointments[i];
                if (current.StartTime < maxEnd)
                {
                    current.IsConflict = true;
                    max.IsConflict = true;
                }
                if (current.EndTime > maxEnd)
                {
                    maxEnd = current.EndTime;
                    max = current;
                }
            }

            return new ComparableArray<Appointment>() {Array = input};
        }

        public string QuestionName {
            get { return "If you had a list of appointments (each appointment has a   begin time, an end time, and a boolean hasConflict), how would you efficiently go through them and set the isConflict boolean for each. You cannot assume they are sorted in any way. Keep in mind that one appointment may be very long, etc"; }
        }
    }

    public class Appointment : ICloneable, IComparable, IComparable<Appointment>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsConflict { get; set; }

        public object Clone()
        {
            return new Appointment()
            {
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                IsConflict = this.IsConflict
            };
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Appointment);
        }

        public int CompareTo(Appointment other)
        {
            if (other == null)
                return -1;

            if (StartTime == other.StartTime && EndTime == other.EndTime && IsConflict == other.IsConflict)
                return 0;

            return 1;
        }
    }
}
