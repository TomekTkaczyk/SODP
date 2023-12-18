namespace SODP.UI.Services;

public class PaginationCalculator : IPaginationCalculator
{
	private int _total;
	private int _margin;
	private int _current;

	public (int Left, int Right) Calculate(int total, int margin, int current)
	{
		_total = total;
		_margin = margin;
		_current = current;

		return(Left(),Right());
	}

	private int Left()
	{
		if (_current - _margin > 3)
		{
			return _current - _margin;
		}

		return 2;
	}

	private int Right()
	{
		if (_current + _margin < _total - 2)
		{
			return _current + _margin;
		}

		return _total - 1;
	}
}
