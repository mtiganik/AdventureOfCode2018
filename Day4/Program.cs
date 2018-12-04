using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<DateTime, string> entries = new Dictionary<DateTime,string>();
			
			using (StreamReader sr = new StreamReader("input.txt"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					DateTime date = DateTime.Parse(line.Split(']')[0].TrimStart('['));
					entries.Add(date,line.Split(']')[1].Trim());
				}
			}

			var result = from entry in entries orderby entry.Key ascending select entry;
			bool wasFirst = true;

			int guardId = 0;
			Guard guard = new Guard();
			Guarding guarding = new Guarding();
			DateTime guardingDate = new DateTime();
			DateTime fallsSleep = new DateTime();
			DateTime wakesUp = new DateTime();
			List<Guard> guardslist = new List<Guard>();
			foreach (KeyValuePair<DateTime, string> kvp in result)
			{
				if (kvp.Value.Contains("Guard"))
				{
					if (wasFirst)
					{
						wasFirst = false;
					}
					else
					{
						// stuff to save last entry
						if (guardslist.Where(t => t.GuardId == guardId).Count() == 0) {
							guard.Guardings.Add(guarding);
							guardslist.Add(guard);
							Console.WriteLine("new Guard: " + guard.GuardId);
						}
						else
						{
							int index = guardslist.FindIndex(c => c.GuardId == guardId);
							var guardToAdd = guardslist[index];
							guardToAdd.Guardings.Add(guarding);
							guardslist[index] = guardToAdd;
							Console.WriteLine("Updated: " + guardToAdd.GuardId);
						}
					}

					guardId = Convert.ToInt32(kvp.Value.Split('#')[1].Split(' ')[0]);
					if (guardslist.Where(t => t.GuardId == guardId).Count() > 1)
					{
						guard = guardslist.Where(t => t.GuardId == guardId).Single();
					}
					else
					{
						guard = new Guard(guardId);
					}

					guardingDate = kvp.Key;
					if (guardingDate.Hour == 23)
					{
						guardingDate = guardingDate.AddDays(1).Date;
					}
					guardingDate = guardingDate.Date;
					guarding = new Guarding(guardingDate);

					
					
				}
				if(kvp.Value.Contains("falls asleep"))
				{
					fallsSleep = kvp.Key;
				}
				if(kvp.Value.Contains("wakes up"))
				{
					wakesUp = kvp.Key;
					guarding.addSleeping(fallsSleep, wakesUp);
				}
			}
			Console.WriteLine("GuardList contains " +guardslist.Count);
			foreach (Guard g in guardslist)
			{
				Console.WriteLine(g.GuardId + "\t slept " + g.getTotalSleepingTime() + " minutes. Best time: " + g.MinuteWhenGuardMostlySleeps);
			}

			Console.WriteLine("last loop\n\n:");
			foreach(Guard g in guardslist)
			{
				Console.WriteLine(g.GuardId + "\t slept " + g.getHowManyTimesHeSleptOnThatMinute() + " times out of " + g.Guardings.Count() + " on minute " + g.MinuteWhenGuardMostlySleeps);

			}

			Console.WriteLine("Guard + g.id guarded x times and of those times slept ");
			Console.WriteLine("End of program");
			Console.ReadLine();
		}
	}

	class Guard
	{
		public int GuardId { get; set; }
		public List<Guarding> Guardings { get; set; } = new List<Guarding>();

		public int MinuteWhenGuardMostlySleeps { get { return getTimeWhenMostTimeIsGuardSleeping(); } }

		public Guard(int id)
		{
			this.GuardId = id;
		}

		public Guard() { }

		public int getTotalSleepingTime()
		{
			int totalGuardingCount = 0;
			foreach(Guarding g in Guardings)
			{
				totalGuardingCount += g.getSleepingTime();
			}
			return totalGuardingCount;
		}

		private int getTimeWhenMostTimeIsGuardSleeping()
		{
			int[] intArr = new int[60];
			for(int i = 0; i < 60; i++)
			{
				foreach (Guarding g in Guardings)
				{
					intArr[i] += g.didGuardSleptOnThatMinute(i);
				}
			}

			int returnMinute = -1;
			int maximumMinute = 0;
			for(int i = 0; i < 60; i++)
			{
				if(maximumMinute < intArr[i])
				{
					maximumMinute = intArr[i];
					returnMinute = i;
				}
			}

			return returnMinute;
		}

		public int getHowManyTimesHeSleptOnThatMinute()
		{
			int count = 0;
			int minute = MinuteWhenGuardMostlySleeps;
			foreach (Guarding g in Guardings)
			{
				count += g.didGuardSleptOnThatMinute(minute);
			}
			return count;

		}
	}

	class Guarding
	{
		
		public DateTime Date { get; set; }
		public List<KeyValuePair<DateTime, DateTime>> sleepingsInOneDay { get; set; } = new List<KeyValuePair<DateTime, DateTime>>();

		public Guarding(DateTime date)
		{
			this.Date = date;
		}
		public Guarding() { }

		public void addSleeping(DateTime fallsSleep, DateTime wakesUp)
		{
			KeyValuePair<DateTime, DateTime> newEntry = new KeyValuePair<DateTime, DateTime>(fallsSleep, wakesUp);
			sleepingsInOneDay.Add(newEntry);
		}

		public int getSleepingTime()
		{
			int returner = 0;
			foreach(KeyValuePair<DateTime, DateTime> kvp in sleepingsInOneDay)
			{
				returner += kvp.Value.Minute - kvp.Key.Minute;
			}
			return returner;
		}

		public int GetSleepingCount()
		{
			return sleepingsInOneDay.Count;
		}

		public int didGuardSleptOnThatMinute(int minute)
		{
			foreach (KeyValuePair<DateTime, DateTime> kvp in sleepingsInOneDay)
			{
				if (kvp.Key.Minute <= minute && kvp.Value.Minute > minute) return 1;
			}
			return 0;
		}


	}
}
