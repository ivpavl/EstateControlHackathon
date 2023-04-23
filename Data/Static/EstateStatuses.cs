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
        // public static List<IngredientModel> IngredientList = new List<IngredientModel>()
        // {
        //     new IngredientModel(){Id = 1, Name = "Cheeze"},
        //     new IngredientModel(){Id = 2, Name = "Chicken"},
        //     new IngredientModel(){Id = 3, Name = "Carrot"},
        //     new IngredientModel(){Id = 4, Name = "Pineapple"},

        // };
        // public static Dictionary<int, int> PizzaIngredientsIdToPrice = new Dictionary<int, int>()
        // {
        //     {1, 50},
        //     {2, 60},
        //     {3, 70},
        //     {4, 20},
        // };
        
    }
}