namespace SODP.UI.Services;

public interface IPaginationCalculator
{
	(int Left, int Right) Calculate(int total, int margin, int current);
}