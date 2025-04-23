



using System;
using System.Collections.Generic;

public class HashTable
{
    private readonly List<KeyValuePair<string, string>>[] table;

    public HashTable(int size)
    {
        table = new List<KeyValuePair<string, string>>[size];
        for (int i = 0; i < size; i++)
        {
            table[i] = new List<KeyValuePair<string, string>>();
        }
    }

    private int GetIndex(string key)
    {
        int hash = key.GetHashCode();
        hash = Math.Abs(hash); // Zabráníme záporným indexům
        return hash % table.Length;
    }

    public void Insert(string key, string value)
    {
        int index = GetIndex(key);
        foreach (var pair in table[index])
        {
            if (pair.Key == key)
            {
                Console.WriteLine("Klíč už existuje. Pro přepsání implementuj Update.");
                return;
            }
        }
        table[index].Add(new KeyValuePair<string, string>(key, value));
    }

    public string Search(string key)
    {
        int index = GetIndex(key);
        foreach (var pair in table[index])
        {
            if (pair.Key == key)
                return pair.Value;
        }
        return null;
    }

    public bool Delete(string key)
    {
        int index = GetIndex(key);
        for (int i = 0; i < table[index].Count; i++)
        {
            if (table[index][i].Key == key)
            {
                table[index].RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public void PrintTable()
    {
        for (int i = 0; i < table.Length; i++)
        {
            Console.Write($"Index {i}: ");
            foreach (var pair in table[i])
            {
                Console.Write($"[{pair.Key}, {pair.Value}] ");
            }
            Console.WriteLine();
        }
    }
}

public class Program
{
    public static void Main()
    {
        HashTable ht = new HashTable(5); // Malá tabulka → větší šance kolizí

        ht.Insert("apple", "fruit");
        ht.Insert("car", "vehicle");
        ht.Insert("book", "object");
        ht.Insert("arc", "maybe collision?");
        ht.Insert("pac", "another?");

        ht.PrintTable();

        Console.WriteLine("\nHledání hodnot:");
        Console.WriteLine($"apple: {ht.Search("apple")}");
        Console.WriteLine($"book: {ht.Search("book")}");
        Console.WriteLine($"nonexistent: {ht.Search("nonexistent")}");

        Console.WriteLine("\nMazání:");
        Console.WriteLine($"Delete 'car': {ht.Delete("car")}");
        Console.WriteLine($"Delete 'nonexistent': {ht.Delete("nonexistent")}");

        ht.PrintTable();
    }
}
