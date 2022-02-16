using System;

namespace SODP.UI.Services
{
    public class PaginationCalculator : IPaginationCalculator
    {
        public int Total { get; set; }
        public int Width { get; set; }
        public int Current { get; set; }

        public PaginationCalculator(int total, int width, int current)
        {
            Total = total;
            Width = width;
            Current = current;
        }

        public int Left
        {
            get
            {
                if ((Total < Width + 5) || (Current <= Width))
                {
                    return 2;
                }
                else if(Current > Total - Width)
                {
                    return Total - Width - 1;
                }
                return Current - (Width - 1) / 2;
            }
        }
        public int Right
        {
            get
            {
                if ((Total < Width + 5) || (Current < Width))
                {
                    return Math.Min(Width + 2, Total - 1);
                } 
                else if(Current > Total - Width)
                {
                    return Total - 1;
                }
                return Current + (Width - 1) / 2;
            }
        }
    }
}
