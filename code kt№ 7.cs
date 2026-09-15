//тесты ро работе самого крафта
var station = new CraftingStation();
var fullInventory = new Dictionary<string, int> { ["изумруд"] = 3, ["Дерево"] = 3 };
var poorInventory = new Dictionary<string, int> { ["изумруд"] = 1, ["Дерево"] = 1 };

TryCraft("посох", 5, fullInventory);
TryCraft("посох", 5, poorInventory);
TryCraft("посох", 1, fullInventory);
TryCraft("посох", 5, fullInventory);

void TryCraft(string item, int level, Dictionary<string, int> inventory)
{
    try
    {
        station.Craft(item, level, inventory);
        Console.WriteLine($"Создан предмет: {item}");
    }
    catch (MissingIngredientException ex)
    {
        Console.WriteLine($"Не хватает: {ex.IngredientName}, нужно {ex.RequiredAmount}");
    }
    catch (InsufficientLevelException ex)
    {
        Console.WriteLine($"Нужен уровень {ex.RequiredLevel}, у вас {ex.CurrentLevel}");
    }
    catch (CraftingException ex)
    {
        Console.WriteLine($"Ошибка крафта: {ex.Message}");
    }
}

// 1
public class CraftingException : Exception
{
    public CraftingException() { }
    public CraftingException(string message) : base(message) { }
    public CraftingException(string message, Exception innerException) : base(message, innerException) { }
}          : base($"Не хватает ингредиента '{ingredientName}': нужно {requiredAmount}")
      {
          IngredientName = ingredientName;
          RequiredAmount = requiredAmount;
      }                                 
       }

  // 3
  public class InsufficientLevelException : CraftingException
  {
      public int RequiredLevel { get; }
      public int CurrentLevel { get; }

      public InsufficientLevelException(int requiredLevel, int currentLevel)
          : base($"Нужен уровень {requiredLevel}, текущий {currentLevel}")
      {
          RequiredLevel = requiredLevel;
          CurrentLevel = currentLevel;
      }
  }

  
  public class CraftingStation
  {
      private readonly Dictionary<string, (int Level, Dictionary<string, int> Ingredients)> recipes = new()
      {
          ["посох"] = (3, new() { ["изумруд"] = 3, ["Дерево"] = 3 })
      };

      public void Craft(string itemName, int playerLevel, Dictionary<string, int> inventory)
      {
          if (!recipes.TryGetValue(itemName, out var recipe))
              throw new CraftingException($"Рецепт '{itemName}' не найден");

          foreach (var (name, amount) in recipe.Ingredients)
              if (inventory.GetValueOrDefault(name) < amount)
                  throw new MissingIngredientException(name, amount);

          if (playerLevel < recipe.Level)
              throw new InsufficientLevelException(recipe.Level, playerLevel);
      }
  }


// 2
public class MissingIngredientException : CraftingException
{
    public string IngredientName { get; } = "";
    public int RequiredAmount { get; }

    public MissingIngredientException() { }
    public MissingIngredientException(string message) : base(message) { }
    public MissingIngredientException(string message, Exception innerException) : base(message, innerException) { }

    public MissingIngredientException(string ingredientName, int requiredAmount)
        : base($"Не хватает ингредиента '{ingredientName}': нужно {requiredAmount}")
    {
        IngredientName = ingredientName;
        RequiredAmount = requiredAmount;
    }
}

// 3
public class InsufficientLevelException : CraftingException
{
    public int RequiredLevel { get; }
    public int CurrentLevel { get; }

    public InsufficientLevelException() { }
    public InsufficientLevelException(string message) : base(message) { }
    public InsufficientLevelException(string message, Exception innerException) : base(message, innerException) { }

    public InsufficientLevelException(int requiredLevel, int currentLevel)
        : base($"Нужен уровень {requiredLevel}, текущий {currentLevel}")
    {
        RequiredLevel = requiredLevel;
        CurrentLevel = currentLevel;
    }
}

public class CraftingStation
{
    private readonly Dictionary<string, (int Level, Dictionary<string, int> Ingredients)> recipes = new()
    {
        ["посох"] = (3, new() { ["изумруд"] = 3, ["Дерево"] = 3 })
    };

    public void Craft(string itemName, int playerLevel, Dictionary<string, int> inventory)
    {
        if (!recipes.TryGetValue(itemName, out var recipe))
            throw new CraftingException($"Рецепт '{itemName}' не найден");

        foreach (var (name, amount) in recipe.Ingredients)
            if (inventory.GetValueOrDefault(name) < amount)
                throw new MissingIngredientException(name, amount);

        if (playerLevel < recipe.Level)
            throw new InsufficientLevelException(recipe.Level, playerLevel);
    }
}
