using System;

namespace SODP.UI.Services
{
    public class PaginationCalculator : IPaginationCalculator
    {
        public int Total { get; set; }
        public int Margin { get; set; }
        public int Current { get; set; }

        public PaginationCalculator(int total, int margin, int current)
        {
            Total = total;
            Margin = margin;
            Current = current;
        }

        public int Left
        {
            get
            {
                if(Current - Margin > 3)
                {
                    return Current - Margin;
				}

                return 2;
            }
        }

        public int Right
        {
            get
            {
                if (Current + Margin < Total - 2)
                {
                    return Current + Margin;  
                }

                return Total - 1;
            }
        }
    }
}
