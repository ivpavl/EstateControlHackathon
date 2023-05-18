using TestTask.Models;

namespace TestTask.Data.Static
{
    public static class EstateStatuses
    {
        public static Dictionary<int, string> StatusDict = new Dictionary<int, string>()
        {
            {0, "Продано"},
            {1, "Активно"},
            {2, "В оренді"},
            {4, "Готово до оренди"},
            {5, "Архівовано"},
        };
    }
}