namespace SODP.UI.Services
{
    public interface IPaginationCalculator
    {
        int Current { get; set; }
        int Left { get; }
        int Right { get; }
        int Total { get; set; }
        int Width { get; set; }
    }
}