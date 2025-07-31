using UnityEngine;


    [System.Serializable] // lets Unity display it in Inspector
    public class Person
    {
        public string Name;
        public int Age;
    }
    [System.Serializable]
    public class Student : Person
    {
        public float Score;
    }
    [System.Serializable]
    public class Teacher : Person
    {
        public float Score;
    }






