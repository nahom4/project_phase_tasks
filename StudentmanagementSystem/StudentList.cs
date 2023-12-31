using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

public class StudentList<T> where T : Student
{
    static string JsonFilePath = "Student.json";
    public static List<T> Container = new List<T>();

    public static void AddStudent(T StudentToBeAdded)
    {
        Container.Add(StudentToBeAdded);
        
    }

    public static List<T> SelectStudentsByName(string StudentName)
    {
        return Container.Where(s => s.Name == StudentName).ToList();
    }

    public static List<T> SelectStudentsById(int Id)
    {
        return Container.Where(s => s.RollNumber == Id).ToList();
    }

    public static void DisplayAllStudents()
    {   
        foreach (T s in Container)
        {   
            DisplayStudent(s);
        }
    }

    public static void DisplayGroupOfStudents(List<T> GroupOfStudents)
    {  
        foreach (T s in GroupOfStudents)
        { 
            DisplayStudent(s);
        }
    }

    public static void DisplayStudent(T StudentToBeDisplayed)
    {
        Console.WriteLine($"Name ------------------------- {StudentToBeDisplayed.Name}");
        Console.WriteLine($"Age -------------------------- {StudentToBeDisplayed.Age}");
        Console.WriteLine($"Roll Number ------------------  {StudentToBeDisplayed.RollNumber}");
        Console.WriteLine($"Grade --------------------------{StudentToBeDisplayed.Grade}");
        Console.WriteLine("------------------------------------------------");
    }

    public static async Task SerializeToJsonFile()
    {   
        string json = JsonConvert.SerializeObject(Container, Formatting.Indented);
        await File.WriteAllTextAsync(JsonFilePath, json);
    }

    public static async Task DeserializeFromJsonFile()
    {
        if (File.Exists(JsonFilePath))
        {
            string jsonData = await File.ReadAllTextAsync(JsonFilePath);
            Container = JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
        else
        {
            Console.WriteLine("JSON file not found. Initializing with an empty list.");
            Container =  new List<T>();
        }
    }
}
