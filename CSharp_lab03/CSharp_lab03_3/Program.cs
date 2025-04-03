class Currency
{
    public double Value { get; set; }

    public Currency(double value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}

// Класс для USD
class CurrencyUSD : Currency
{
    public static double UsdToEur { get; set; } // Курс USD к EUR
    public static double UsdToRub { get; set; } // Курс USD к RUB

    public CurrencyUSD(double value) : base(value) { }

    // Преобразование из USD в EUR
    public static implicit operator CurrencyEUR(CurrencyUSD currencyUSD)
    {
        return new CurrencyEUR(currencyUSD.Value * UsdToEur);
    }

    // Преобразование из USD в RUB
    public static explicit operator CurrencyRUB(CurrencyUSD currencyUSD)
    {
        return new CurrencyRUB(currencyUSD.Value * UsdToRub);
    }
}

// Класс для EUR
class CurrencyEUR : Currency
{
    public static double EurToUsd { get; set; } // Курс EUR к USD
    public static double EurToRub { get; set; } // Курс EUR к RUB

    public CurrencyEUR(double value) : base(value) { }

    // Преобразование из EUR в USD
    public static implicit operator CurrencyUSD(CurrencyEUR currencyEUR)
    {
        return new CurrencyUSD(currencyEUR.Value * EurToUsd);
    }

    // Преобразование из EUR в RUB
    public static explicit operator CurrencyRUB(CurrencyEUR currencyEUR)
    {
        return new CurrencyRUB(currencyEUR.Value * EurToRub);
    }
}

// Класс для RUB
class CurrencyRUB : Currency
{
    public static double RubToUsd { get; set; } // Курс RUB к USD
    public static double RubToEur { get; set; } // Курс RUB к EUR

    public CurrencyRUB(double value) : base(value) { }

    // Преобразование из RUB в USD
    public static implicit operator CurrencyUSD(CurrencyRUB currencyRUB)
    {
        return new CurrencyUSD(currencyRUB.Value * RubToUsd);
    }

    // Преобразование из RUB в EUR
    public static explicit operator CurrencyEUR(CurrencyRUB currencyRUB)
    {
        return new CurrencyEUR(currencyRUB.Value * RubToEur);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Ввод курсов обмена
        Console.Write("Введите курс обмена между долларом США и рублём: ");
        CurrencyUSD.UsdToRub = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите курс обмена между Евро и рублём: ");
        CurrencyEUR.EurToRub = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите курс обмена между долларом США и евро: ");
        CurrencyUSD.UsdToEur = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите курс обмена между рублем и евро: ");
        CurrencyRUB.RubToEur = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите курс обмена между рублем и долларом США: ");
        CurrencyRUB.RubToUsd = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите курс обмена между евро и долларом США: ");
        CurrencyEUR.EurToUsd = Convert.ToDouble(Console.ReadLine());

        // Пример использования
        double quantity = 100;
        CurrencyUSD usd = new CurrencyUSD(quantity);
        CurrencyEUR eur = (CurrencyEUR)usd; // Преобразование USD в EUR
        CurrencyRUB rub = (CurrencyRUB)usd; // Преобразование USD в RUB

        Console.WriteLine($"{quantity} USD = {eur} EUR");
        Console.WriteLine($"{quantity} USD = {rub} RUB");

        // Преобразование RUB в EUR
        CurrencyRUB rubles = new CurrencyRUB(quantity);
        CurrencyEUR eurosFromRubles = (CurrencyEUR)rubles;
        Console.WriteLine($"{quantity} RUB = {eurosFromRubles} EUR");

        // Преобразование EUR в USD
        CurrencyEUR euros = new CurrencyEUR(quantity);
        CurrencyUSD dollarsFromEuros = (CurrencyUSD)euros;
        Console.WriteLine($"{quantity} EUR = {dollarsFromEuros} USD");
    }
}
