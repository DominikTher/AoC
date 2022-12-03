using AoC.Console.Interfaces;

namespace AoC.Console;

internal sealed class DayExecuter : IDayExecuter
{
	private readonly IEnumerable<IDay> days;
	private readonly IInput input;
	private readonly IConsoleWriter writer;

	public DayExecuter(IEnumerable<IDay> days, IInput input, IConsoleWriter writer)
	{
		this.days = days;
		this.input = input;
		this.writer = writer;
	}

	public async Task Execute(int year, int dayNumber)
	{
		var day = days.First(d => d.Year == year && d.DayNumber == dayNumber);
		var rows = await input.GetRows(year, day.DayNumber);

		var partOneResult = day.PartOne(rows);
		writer.WriteLine($"PartOne: {partOneResult}");

		var partTwoResult = day.PartTwo(rows);
		writer.WriteLine($"PartTwo: {partTwoResult}");
	}
}
