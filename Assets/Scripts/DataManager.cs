using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static List<Student> Students = new List<Student>();
    public static List<Teacher> Teachers = new List<Teacher>();

    void Awake()
    {

        Students.Clear();
        Students.Add(new Student { Name = "Arvin", Age = 18, Score = 100 });
        Students.Add(new Student { Name = "Bahman", Age = 13, Score = 45 });
        Students.Add(new Student { Name = "Cameron", Age = 22, Score = 76 });

        Teachers.Clear();
        Teachers.Add(new Teacher { Name = "Mr.A", Age = 32, Score = 100 });
        Teachers.Add(new Teacher { Name = "Mr.B", Age = 56, Score = 80 });
        Teachers.Add(new Teacher { Name = "Mr.C", Age = 60, Score = 79 });
    }
}
