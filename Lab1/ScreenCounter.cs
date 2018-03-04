using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public static class ScreenCounter
    {
        public static ScreenCounterResult Count(int roadLength, int screensQuantity, List<Screen> screens)
        {
            List<ScreenCounterResult> result = new List<ScreenCounterResult>();

            for (int roadPosition = 0; roadPosition <= roadLength; roadPosition++)
            {
                Screen screen = screens.Where(s => s.Position == roadPosition).FirstOrDefault();
                int roadPositionPrice = 0;
                List<int> roadPositions = new List<int>();

                if (screen != null)
                {
                    int previousScreenPrice = roadPosition < 1 ? 0 : result[roadPosition - 1].MaxPrice;
                    int closestScreenAtFiveMilesPrice = roadPosition < 6 ? 0 : result[roadPosition - 6].MaxPrice;

                    List<int> closestRoadPositions;
                    if (previousScreenPrice > closestScreenAtFiveMilesPrice + screen.Price)
                    {
                        roadPositionPrice = previousScreenPrice;
                        closestRoadPositions = roadPosition < 1 ? roadPositions : result[roadPosition - 1].ScreensNumbers;
                    }
                    else
                    {
                        roadPositionPrice = closestScreenAtFiveMilesPrice + screen.Price;
                        closestRoadPositions = roadPosition < 6 ? roadPositions : result[roadPosition - 6].ScreensNumbers;
                        roadPositions.Add(roadPosition);
                    }

                    roadPositions.AddRange(closestRoadPositions);
                }
                else
                {
                    roadPositionPrice = roadPosition < 1 ? 0 : result[roadPosition - 1].MaxPrice;
                    roadPositions = roadPosition < 1 ? roadPositions : result[roadPosition - 1].ScreensNumbers;
                }

                result.Add(new ScreenCounterResult(roadPositionPrice, roadPositions.OrderBy(rp => rp).ToList()));
            }

            return result.Last();
        }
    }

    public class Screen
    {
        public int Position { get; set; }

        public int Price { get; set; }

        public Screen(int position, int price)
        {
            Position = position;
            Price = price;
        }
    }

    public class ScreenCounterResult
    {
        public int MaxPrice { get; set; }

        public List<int> ScreensNumbers { get; set; }

        public ScreenCounterResult(int maxPrice, List<int> screensNumbers)
        {
            MaxPrice = maxPrice;
            ScreensNumbers = screensNumbers;
        }
    }
}
